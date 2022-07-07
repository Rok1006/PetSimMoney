using System;
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
                    case ItemType.spItem:
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
        ItemsInfo info = shopItem.item.GetComponent<ItemsInfo>();

        newItem.GetComponent<ObjectReference>().referencedObj = shopItem.item;
        newItem.name = info.itemID;
        newItem.transform.Find("Product").gameObject.GetComponent<Image>().sprite = shopItem.item.GetComponent<Image>().sprite;
        newItem.transform.Find("Title").gameObject.GetComponent<Text>().text = info.itemName;
        newItem.transform.Find("Descript").gameObject.GetComponent<Text>().text = info.description;
        newItem.transform.Find("Price").gameObject.GetComponentInChildren<Text>().text = shopItem.cost.ToString();
        GameObject lockBlock = newItem.transform.Find("LockBlock").gameObject;
        lockBlock.transform.Find("Lv").gameObject.GetComponent<Text>().text = "LV" + info.levelRequirement.ToString();
        if(User.Instance.level >= info.levelRequirement)
        {
            lockBlock.SetActive(false);
        }
        else
        {
            lockBlock.SetActive(true);
        }
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

        if(Status.Instance.LeafChange(costMethod, -info.cost))
        {
            SoundManager.Instance.Buy();
            ////////ADD ITEM TO INVENTORY
            Inventory.instance.AddToSlots(item, 1); //Current amount of all item is 1
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
