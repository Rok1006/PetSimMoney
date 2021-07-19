using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place this in every garment, info 
public class GarmentInfo : MonoBehaviour
{
    public string itemID;
    public string itemName; //name of the item
    public GameObject GeneratedItem; //place real gameobject of the costumes
    public GarmentType type;
    public Rarity rarity;
    public GameObject HeadPosition;
    public GameObject NeckPosition;
    public GameObject BackPosition;
    void Awake() {
        HeadPosition = GameObject.Find("HeadDecoration");
        NeckPosition = GameObject.Find("NeckDecoration");
        BackPosition = GameObject.Find("BackDecoration");
    }
}
