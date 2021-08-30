using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DailyRewardsManager : MonoBehaviour
{
    //using switch case, case 1-7 sth sth
    //in each case, open the block 
    //++index: when num arrive 7 , reset it when the next day come
    //then save the current index in save and load
    public static DailyRewardsManager Instance;
    public GameObject[] Day; //place all the slots here
    public GameObject[] Blocks;
    public GameObject[] Checks;
    public int currentday = 0;
    public int buttonID;
    public bool collected = false;
    public GameObject notice;
    [Header("GiftPanel")]
    public GameObject RewardPopUpPanel;
    public Image rewardImage; //image replace, place the image slot ui 
    public Text rewardText; //place the text ui here
    int RewardNum; //amount of gift
    string Today; //what day is today
    [Header("NeededItems")]
    public GameObject OrangeJuice;
    public GameObject Pretzels;
    public GameObject Milk;
    public GameObject Cupcake;
    public GameObject WoolBall;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        OffAllBlocksCheck();
        SwitchingDays();
        RewardPopUpPanel.SetActive(false);
        //Day[currentday].GetComponent<Button>().interactable = true;
        notice.SetActive(false);
    }

    void Update()
    {
        SwitchingDays();
        if(Input.GetKey(KeyCode.R)){
            OffAllBlocksCheck();
            collected = false;
            notice.SetActive(true);
            Manager.Instance.RewardPanel.SetActive(true);
            currentday = 0;
        }
        // if(Input.GetKeyDown(KeyCode.T)){
        //     currentday++;
        //     if(currentday>6){
        //         currentday = 0;
        //     }
        // }
        
    }
    public void OpenNextDayReward(){
            notice.SetActive(true);
            Manager.Instance.RewardPanel.SetActive(true);
            collected = false; //turn false again when next day come, player can now collect the next day reward
            currentday++;
            if(currentday>6){
                currentday = 0;
                OffAllBlocksCheck();
            }
    }
    public void OffAllBlocksCheck(){
        for(int i = 0; i < Blocks.Length; i++){
            Blocks[i].SetActive(true);
            //break;
        }
        for(int i = 0; i < Checks.Length; i++){
            Checks[i].SetActive(false);
            //break;
        }
          for(int i = 0; i < Day.Length; i++){
            Day[i].GetComponent<Button>().interactable = false;
            //break;
        }
    }
    public void SwitchingDays(){
        switch(currentday){
            case 0://Day1
                Blocks[0].SetActive(false); 
                if(!collected){
                Day[0].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 1://Day2
                Blocks[1].SetActive(false); 
                Day[0].GetComponent<Button>().interactable = false; //make it clickable
                if(!collected){
                Day[1].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 2://Day3
                Blocks[2].SetActive(false); 
                Day[1].GetComponent<Button>().interactable = false;
                if(!collected){
                Day[2].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 3://Day4
                Blocks[3].SetActive(false); 
                Day[2].GetComponent<Button>().interactable = false;
                if(!collected){
                Day[3].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 4://Day5
                Blocks[4].SetActive(false); 
                Day[3].GetComponent<Button>().interactable = false;
                if(!collected){
                Day[4].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 5://Day6
                Blocks[5].SetActive(false); 
                Day[4].GetComponent<Button>().interactable = false;
                if(!collected){
                Day[5].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 6://Day7
                Blocks[6].SetActive(false); 
                Day[5].GetComponent<Button>().interactable = false;
                if(!collected){
                Day[6].GetComponent<Button>().interactable = true; //make it clickable
                }
            break;
            case 7:
                Day[6].GetComponent<Button>().interactable = false;
                //then when next day come do  if current day = 7 reset num
            break;
        }
    }
    //FUnction
      public void DeactivateButton(Button button)
    {
        notice.SetActive(false);
        Debug.Log(button.name);
        button.GetComponent<Button>().interactable = false;
        Day[buttonID].GetComponent<Button>().interactable = false;
        Checks[buttonID].SetActive(true);
        collected = true;
        //GameObject c = button.gameobject.transform.Find("Check") ; //on the parent gameobject
    }
    public void GetButtonNum(int num){ //assign the num back in inspector add to button
        buttonID = num;
    }
     public void RewardImageGenerate(Sprite img){ //add to button
        rewardImage.GetComponent<Image>().sprite = img; 
    }
    public void RewardCountGenerate(int num){ //add to button
        rewardText.GetComponent<Text>().text = "X " + num.ToString();
        RewardNum = num;
    }
    public void DetermineRewardOnDay(string _day){ //add to button
        Today = _day;
    }
    public void CollectGift(){ //link to collect button
        //link num to player own count
        if(Today == "Day1"){  
            Status.Instance.LeafChange(CostMethod.GreenLeaf, RewardNum);
        }else if(Today == "Day2"){  
            Status.Instance.LeafChange(CostMethod.GoldLeaf, RewardNum);
        }else if(Today == "Day3"){  
            Status.Instance.LeafChange(CostMethod.GoldLeaf, RewardNum);
        }else if(Today == "Day4"){  
            Status.Instance.LeafChange(CostMethod.GoldLeaf, RewardNum);
        }else if(Today == "Day5"){  
             GameObject item = OrangeJuice;
             Inventory.instance.AddToSlots(item, RewardNum);
        }else if(Today == "Day6"){  
             GameObject item = Pretzels;
             Inventory.instance.AddToSlots(item, RewardNum);
        }else if(Today == "Day7"){  
             GameObject item0 = Milk;
              Inventory.instance.AddToSlots(item0, RewardNum); //add to inventory
             GameObject item1 = Cupcake;
              Inventory.instance.AddToSlots(item1, RewardNum);
             GameObject item2 = WoolBall;
              Inventory.instance.AddToSlots(item2, RewardNum);
        }
        RewardPopUpPanel.SetActive(false);
        //currentday++; //move on to the next day
    }
    public void ClaimRewards(){
        RewardPopUpPanel.SetActive(true);
    }
}
