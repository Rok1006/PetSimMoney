using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random=UnityEngine.Random;
//SetLevel for speed
public class mochi : MonoBehaviour
{
    public static mochi Instance;
    public List<GameObject> _prefabsList = new List<GameObject>();
    public List<GameObject> prefabsList { get { return _prefabsList; } }
    GameObject[] mochiList;
    [SerializeField] GameObject Folder;
    [SerializeField] Text DateTimeText;
    [SerializeField] Text Score;
    [SerializeField] Text LevelText;
    [SerializeField] Text BESTScore;
    [SerializeField] GameObject Point1;
    [SerializeField] GameObject Point2;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Game;
    Vector3 Pos;
    float x1,x2 ;
    int level;
    public int score;
    TimeSpan ts;
    DateTime time;
    public int bestScore;

    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bestScore = ScoreManager.Instance.mochiScore;
        Pos = Point1.transform.position;
        x1 = Pos.x;
        x2 = Point2.transform.position.x;
        _prefabsList = new List<GameObject>(Resources.LoadAll<GameObject>("MiniGame/mochi"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnMouseDown(){
         Debug.Log("onclick");
        Game.SetActive(true);
    }
    public void gameStart(){
        Cat.Instance.mochiBool = true;
        Panel.SetActive(false);
        Manager.Instance.catdisable();
        score = 0;
        Score.text = score.ToString();
        time = DateTime.Now.AddSeconds(20);
        doUpdate();
        InvokeRepeating("NewPrefabs",0f,0.2f);//repeat every 2s
    }
    public void FinishGame(){
        CancelInvoke();
        mochiList = GameObject.FindGameObjectsWithTag("mochi");
        foreach(GameObject hi in mochiList){
            Destroy(hi);
        }
        
        if(score > bestScore){
            bestScore = score;
            ScoreManager.Instance.mochiScore = score;
            BESTScore.text = score.ToString();
        }
        Cat.Instance.Idle();
        Cat.Instance.mochiBool = false;
        Panel.SetActive(true);//restart the game

        
        //Ads for double
        if(score >= 1000){
            ScoreManager.Instance.CollectGift(5);
        }else if(score >= 500){
            ScoreManager.Instance.CollectGift(2);
        }

    }
    public void closePanel(){
        Manager.Instance.catenable();
    }
    void NewPrefabs()
    {
        Instantiate(prefabsList[randomPrefab()], new Vector3(Random.Range(x1,x2), Pos.y,Pos.z), Quaternion.identity,Folder.transform.parent);
    }
    int randomPrefab(){
        int p = Random.Range(0,100);
        if(p>0 && p<25){
            return 6;
        }else if(p>=25 && p<50){
            return 5;
        }else if(p>=50 && p<60){
            return 0;
        }else{
            return Random.Range(1,5);
        }
    }

    public void StatsChange(int i){
        score += i;
        Score.text = score.ToString();
    }
    void doUpdate()
    {
        StartCoroutine("updateTimer");
    }
    IEnumerator updateTimer(){
        ts = time - DateTime.Now;
        DateTimeText.text = Math.Round(ts.TotalSeconds).ToString();
        if(ts.Seconds <= 0){
            DateTimeText.text = "0";
            FinishGame();
        }else{
            yield return new WaitForSeconds(0.5f);
            doUpdate(); 
        }
    }
}
