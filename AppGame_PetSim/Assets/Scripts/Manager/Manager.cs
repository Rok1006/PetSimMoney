using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//this script is for implementing function/animation for UI button
public class Manager : MonoBehaviour
{
    public static Manager Instance;
    [Header("GameObject")]
    public GameObject Star;
    public GameObject InsideStatus;
    public GameObject Bag;
    public GameObject InsideBag;
    public GameObject StoreMenu;
    public GameObject Inventory;
    public GameObject MainScreenUI;
    [Header("Costume")]
    public GameObject CostumePanel;
    public GameObject HeadPage;
    public GameObject NeckPage;
    public GameObject BackPage;
    [Header("Gacha")]
    public GameObject GachaScreen;
    public GameObject ProbaScreen;
    public GameObject RedMachine;
    public GameObject BlueMachine;
    public GameObject ExchangePanel;
    [Header("Others")]
    public GameObject SettingPannel;
    public GameObject GiftPannel;
    public GameObject RewardPanel;
    [Header("Anim")]
    public int boughtItems;
    public int CostumePageCount = 0;
    public Text boughtnum;
    public GameObject numholder;
    Animator starAnim;
    Animator InsideStatusAnim;
    Animator bagAnim;
    Animator InsideBagAnim;
    Animator InventAnim;
    public GameObject SoundM;
    public SoundManager sm;
    public GameObject BGM;
    public string sceneName;
    public GameObject BGCover;
    public GameObject CrossCloseEgg;
    public GameObject functionManager;
    public SaveLoadManager slm;
  
    void Awake(){
        Instance = this;
        sm = SoundM.GetComponent<SoundManager>();
        BGM.SetActive(true);
        SoundM.SetActive(true);
        slm = GameObject.Find("FunctionManager").GetComponent<SaveLoadManager>();
    }

