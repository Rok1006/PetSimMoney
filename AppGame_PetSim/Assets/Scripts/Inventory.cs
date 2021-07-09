using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    //public List<GameObject> Items = new List<GameObject>();
    public GameObject[] inventorySlot; //to put invent slot
    public bool add;
    public GameObject purchasedItem;
    public bool[] full;  //check if slots are full
    public Vector3 productSize;
    public int fullslot;
    void Awake(){
        instance = this;
    }
    void Start()
    {
        fullslot = 0;
    }
    void Update()
    {
        if(add){ //called in BUy script
            AddToSlots();
            add = false;
        }
        CheckNull(); //check if all the slots is null
        //CheckAllFull();
    }
    public void AddToSlots(){
        Debug.Log("good");
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            //if(inventorySlot[i] != null){ //not appearing
             if(full[i] == false){
                   full[i] = true; //true if there is sth in it
                GameObject item = Instantiate(purchasedItem, inventorySlot[i].transform.position, Quaternion.identity)as GameObject; //instanciate items in slot pos
                item.transform.parent = inventorySlot[i].transform; //must need, make it as a child
                item.transform.localScale = productSize;  //new Vector3(.4f, .5f, .5f);
                Debug.Log(inventorySlot[i]); 
                break;
             } 
            }
    }
    void CheckNull(){ //check true false if every slot is empty
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if(inventorySlot[i].transform.childCount < 1)
            {
                full[i] = false;
            }
             if(inventorySlot[i].transform.childCount > 0)
            {
                full[i] = true;
            }
        }
    }
    public void CheckAllFull(){ //check if slots are full
          for (int i = 0; i < inventorySlot.Length; i++)
        {
            if(full[i] == true){
                fullslot += 1;
            }
            break;
        }
        if(fullslot>14){ //change value if have more slots
            Debug.Log("all full");
            Buy.Instance.canBuy = false;
        }else{
            Buy.Instance.canBuy = true;
        }
    }
}
