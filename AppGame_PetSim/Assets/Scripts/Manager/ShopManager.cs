﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public GameObject valueHolder;
    public Animator valueAnim;

    [SerializeField]
    private GameObject shopDrinkTemplates, shopFoodTemplates, shopToyTemplates;
    //TODO
    //private GameObject shopLeafTemplates;
    private List<List<GameObject>> shopItemUIList = new List<List<GameObject>>();
    private List<List<ShopItem>> _shopItemList = new List<List<ShopItem>>();
    public List<List<ShopItem>> shopItemList { get { return _shopItemList; } }
    private GameObject selectedButton;

    //public int cost; //determine the cost of every item, indicate this in the inspector, each is different
    //public GameObject item; //assign the product in inspector
    //public bool canBuy = true;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        valueAnim = valueHolder.GetComponent<Animator>();
        ShopItemsGen();
    }
    void Update()
    {
        
    }

    public void ShopItemsGen()
    {
        _shopItemList.Clear(); //clear old list
        foreach(int type in Enum.GetValues(typeof(ItemType)))
        {
            _shopItemList.Add(new List<ShopItem>());
            foreach(GameObject item in ItemManager.Instance.separatedItemLists[(int) type])
            {
                ItemsInfo info = item.GetComponent<ItemsInfo>();
                _shopItemList[(int) type].Add(new ShopItem(item, info.cost, info.description, User.Instance.level >= info.levelRequirement));
            }
        }
        ShopItemUIGen();
    }

    private void ShopItemUIGen()
    {
        foreach (List<GameObject> items in shopItemUIList)
        {
            foreach (GameObject item in items) //clear old list
            {
                Destroy(item);
            }
        }
        shopItemUIList.Clear();

        foreach(int type in Enum.GetValues(typeof(ItemType)))
        {
            shopItemUIList.Add(new List<GameObject>());
            foreach(ShopItem item in shopItemList[(int) type])
            {
                switch((ItemType) type)
                {
                    case ItemType.Drink:
                        ShopItemCreate(item, shopDrinkTemplates, type);
                        break;
                    case ItemType.Food:
                        ShopItemCreate(item, shopFoodTemplates, type);
                        break;
                    case ItemType.Toy:
                        ShopItemCreate(item, shopToyTemplates, type);
                        break;
                    case ItemType.Leaf:
                        //TODO
                        //ShopItemCreate(item, shopLeafTemplates, type);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void ShopItemCreate(ShopItem shopItem, GameObject template, int typeIndex)
    {
        GameObject newItem = Instantiate(template) as GameObject;

        newItem.GetComponent<ObjectReference>().referencedObj = shopItem.item;
        newItem.name = shopItem.item.GetComponent<ItemsInfo>().itemID;
        newItem.transform.Find("Product").gameObject.GetComponent<Image>().sprite = shopItem.item.GetComponent<Image>().sprite;
        newItem.transform.Find("Title").gameObject.GetComponentInChildren<Text>().text = shopItem.item.GetComponent<ItemsInfo>().itemName;
        newItem.transform.Find("Descript").gameObject.GetComponentInChildren<Text>().text = shopItem.item.GetComponent<ItemsInfo>().description;
        newItem.transform.Find("Price").gameObject.GetComponentInChildren<Text>().text = shopItem.cost.ToString();
        newItem.SetActive(true);
        newItem.transform.SetParent(template.transform.parent, false);
        shopItemUIList[typeIndex].Add(newItem);
    }

    public void OnClickBuyItem()
    { //pay with normleaf
        selectedButton = EventSystem.current.currentSelectedGameObject;
        GameObject item = selectedButton.transform.parent.gameObject.GetComponent<ObjectReference>().referencedObj;
        ItemsInfo info = item.GetComponent<ItemsInfo>();
        CostMethod costMethod = info.costMethod;

        if(Status.Instance.LeafChange(costMethod, info.cost))
        {
            //TODO
            ////////ADD ITEM TO INVENTORY
            Manager.Instance.boughtItems += 1;

            selectedButton.GetComponent<Animator>().SetTrigger("buy");
            valueAnim.SetTrigger("norm");

            Debug.Log("Item bought: " + info.itemName + " (" + info.itemID + ")");
        }
        else
        {
            Debug.Log("Not enough leaf");
        }
    }
}

public class ShopItem
{
    public GameObject item; //assign the product in inspector
    public int cost; //determine the cost of every item, indicate this in the inspector, each is different
    public string description;
    public bool canBuy = true;
    public ShopItem(GameObject item, int cost, string description ,bool canBuy)
    {
        this.item = item;
        this.cost = cost;
        this.description = description;
        this.canBuy = canBuy;
    }
}