using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place this script in every bought items in invent
public class ItemsInfo : MonoBehaviour
{
    public string itemID;
    public string itemName; //name of the item
    public GameObject GeneratedItem; //place real gameobject
    //public int valueAdded; //value added to cat if consume
    public ItemManager.ItemType type;
    public ItemManager.Rarity rarity;
}
