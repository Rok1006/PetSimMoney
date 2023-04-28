using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Linq;
using Random=UnityEngine.Random;
using UnityEngine.EventSystems;


//public enum source { orginial, ocean, ghost, dessert, Hamburger }
public enum FurnitureType { wallpaper, Toys, Furnitures }
public enum Result {planet, trash}

public class AdventureManager : MonoBehaviour
{
    public static AdventureManager Instance;
    public List<GameObject> _PlanetList = new List<GameObject>();
    public List<GameObject> PlanetList { get { return _PlanetList; } }

    public List<int> AdventurePlanetList = new List<int>(); //put into saveloadManager

    [SerializeField]GameObject prefabsFolder;
    GameObject[] PlanetPT;
    public int planetID;//put into saveloadManager
    public Result result;
    [Header("Pannel")]
    public GameObject ExplorePannel;
    public GameObject RewardPannel;
    [Header("UI")]
    [SerializeField] GameObject exploreUI;
    Text DateTimeText;
    GameObject notice;
    [Header("Timer")]
    int minuteGTC, hourGTC;
    public int adventureNUM=0;//how many time player go to adventure
    DateTime time;
    public TimeSpan ts;
    int timeTaken;//timer stand for minutes
    int hh,mm,dd;
    public bool isExplore;

