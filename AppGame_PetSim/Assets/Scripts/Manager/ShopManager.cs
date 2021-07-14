using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    Animator bAnim;
    public GameObject valueHolder;
    Animator valueAnim;
    public int cost; //determine the cost of every item, indicate this in the inspector, each is different
    public GameObject item; //assign the product in inspector
    public Vector3 size; //not using currently
    public bool canBuy = true;
    void Awake(){
        Instance = this;
    }
    void Start()
    {
         bAnim = this.GetComponent<Animator>();
        valueAnim = valueHolder.GetComponent<Animator>();
         Debug.Log(Status.Instance.normalC);
    }
    void Update()
    {
        
    }
    // public void ClickBuyWaterA01C(){
    //     if(Status.Instance.normalC>=1){ //cost = 1, result: 3 drinks
    //         bAnim.SetTrigger("buy");
    //         valueAnim.SetTrigger("norm");
    //         Status.Instance.normalC -=1;
    //     }
    // }
    public void ClickBuyNormalItem(GameObject item){ //pay with normleaf
        if(Status.Instance.normalC>=cost && canBuy){ //cost = 1, result: 3 drinks
            bAnim.SetTrigger("buy");
            valueAnim.SetTrigger("norm");
            Status.Instance.normalC -= cost;
            //Inventory.instance.AddToSlots(item) //turn true, will turn false after instanciate
            Inventory.instance.productSize = size;
            Manager.Instance.boughtItems += 1;
            //Inventory.instance.CheckAllFull();
        }
    }
    public void ClickBuyRareItem(){ //pay with normleaf
        if(Status.Instance.rareC>=cost && canBuy){ //cost = 1, result: 3 drinks
            bAnim.SetTrigger("buy");
            valueAnim.SetTrigger("rare");
            //invent.add == true?
            Status.Instance.rareC -= cost;
            //Inventory.instance.purchasedItem = item;
            Inventory.instance.productSize = size;
            Manager.Instance.boughtItems += 1;
            //Inventory.instance.CheckAllFull();
        }
    }
   

}
