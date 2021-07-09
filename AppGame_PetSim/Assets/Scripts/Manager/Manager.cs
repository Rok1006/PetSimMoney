using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject GachaScreen;
    public GameObject ProbaScreen;
    [Header("Anim")]
    Animator starAnim;
    Animator InsideStatusAnim;
    Animator bagAnim;
    Animator InsideBagAnim;
    Animator InventAnim;
    public int boughtItems;
    public Text boughtnum;
    public GameObject numholder;
    void Awake(){
        Instance = this;
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
    }
    public void ClickStatus(){ //click the star
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
        starAnim.SetBool("hide", false);
        starAnim.SetBool("reveal", true);
        InsideStatusAnim.SetBool("reveal", false);
        InsideStatusAnim.SetBool("hide", true);
    }
    public void ClickBag(){
        bagAnim.SetBool("hide", true);
        bagAnim.SetBool("out", false);
        InsideBag.SetActive(true); //then play animation
        InsideBagAnim.SetBool("out", true);
        InsideBagAnim.SetBool("hide", false);
    }
     public void ClickHideBag(){
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
        StoreMenu.SetActive(true);
        //MainScreenUI.SetActive(false);
         //also close inventory
        InventAnim.SetBool("hide", true);
        InventAnim.SetBool("out", false);
        openclose = 0;
    }
     public void ClickCloseStore(){
        StoreMenu.SetActive(false);
        //MainScreenUI.SetActive(true);
    }
    public void ClickSetting(){

    }
    public void ClickInvent(){
      openclose+=1;
        ClickHideStatus();
    }
    public void ClickStoreBag(){
        ClickCloseStore();
        ClickBag();
        ClickInvent();
        boughtItems = 0;
    }
    public void ClickGacha(){
        GachaScreen.SetActive(true);
    }
    public void ClickCloseGacha(){
        GachaScreen.SetActive(false);
    }
    public void ClickProbability(){
        ProbaScreen.SetActive(true);
    }
    public void ClickCloseProob(){
        ProbaScreen.SetActive(false);
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