    public int openclose = 0;
    void Start()
    {
        starAnim = Star.GetComponent<Animator>();
        InsideStatusAnim = InsideStatus.GetComponent<Animator>();
        bagAnim = Bag.GetComponent<Animator>();
        InsideBagAnim = InsideBag.GetComponent<Animator>();
        InventAnim = Inventory.GetComponent<Animator>();
        InsideStatus.SetActive(false);
        InsideBag.SetActive(false);
        StoreMenu.SetActive(false);
        Inventory.SetActive(false);
        MainScreenUI.SetActive(true);
        boughtItems = 0;
        numholder.SetActive(false);
        GachaScreen.SetActive(false);
        ProbaScreen.SetActive(false);
        RedMachine.SetActive(true);
        BlueMachine.SetActive(false);
        ExchangePanel.SetActive(false);

        CostumePanel.SetActive(false);
        HeadPage.SetActive(false);
        NeckPage.SetActive(false);
        BackPage.SetActive(false);
        SettingPannel.SetActive(false);
        GiftPannel.SetActive(false);
        RewardPanel.SetActive(false);
        BGCover.SetActive(false); //in gacha pannel
        CrossCloseEgg.SetActive(false);
        
    }
    void Update()
    {
         DetermineOnOff();
         if(boughtItems>0){
             numholder.SetActive(true);
         }else{
             numholder.SetActive(false);
         }
         boughtnum.text = boughtItems.ToString();

         if(CostumePageCount==0){
            HeadPage.SetActive(true);
            NeckPage.SetActive(false);
            BackPage.SetActive(false);
         }else if(CostumePageCount==1){
            HeadPage.SetActive(false);
            NeckPage.SetActive(true);
            BackPage.SetActive(false);
         }else if(CostumePageCount==2){
            HeadPage.SetActive(false);
            NeckPage.SetActive(false);
            BackPage.SetActive(true);
         }
         if(Input.GetKey(KeyCode.B)){
             sm.Click();
         }
    }
    public void ClickStatus(){ //click the star
        sm.Click();
        starAnim.SetBool("hide", true);
        starAnim.SetBool("reveal", false);
        InsideStatus.SetActive(true); //then play animation
        InsideStatusAnim.SetBool("reveal", true);
        InsideStatusAnim.SetBool("hide", false);
        //hide invent
        //ClickHideBag();
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
    public void ClickHideStatus(){
        sm.Click();
        starAnim.SetBool("hide", false);
        starAnim.SetBool("reveal", true);
        InsideStatusAnim.SetBool("reveal", false);
        InsideStatusAnim.SetBool("hide", true);
    }
    public void ClickBag(){
        sm.Click();
        bagAnim.SetBool("hide", true);
        bagAnim.SetBool("out", false);
        InsideBag.SetActive(true); //then play animation
        InsideBagAnim.SetBool("out", true);
        InsideBagAnim.SetBool("hide", false);
        ClickInvent();
    }
     public void ClickHideBag(){
        sm.Click();
        bagAnim.SetBool("hide", false);
        bagAnim.SetBool("out", true);
        InsideBagAnim.SetBool("out", false);
        InsideBagAnim.SetBool("hide", true);
        //also close inventory
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
    public void ClickStore(){
        sm.Click();
        StoreMenu.SetActive(true);
        //MainScreenUI.SetActive(false);
        ShopManager.Instance.ShopItemsGen();
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
     public void ClickCloseStore(){
        sm.Click();
        StoreMenu.SetActive(false);
        //MainScreenUI.SetActive(true);
    }
    public void ClickSetting(){
        sm.Click();
        SettingPannel.SetActive(true);
    }
    public void CLickCLoseSetting(){
        sm.Click();
        SettingPannel.SetActive(false);
    }
    public void SettingtoMenu(){ //click to menu
        slm.SaveData();
         SceneManager.LoadScene(sceneName);
    }
    public void ClickInvent(){
        sm.Click();
        openclose+=1;
        ClickHideStatus();
    }
    public void ClickStoreBag(){
        sm.Click();
        ClickCloseStore();
        ClickBag();
        //ClickInvent();
        boughtItems = 0;
    }
    //Gacha Page Related
    public void ClickGacha(){
        sm.Click();
        GachaScreen.SetActive(true);
    }
    public void ClickCloseGacha(){
        sm.Click();
        GachaScreen.SetActive(false);
    }
    public void ClickProbability(){
        sm.Click();
        ProbaScreen.SetActive(true);
    }
    public void ClickCloseProob(){
        sm.Click();
        ProbaScreen.SetActive(false);
    }
    public void ClickItemB(){
        sm.Click();
        RedMachine.SetActive(false);
        BlueMachine.SetActive(true);
    }
     public void ClickCostumeB(){
        sm.Click();
        RedMachine.SetActive(true);
        BlueMachine.SetActive(false);
    }
    //Costume Page RElated
    public void ClickCostumes(){
        sm.Click();
        CostumePanel.SetActive(true);
    }
    public void ClickCloseCostumes(){
        sm.Click();
        CostumePanel.SetActive(false);
    }
     public void ClickLeft(){
         sm.Click();
         if(CostumePageCount>0){
           CostumePageCount-=1;   
         }else{
            CostumePageCount=2;//Last
         }
    }
     public void ClickRight(){
         sm.Click();
        if(CostumePageCount<2){
           CostumePageCount+=1;   
        }else{
            CostumePageCount=0;//First
        }
    }
    //others
    public void ClickGift(){
        sm.Click();
        GiftPannel.SetActive(true);
        AdsManager.Instance.CheckTime();
    }
     public void ClickCloseGift(){
        sm.Click();
        GiftPannel.SetActive(false);
    }
     public void ClickTodayReward(){
        sm.Click();
        RewardPanel.SetActive(true);
    }
     public void ClickCloseReward(){
        sm.Click();
        RewardPanel.SetActive(false);
    }
    //Settings
    public void OnClickMusicToggle(bool isOn){
        sm.Click();
        if(isOn){
            Debug.Log("Music On");
            BGM.SetActive(true);
        }else{
            Debug.Log("Music Off");
            BGM.SetActive(false);
        }
    }
      public void OnClickSoundToggle(bool isOn){
         sm.Click();
        if(isOn){
            Debug.Log("Music On");
            SoundM.SetActive(true);
        }else{
            Debug.Log("Music Off");
            SoundM.SetActive(false);
        }
    }
      public void OnClickNoticeToggle(bool isOn){
          sm.Click();
        if(isOn){
            Debug.Log("Music On");
        }else{
            Debug.Log("Music Off");
        }
    }
    public void UpdateSettingPannel()
    {
        SettingPannel.transform.Find("Music/MusicToggle").gameObject.GetComponent<Toggle>().isOn = BGM.activeSelf;
        SettingPannel.transform.Find("Sounds/SoundToggle").gameObject.GetComponent<Toggle>().isOn = SoundM.activeSelf;
    }
    void DetermineOnOff(){   //this is the function for the APP ui Pannel
    if(openclose==1){
        Inventory.SetActive(true);
        InventAnim.SetBool("out", true);
        InventAnim.SetBool("hide", false);
        }
        if(openclose==2){
        InventAnim.SetBool("out", false);
        InventAnim.SetBool("hide", true);
        openclose = 0; //close
        }
    }
}
