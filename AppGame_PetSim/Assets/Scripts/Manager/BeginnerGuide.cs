using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//To do: 
//- set cat to stop action stay idle
//- create complete dialogue
//- when dialogue end,(ifindex == index.length sthsth, diable beginner guide)
// event happen with dialogue, fake buttns?
//sprites quide
public class BeginnerGuide : MonoBehaviour
{
    public GameObject Cat;
    public CatAI catai;
    public GameObject BeginnerGuideUI;
    Animator BGAnim;
    [Header("Buttons")]
    public GameObject StatusButton;
    public GameObject BagButton;
    public GameObject GachaButton;
    public GameObject StoreButton;
    public GameObject CostumeButton;
    public GameObject GiftsButton;
    public GameObject DailyRewardButton;
    public GameObject[] Fingers;
    [Header("TextDisplay")]
    public string playerName;
    public float typingSpeed;
    private int index;
    public Text textDisplay;
    public GameObject NextB;
    [TextArea(3,10)]
    public string[] sentences;
  
    public int guideState = 0;
    public bool doIt = false;
    void Awake(){
        catai = Cat.GetComponent<CatAI>();
        catai.enabled = false;
    }
    void Start()
    {
        BGAnim = BeginnerGuideUI.GetComponent<Animator>();
        StartCoroutine(Type());
        NextB.GetComponent<Button>().interactable = false;
        StatusButton.GetComponent<Button>().interactable = false;
        BagButton.GetComponent<Button>().interactable = false;
        GachaButton.GetComponent<Button>().interactable = false;
        StoreButton.GetComponent<Button>().interactable = false;
        CostumeButton.GetComponent<Button>().interactable = false;
        GiftsButton.GetComponent<Button>().interactable = false;
        DailyRewardButton.GetComponent<Button>().interactable = false;
        DisableAllFingers();
        
    }

    void Update()
    {
        if(!User.Instance.beginnerGuide)
        {
            switch(guideState) {
                case 0:
                break;
                case 1:
                break;
                case 2: //introduce status panel
                        Fingers[0].SetActive(true);
                        StatusButton.GetComponent<Button>().interactable = true;
                break;
                case 3: //show status panel
                        if(doIt){
                            Fingers[0].SetActive(false);
                            Manager.Instance.ClickStatus();
                            doIt = false;
                        }
                break;
                case 4: //introduce store
                        if(doIt){
                            Manager.Instance.ClickHideStatus();
                            StatusButton.GetComponent<Button>().interactable = false;
                            StoreButton.GetComponent<Button>().interactable = true;
                            Fingers[3].SetActive(true);
                            doIt = false;
                        }
                break;
                case 5:
                        if(doIt){
                            Fingers[3].SetActive(false);
                            Manager.Instance.ClickStore();
                            doIt = false;
                        }
                break;
                case 6: //introduce bag
                        if(doIt){
                            Manager.Instance.ClickCloseStore();
                            StoreButton.GetComponent<Button>().interactable = false;
                            BagButton.GetComponent<Button>().interactable = true;
                            Fingers[1].SetActive(true);
                            doIt = false;
                        }
                break;
                case 7:
                        if(doIt){
                            Fingers[1].SetActive(false);
                            Manager.Instance.ClickBag();
                            doIt = false;
                        }
                break;
                case 8: //introduce gacha
                        if(doIt){
                            Manager.Instance.ClickHideBag();
                            BagButton.GetComponent<Button>().interactable = false;
                            GachaButton.GetComponent<Button>().interactable = true;
                            Fingers[2].SetActive(true);
                            doIt = false;
                        }
                break;
                case 9:
                    if(doIt){
                            Fingers[2].SetActive(false);
                            Manager.Instance.ClickGacha();
                            doIt = false;
                        }
                break;
                case 10: //contiue dialogue in gacha screen
                break;
                case 11:
                if(doIt){
                            Manager.Instance.ClickCloseGacha();
                            GachaButton.GetComponent<Button>().interactable = false;
                            CostumeButton.GetComponent<Button>().interactable = true;
                            Fingers[4].SetActive(true);
                            doIt = false;
                        }
                break;
                case 12:
                    if(doIt){
                            Fingers[4].SetActive(false);
                            Manager.Instance.ClickCostumes();
                            doIt = false;
                        }
                break;
                case 13:
                if(doIt){
                            Manager.Instance.ClickCloseCostumes();
                            CostumeButton.GetComponent<Button>().interactable = false;
                            GiftsButton.GetComponent<Button>().interactable = true;
                            Fingers[5].SetActive(true);
                            doIt = false;
                        }
                break;
                case 14:
                    if(doIt){
                            Fingers[5].SetActive(false);
                            Manager.Instance.ClickGift();
                            doIt = false;
                        }
                break;
                case 15:
                if(doIt){
                            Manager.Instance.ClickCloseGift();
                            GiftsButton.GetComponent<Button>().interactable = false;
                            DailyRewardButton.GetComponent<Button>().interactable = true;
                            Fingers[6].SetActive(true);
                            doIt = false;
                        }
                break;
                case 16:
                if(doIt){
                        Fingers[6].SetActive(false);
                        Manager.Instance.ClickTodayReward();
                        doIt = false;
                    }
                break;
                case 17: //Lastly, dont forget to always reflect your love towards your cat, by gently petting it.
                  //empty 
                if(doIt){
                        Fingers[7].SetActive(true);
                        Manager.Instance.ClickCloseReward();
                        doIt = false;
                    }
                break;
                case 18: //And for now.......You are all set! Have a great time!
                    Fingers[7].SetActive(false); 
                    EnableAllButton();
                    catai.enabled = true;
                    BeginnerGuideUI.SetActive(false);
                    User.Instance.ExpUP(User.Instance.maxfpValue); //here level up
                    User.Instance.beginnerGuide = true;
                    this.enabled = false;
                break;


            }
            if(textDisplay.text == sentences[index]){
                NextB.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            NoGuide();
        }
    }
    public void DisableAllFingers(){
        for(int i = 0; i < Fingers.Length; i++){
            Fingers[i].SetActive(false);
    }
    }
    //about the function of dialogue
    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void EnableAllButton(){
        StatusButton.GetComponent<Button>().interactable = true;
        BagButton.GetComponent<Button>().interactable = true;
        GachaButton.GetComponent<Button>().interactable = true;
        StoreButton.GetComponent<Button>().interactable = true;
        CostumeButton.GetComponent<Button>().interactable = true;
        GiftsButton.GetComponent<Button>().interactable = true;
        DailyRewardButton.GetComponent<Button>().interactable = true;
    }
    public void NextSentences(){
        doIt = true;
        BGAnim.SetTrigger("Next");
        NextB.GetComponent<Button>().interactable = false;
        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else{
            textDisplay.text = "";
        }
        guideState++;
    }
    public void SkipButton(){
        DisableAllFingers();
        EnableAllButton();
        catai.enabled = true;
        BeginnerGuideUI.SetActive(false);
        User.Instance.ExpUP(User.Instance.maxfpValue); //here player level up tp 1
        //check beginner guide as finished
        User.Instance.beginnerGuide = true;
        this.enabled = false;
    }
    public void NoGuide(){
        DisableAllFingers();
        EnableAllButton();
        catai.enabled = true;
        BeginnerGuideUI.SetActive(false);
        this.enabled = false;
    }
}
