using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;


public enum RewardType { greenLeaf, goldenLeaf, exp}
public enum RewardCondition {Trash, greenLeaf, Level, adventureNUM}


public  class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    [Header("rewardType")]
    [SerializeField] private RewardType _type;
    public RewardType type { get { return _type; } }
    [SerializeField] private int rewardValue;
    [Header("activeCondidtion")]
    [SerializeField] private RewardCondition _kind;
    public RewardCondition kind { get { return _kind; } }
    [SerializeField] private int achievedValue;

    GameObject[] List;//AchievementList
    GameObject lockBlock;
    bool active = false;

    [Header("AchievementCount")]
    public int[] Count = new int[4]{0, 0,0,0}; //RewardCondition



    void Awake(){
        Instance = this;
    }
    void Start(){
        //updateUI();
    }
    public void updateUI()
    {
        lockBlock = this.transform.Find("LockBlock").gameObject;
        List = GameObject.FindGameObjectsWithTag("Achievement");
        foreach(GameObject Lists in List)
        {
            AchievementInfo info = Lists.GetComponent<AchievementInfo>();
            RewardCondition kind = info.kind;
            int value = info.achievedValue;
            if(Check(kind,value)){
                //unlock
                this.GetComponent<Button>().interactable = false;
                lockBlock.SetActive(true);
            }else{
                //lock
                lockBlock.SetActive(false);
                this.GetComponent<Button>().interactable = true;
            }
            //PlanetPT[i].AddComponent<Image>().sprite = Lists.GetComponent<AchievementInfo>().GetComponent<Image>().sprite;
        }
    }

    /*bool check(RewardCondition method, int value){

    }*/
    void check(){//check whether achieved the achievement
       switch(kind){
            case RewardCondition.Trash:
                if(Status.Instance.trashCounter >= achievedValue){
                    active = true;
                }
            break;
            case RewardCondition.greenLeaf:
                if(Status.Instance.greenLeafCounter >= achievedValue){
                    active = true;
                }
            break;
            case RewardCondition.Level:
                if(User.Instance.level >= achievedValue){
                    active = true;
                }
            break;
        }
    }


    public void getReward()
    {
        
        if(lockBlock.activeSelf){
            switch(type){
                case RewardType.greenLeaf:
                    Status.Instance.LeafChange(CostMethod.GreenLeaf, rewardValue);
                break;
                case RewardType.goldenLeaf:
                    Status.Instance.LeafChange(CostMethod.GoldLeaf, rewardValue);
                break;
                case RewardType.exp:
                    User.Instance.fpValue += rewardValue;
                break;
            }
            DailyRewardsManager.Instance.RewardPopUpPanel.SetActive(true);
        }
    }
    //Check
    bool Check(RewardCondition kind,int value){
        int methodNum = (int) kind;
        if(Count[methodNum] <= value){
            return true;
        }else{
            return false;
        }
    }
    //Achievement Counter
    public void Counter(RewardCondition kind , int value)
    {
        int methodNum = (int) kind;
        Count[methodNum] += value;
    }

}




