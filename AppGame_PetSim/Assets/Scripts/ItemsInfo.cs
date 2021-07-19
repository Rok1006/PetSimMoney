using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place this script in every bought items in invent
public abstract class ItemsInfo : MonoBehaviour
{
    [SerializeField]
    private string _itemID; //It should be same as the name of the prefab
    public string itemID { get { return _itemID; } }
    [SerializeField]
    private string _itemName; //name of the item
    public string itemName { get { return _itemName; } }
    [SerializeField]
    private GameObject _generatedItem; //place real gameobject
    public GameObject generatedItem { get { return _generatedItem; } }
    //public int valueAdded; //value added to cat if consume
    [SerializeField]
    private ItemType _type;
    public ItemType type { get { return _type; } }
    [SerializeField]
    private Rarity _rarity;
    public Rarity rarity { get { return _rarity; } }
    [SerializeField]
    private CostMethod _costMethod;
    public CostMethod costMethod  { get { return _costMethod; } }
    [SerializeField]
    private int _cost;
    public int cost { get { return _cost; } }
    [SerializeField]
    private int _levelRequirement;
    public int levelRequirement { get { return _levelRequirement; } }
    [SerializeField]
    private string _description;
    public string description { get { return _description; } }
    public abstract void Use();
}
