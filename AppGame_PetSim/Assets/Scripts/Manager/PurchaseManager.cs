using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PurchaseManager : MonoBehaviour
{
     public static PurchaseManager Instance;
    public GameObject purchasePopupPanel;
    public Image productImage; //image replace, place the image slot ui 
    public Text productText; //place the text ui here
    int leafNum;
    bool green;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        purchasePopupPanel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void ProductImageGenerate(Sprite img){
        productImage.GetComponent<Image>().sprite = img; 
    }
    public void ProductCountGenerate(int num){
        productText.GetComponent<Text>().text = "+" + num.ToString();
        leafNum = num;
    }
    public void GreenGold(bool color){    //determine green or gold
        green = color;
    }
    public void CollectProduct(){ //link to collect button
        //link num to player own count
        if(green){ //if green
            Status.Instance.LeafChange(CostMethod.GreenLeaf, leafNum);
        }else{ //if gold
            Status.Instance.LeafChange(CostMethod.GoldLeaf, leafNum);
        }
        purchasePopupPanel.SetActive(false);
    }
    //button function
    public void RevealPopUp(){//assign in onpurchase complete
        purchasePopupPanel.SetActive(true);
    }
    public void FailClosePopUp(){ //add leaf to owned value, close pannel
        purchasePopupPanel.SetActive(false);
    }
}
