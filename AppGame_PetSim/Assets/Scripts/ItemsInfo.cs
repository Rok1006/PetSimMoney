using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place this script in every bought items in invent
public abstract class ItemsInfo : MonoBehaviour
{
    public string itemID; //It should be same as the name of the prefab
    public string itemName; //name of the item
    public GameObject GeneratedItem; //place real gameobject
    //public int valueAdded; //value added to cat if consume
    public ItemManager.ItemType type;
    public ItemManager.Rarity rarity;
    public ItemManager.CostMethod costMethod;
    public int cost;
    public int levelRequirement;
    public abstract void Use();
}
