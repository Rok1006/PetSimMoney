using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//To do: create index that adds on, every click add 1 to open the block in the next bar [done]
//to reactivate all of them back on the next day [missing the timer]
//click collect to add into players owned count [done]
public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    // #if UNITY_IOS
    //     string gameId = "4232374";
    // #else
    //     string gameId = "4232375";
    // #endif
    public static AdsManager Instance;
    public GameObject GiftPanel;
    public Image giftImage; //image replace, place the image slot ui 
    public Text giftText; //place the text ui here
    int GiftNum;
    bool green;
    int currentBar = 0;
    int lastBar = 4; //count from the opposite
    public GameObject[] bars;
    public GameObject[] ClaimButtons;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        Advertisement.Initialize("4232374");
        Advertisement.AddListener(this);
        GiftPanel.SetActive(false);
        //CheckAndLoadBars();
    }
    void Update() {
        if(Input.GetKey(KeyCode.H)){ //Cheat
            Restart();
        }
        //Debug.Log(lastBar);
        //CheckAndLoadBars();
    }
    public void PLayRewardedAd(){
        if(Advertisement.IsReady("Rewarded_iOS")){
            Advertisement.Show("Rewarded_iOS");
        }else{
            Debug.Log("not ready");
        }
    }
      public void DeactivateButton(Button button)
    {
        Debug.Log(button.name);
        button.GetComponent<Button>().interactable = false;
    }
    public void GiftImageGenerate(Sprite img){
        giftImage.GetComponent<Image>().sprite = img; 
    }
    public void GiftCountGenerate(int num){
        giftText.GetComponent<Text>().text = "X" + num.ToString();
        GiftNum = num;
    }
    public void GreenGold(bool color){
        //determine green or gold
        green = color;
    }
    public void CollectGift(){ //link to collect button
        //link num to player own count
        if(green){ //if green
            Status.Instance.LeafChange(CostMethod.GreenLeaf, GiftNum);
        }else{ //if gold
            Status.Instance.LeafChange(CostMethod.GoldLeaf, GiftNum);
        }
        GiftPanel.SetActive(false);
        RevealBar(); //unock the next bar
    }
    public void RevealBar(){ //use this to reveal the next bar when last ads is read
        for(int i = 0; i < bars.Length; i++){
            bars[currentBar].SetActive(false);
            currentBar+=1; //back to 0 at the end
            lastBar-=1;
            break;
        }
    }
    // public void CheckAndLoadBars(){
    //     if(currentBar == 1){
    //         int barNum = currentBar;
    //         for(int i = barNum; i < bars.Length; i++){
    //         bars[i].SetActive(true);
    //         }
    //          for(int i = barNum; i < ClaimButtons.Length; i++){
    //         ClaimButtons[i].GetComponent<Button>().interactable = true;
    //         }
    //     }
       
    // }
    public void Restart(){ //change everything back to normal
        for(int i = 0; i < bars.Length; i++){
            bars[i].SetActive(true);
            currentBar=0; //back to 0 at the end
            lastBar = 4;
        }
         for(int i = 0; i < ClaimButtons.Length; i++){
            ClaimButtons[i].GetComponent<Button>().interactable = true;
        }
    }
    //Ads Listened
    public void OnUnityAdsReady(string placementId){
        //Debug.Log("ads ready");
    }
     public void OnUnityAdsDidError(string message){
        //Debug.Log("Error"+message);
    }
     public void OnUnityAdsDidStart(string placementId){
        //Debug.Log("video started");
    }
     public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
         if(placementId == "Rewarded_iOS" && showResult == ShowResult.Finished){
              //trigger after ads finish
            Debug.Log("reward given");
            GiftPanel.SetActive(true);
         }
       
    }
}
