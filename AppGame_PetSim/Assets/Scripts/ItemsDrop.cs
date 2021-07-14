using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//drop item only when walking
public class ItemsDrop : MonoBehaviour
{
    public static ItemsDrop Instance;
   public GameObject itemEmitSpot;
   public GameObject NormalItem; //prefab, ID = 1
   public GameObject RareItem; //prefab  ID = 2
   public GameObject Trash; //prefab  ID = 0
   int finalprobability;
   int itemID;
   int numOfRare;
   int numOfNor;
   int numOfTrash;
   public int totalItemDropped;
   public int totalTrashDropped;
 
   void Awake() {
       Instance = this;
   }
    void Start()
    { 
        InvokeRepeating("Generator",0,7);
    }

    void Update()
    {
        CheckDroppedItem();
    }
    void CheckDroppedItem(){
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        numOfTrash = trash.Length;
        GameObject[] nor = GameObject.FindGameObjectsWithTag("Normal");
        numOfNor = nor.Length;
        GameObject[] rare = GameObject.FindGameObjectsWithTag("Rare");
        numOfRare = rare.Length;
        //------
        totalItemDropped = numOfRare+ numOfNor + numOfTrash;
        totalTrashDropped = numOfTrash;
    }
    //check total num, generate if less than ?? num
    void Generator(){
        if(CatAI.Instance.action == CatAI.Action.Walking && totalItemDropped <20){ //drop items only when walking and total less than 20, can change
            //Debug.Log("dropped");
            ItemProbability();
            if(itemID==0){
                GameObject i = Instantiate(Trash, itemEmitSpot.transform.position, Quaternion.identity);
            }else if(itemID==1){
                GameObject i = Instantiate(NormalItem, itemEmitSpot.transform.position, Quaternion.identity);
            }else if(itemID==2){
                GameObject i = Instantiate(RareItem, itemEmitSpot.transform.position, Quaternion.identity);
            }
        }
    }

    void ItemProbability(){
        finalprobability = Random.Range(1,101); //full 100
        if(finalprobability<10){ //10%
            itemID = 0;
        }else if(finalprobability>=10 && finalprobability<95){ //85%
            itemID = 1;
        }else if(finalprobability>=95){ //5%
            itemID = 2;
        }
    }
}
