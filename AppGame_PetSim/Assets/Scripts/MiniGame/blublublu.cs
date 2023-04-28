using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random=UnityEngine.Random;
public class blublublu : MonoBehaviour
{
    public static blublublu Instance;
    public List<GameObject> _prefabsList = new List<GameObject>();
    public List<GameObject> prefabsList { get { return _prefabsList; } }
    GameObject[] fishList;
    [SerializeField] GameObject Folder;
    [SerializeField] GameObject Point1;
    [SerializeField] GameObject Point2;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Game;
    [SerializeField] Text DateTimeText;
    [SerializeField] Text BESTScore;
    GameObject Prefab;
    float y1,y2;
    Vector3 Pos;
    TimeSpan ts;
    DateTime startTime;
    int score;
    public int bestScore =0;
    
    // Start is called before the first frame update
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        bestScore = ScoreManager.Instance.bluScore;
        Pos = Point1.transform.position;
        y1 = Pos.y;
        y2 = Point2.transform.position.y;
        _prefabsList = new List<GameObject>(Resources.LoadAll<GameObject>("MiniGame/blublublu"));
        Debug.Log(prefabsList.Count);
        
    }
    void OnMouseDown(){
        Game.SetActive(true);
        var rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        Manager.Instance.catdisable();
    }
    public void gameStart(){
        cat.SetActive(true);
        Cat.Instance.bluBool = true;
        startTime = DateTime.Now;
        doUpdate();
        InvokeRepeating("NewPrefabs",0f,0.15f);//repeat every 2s
    }
    public void FinishGame(){
        CancelInvoke();
        StopCoroutine("updateTimer");
        fishList = GameObject.FindGameObjectsWithTag("Fish");
        foreach(GameObject hi in fishList){
            Destroy(hi);
        }
        score = Mathf.RoundToInt((float)ts.TotalSeconds);
        if(score > bestScore){
            bestScore = score;
            ScoreManager.Instance.bluScore = score;
            BESTScore.text = score.ToString();
        }
        
        Panel.SetActive(true);//restart the game

        if(score >= 60){
            ScoreManager.Instance.CollectGift(5);
        }else if(score >= 30){
            ScoreManager.Instance.CollectGift(2);
        }

    }
    public void closeGame(){
        CancelInvoke();
        StopCoroutine("updateTimer");
        fishList = GameObject.FindGameObjectsWithTag("Fish");
        foreach(GameObject hi in fishList){
            Destroy(hi);
        }
        Cat.Instance.Idle();
        Cat.Instance.bluBool = false;
        var rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = false;
        GetComponent<Collider2D>().enabled = true;
        Manager.Instance.catenable();
    }
    void NewPrefabs()
    {
        Prefab = Instantiate(prefabsList[randomPrefab()], new Vector3( Pos.x, Random.Range(y1,y2),Pos.z), Quaternion.identity,Folder.transform.parent);
        Prefab.GetComponent<AddForce>().gameStart();
        Destroy(Prefab , 2);
    }
    int randomPrefab(){
        int i = Random.Range(0,100);
        if(i <= 5){
            Debug.Log("whale");
            return prefabsList.Count -1;
        }else{
            return Random.Range(0,prefabsList.Count-1);
        }
        
    }
    void doUpdate()
    {
        StartCoroutine("updateTimer");
    }
    IEnumerator updateTimer(){
        ts =DateTime.Now - startTime ;
        DateTimeText.text = Math.Round(ts.TotalSeconds).ToString();
        yield return new WaitForSeconds(0.5f);
        doUpdate(); 
    }
}
