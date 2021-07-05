using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place this script in every bought items in invent
public class ItemsInfo : MonoBehaviour
{
    public string itemID;
    public string itemName; //name of the item
    public GameObject GeneratedItem; //place real gameobject
    public int valueAdded; //value added to cat if consume
    public static ItemsInfo Instance;
    void Awake() {
        Instance = this;
    }
  
    void Start()
    {
        
    }
    void Update()
    {
    }
    public void UseWaterC0(){ //TypeC0 water
        Debug.Log("using");
    }
    public void UseFoodC1(){ //TypeC1 water
      
    }
    public void UseToyC2(){ //TypeC1 water
      
    }
}
