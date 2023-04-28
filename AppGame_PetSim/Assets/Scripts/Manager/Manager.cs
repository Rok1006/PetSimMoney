using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//this script is for implementing function/animation for UI button
public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public GameObject Cat;
    public CatAI catai;
    [Header("GameObject")]
    public GameObject Bag;
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
    [Header("Outdoor")]
    public GameObject OutdoorPannel;
    public GameObject AdventurePannel;
    public GameObject outdoorbutton;
    public GameObject outdoorIMG;
    public GameObject homeIMG;
    public GameObject Loading;

    [Header("Others")]
    public GameObject SettingPannel;
    public GameObject GiftPannel;
    public GameObject RewardPanel;
    public GameObject WareroomPannel;
    public GameObject AchievementPannel;

    [Header("Anim")]
    public int boughtItems;
    public int CostumePageCount = 0;
    public Text boughtnum;
    public GameObject numholder;
    Animator bagAnim;
    Animator InventAnim;
    [Header("Sound")]
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
        SoundM.SetActive(true);
        slm = GameObject.Find("FunctionManager").GetComponent<SaveLoadManager>();
    }

    public int openclose = 0;
    void Start()
    {

        SceneryManager.Instance.switchScene();
        catenable();
        bagAnim = Bag.GetComponent<Animator>();
        InventAnim = Inventory.GetComponent<Animator>();
        StoreMenu.SetActive(false);
        Inventory.SetActive(false);
        MainScreenUI.SetActive(true);
        boughtItems = 0;
        numholder.SetActive(false);
        GachaScreen.SetActive(false);
        ProbaScreen.SetActive(false);
        RedMachine.SetActive(true);
        BlueMachine.SetActive(false);

        CostumePanel.SetActive(false);
        HeadPage.SetActive(false);
        NeckPage.SetActive(false);
        BackPage.SetActive(false);
        SettingPannel.SetActive(false);
        GiftPannel.SetActive(false);
        RewardPanel.SetActive(false); //here
        BGCover.SetActive(false); //in gacha pannel
        CrossCloseEgg.SetActive(false);
        
        Loading.SetActive(true);
        StartCoroutine("WaitForSecond");
        
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

    public void ClickBag(){
        sm.Click();
        bagAnim.SetBool("hide", true);
        bagAnim.SetBool("out", false);
        ClickInvent();
    }
     public void ClickHideBag(){
        sm.Click();
        bagAnim.SetBool("hide", false);
        bagAnim.SetBool("out", true);
        //also close inventory
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
    public void ClickStore(){
        sm.Click();
        catdisable();
        StoreMenu.SetActive(true);
        //MainScreenUI.SetActive(false);
        ShopManager.Instance.ShopItemsGen();
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
     public void ClickCloseStore(){
        sm.Click();
        catenable();
        StoreMenu.SetActive(false);
        //MainScreenUI.SetActive(true);
    }

    public void ClickAchievement(){
        sm.Click();
        catdisable();
        AchievementPannel.SetActive(true);
        AchievementInfo.Instance.updateUI();
        //AchievementPannel.GetComponent<AchievementInfo>().lockAchievement();
    }
    public void ClickCloseAchievement(){
        sm.Click();
        catenable();
        AchievementPannel.SetActive(false);
    }



    public void ClickSetting(){
        sm.Click();
/////////////////close bag//////////////////////
        bagAnim.SetBool("hide", false);
        bagAnim.SetBool("out", true);
        //also close inventory
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
/////////////////close bag//////////////////////
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
        catdisable();
        GachaScreen.SetActive(true);
    }
    public void ClickCloseGacha(){
        sm.Click();
        catenable();
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
        catdisable();
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
        catdisable();
        CostumePanel.SetActive(true);
    }
    public void ClickCloseCostumes(){
        sm.Click();
        catenable();
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
        catdisable();
        GiftPannel.SetActive(true);
        AdsManager.Instance.CheckTime();
    }
     public void ClickCloseGift(){
        sm.Click();
        catenable();
        GiftPannel.SetActive(false);
    }
     public void ClickTodayReward(){
        sm.Click();



        /////hidebag////
        bagAnim.SetBool("hide", false);
        bagAnim.SetBool("out", true);
        //also close inventory
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        /////hidebag////


        CatAI.Instance.isPlayingToy = true;
        RewardPanel.SetActive(true);
    }
     public void ClickCloseReward(){
        sm.Click();
        CatAI.Instance.isPlayingToy = false;
        RewardPanel.SetActive(false);
    }
    public void ClickWareroom(){
        sm.Click();
        CatAI.Instance.doIDLE = true;
        CatAI.Instance.sortlayer = true;
        CatAI.Instance.Idle();
        CatAI.Instance.isPlayingToy = true;
        WareroomPannel.SetActive(true);
    }

    public void ClickCloseWareroom(){
        sm.Click();
        CatAI.Instance.onCloseWareroom();
        CatAI.Instance.doIDLE = false;
        CatAI.Instance.isPlayingToy = false;
        WareroomPannel.SetActive(false);
    }

    //Settings
    public void OnClickMusicToggle(bool isOn){
        sm.Click();
        if(isOn){//Debug.Log("Music On");
            BGM.SetActive(true);
        }else{//Debug.Log("Music Off");
            BGM.SetActive(false);
        }
    }
    public void OnClickSoundToggle(bool isOn){
         sm.Click();
        if(isOn){
            //Debug.Log("Music On");
            //SoundM.SetActive(true);
            sm.unmuteSound();
        }else{
            //Debug.Log("Music Off");
            //SoundM.SetActive(false);
            sm.muteSound();
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
    public void ClickChangeScene(){
        Loading.SetActive(true);
        StartCoroutine("WaitForSecond");
        OutdoorPannel.SetActive(false);
        SceneryManager.Instance.switchScene();
    }
    public void ClickOutdoor(){
        sm.Click();  
        OutdoorPannel.SetActive(true);
        if(SceneryManager.Instance.outside())
        {
            //Debug.Log("home img");
            homeIMG.SetActive(true);
            outdoorIMG.SetActive(false);
        }else{
            //Debug.Log("outdoor img");
            homeIMG.SetActive(false);
            outdoorIMG.SetActive(true);
        }
    }

    public void ClickCloseOutdoor(){
        sm.Click();  
        OutdoorPannel.SetActive(false);
    }

    public void ClickAdventure(){
        sm.Click();  
        AdventurePannel.SetActive(true);
        AdventureManager.Instance.updateUI();
        AdventureManager.Instance.FinishAdventure();
        if(AdventureManager.Instance.isExplore){
            Airship.Instance.exploreAnim();
        }
        OutdoorPannel.SetActive(false);
    }

    public void ClickCloseAdventure(){
        sm.Click();
        AdventurePannel.SetActive(false);
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
    public void catenable(){
        Cat.SetActive(true);
        CatAI.Instance.enableCat = true;
    }

    public void catdisable(){
        Cat.SetActive(false);
       // CatAI.Instance.canTouch = false;
    }

     IEnumerator WaitForSecond()
    { //for shorter animation
        yield return new WaitForSeconds(2);
        Loading.SetActive(false);
    }
}