    void Awake(){
        Instance = this;
        _PlanetList = new List<GameObject>(Resources.LoadAll<GameObject>("Adventure/Planet"));
        
        DateTimeText = exploreUI.transform.Find("Text").GetComponentInChildren<Text>();
        exploreUI.GetComponent<Button>().interactable = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void doUpdate()
    {
        StartCoroutine("updateTimer");
    }

    public void updateUI(){
        PlanetPT = GameObject.FindGameObjectsWithTag("PlanetPT");
        for(int i=0; i<AdventurePlanetList.Count ; i++){
            if(PlanetPT[i].GetComponent<Image>() == null)
            {
                PlanetPT[i].name = PlanetList[AdventurePlanetList[i]].GetComponent<PlanetInfo>().ID.ToString();
                PlanetPT[i].AddComponent<Image>().sprite = PlanetList[AdventurePlanetList[i]].GetComponent<PlanetInfo>().GetComponent<Image>().sprite;
            }
         }
        if(PlanetPT[AdventurePlanetList.Count].GetComponent<Image>() == null)
        {
            PlanetPT[AdventurePlanetList.Count].AddComponent<Image>().sprite = PlanetList[0].GetComponent<PlanetInfo>().GetComponent<Image>().sprite;
            PlanetPT[AdventurePlanetList.Count].name = "0";
        }
    }



    public void FinishAdventure(){
        if(isExplore && ts.Seconds <= 0){
            isExplore = false;
            if(planetID != 0){//existing planet
                //show animation
                //get reward
            }else if(AdventurePlanetList.Count ==  PlanetPT.Length) //already unlock all planet
            {
                //show animation
                //get reward
                Debug.Log("already unlock all point");
            }else if(unlockNew()){
                switch(result){
                    case Result.planet: //planet
                        AdventurePlanetList.Add(RanPlanet());
                    break;
                    case Result.trash: //planet
                        AdventurePlanetList.Add(RanTrash());
                    break;
                }
                int i = AdventurePlanetList.Count - 1;
                PlanetPT[i].name = PlanetList[AdventurePlanetList[i]].GetComponent<PlanetInfo>().ID.ToString();
                PlanetPT[i].GetComponent<Image>().sprite = PlanetList[AdventurePlanetList[i]].GetComponent<PlanetInfo>().GetComponent<Image>().sprite;
                PlanetPT[AdventurePlanetList.Count].AddComponent<Image>().sprite = PlanetList[0].GetComponent<PlanetInfo>().GetComponent<Image>().sprite;
                PlanetPT[AdventurePlanetList.Count].name = "0";
                //show success animation
                Manager.Instance.AdventurePannel.SetActive(true);
                Airship.Instance.StartCoroutine("successAnim");
                Airship.Instance.success.transform.Find("Planet").GetComponentInChildren<Image>().sprite =PlanetList[AdventurePlanetList[i]].GetComponent<PlanetInfo>().GetComponent<Image>().sprite; 
            }else{
                Debug.Log("fail to explore new planet");
                //show fail animation
            }
            exploreUI.GetComponent<Button>().interactable = false;
            exploreUI.transform.Find("Notice").gameObject.SetActive(false);
        }

        //reward pannel
    }

    public void ClickPlanet(){
        
        ExplorePannel.SetActive(true);
        planetID =int.Parse(EventSystem.current.currentSelectedGameObject.name);
        if(EventSystem.current.currentSelectedGameObject.name == "0"){
            ExplorePannel.transform.Find("Go").GetComponentInChildren<Button>().interactable = false;
            //ExplorePannel.transform.Find("Text").GetComponentInChildren<Text>().text = exploreTime().ToString("HH:mm:ss");
            
        }else{
            ExplorePannel.transform.Find("Go").GetComponentInChildren<Button>().interactable = true;
            //ExplorePannel.transform.Find("Text").GetComponentInChildren<Text>().text = PlanetList[planetID].GetComponent<PlanetInfo>().title;
                
        }
        ExplorePannel.transform.Find("Text").GetComponentInChildren<Text>().text = exploreTime().ToString("HH:mm:ss");
        ExplorePannel.transform.Find("IMG").GetComponentInChildren<Image>().sprite =PlanetList[planetID].GetComponent<PlanetInfo>().GetComponent<Image>().sprite; 
    }
    public void ClickCloseExplore(){
        ExplorePannel.SetActive(false);
    }

    public void ClickExplore(){
        if(!isExplore)
        {
            time = DateTime.Now.AddMinutes(timeTaken);
            //time = DateTime.Now.AddSeconds(timeTaken);//dev
            isExplore = true;
            adventureNUM++;
            ExplorePannel.SetActive(false);
            Airship.Instance.explore.transform.Find("Planet").GetComponentInChildren<Image>().sprite =PlanetList[planetID].GetComponent<PlanetInfo>().GetComponent<Image>().sprite; 
            Airship.Instance.exploreAnim();
            Airship.Instance.StartCoroutine("exploreAnim2");
            doUpdate();   
        }else{//reminder
        }
    }
    public void ClickGO(){
        ExplorePannel.SetActive(false);
        Airship.Instance.StartCoroutine("switchPlanet");
        SceneryManager.Instance.switchPlanet(planetID);
        //SceneryManager.Instance.playAnim();
    }
    int RanTrash(){
        return Random.Range(7,PlanetList.Count);
    }
    int RanPlanet(){
        int i = Random.Range(1,7);
        while(AdventurePlanetList.Contains(i))
        {
            i = Random.Range(1,7);
        }
        return i;
    }
    bool unlockNew(){//20%trash 5%planet
        int i = Random.Range(0,101);
        if(i <= 20){
            result = Result.trash;
            return true;
        }else if(i>=20 && i<User.Instance.level - 5){
            result = Result.planet;
            return true;
        }else{
            return false;
        }
    }

    DateTime exploreTime(){
        //get adventure hour and minute
        timeTaken = adventureNUM * adventureNUM + 1; //minute
        time = DateTime.Now.AddMinutes(timeTaken);
        while(timeTaken >=60 ){
            timeTaken= timeTaken-60;
            hh++;
        }
        mm = timeTaken ;
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day,hh,mm,0);
    }
    
    IEnumerator updateTimer(){
        //Debug.Log("ts "+ ts);
        ts = time - DateTime.Now;
        DateTimeText.text = Math.Round(ts.TotalSeconds).ToString();
        if(ts.Seconds <= 0){
            DateTimeText.text = "0";
            exploreUI.transform.Find("Notice").gameObject.SetActive(true);
            exploreUI.GetComponent<Button>().interactable = true;
        }else{
            yield return new WaitForSeconds(0.5f);
            doUpdate(); 
        }
        
    }
}
