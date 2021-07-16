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
    Animator valueAnim;

    [SerializeField]
    private GameObject shopDrinkTemplates, shopFoodTemplates, shopToyTemplates, shopLeafTemplates;
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
                        ShopItemCreate(item, shopDrinkTemplates);
                        break;
                    case ItemType.Food:
                        ShopItemCreate(item, shopFoodTemplates);
                        break;
                    case ItemType.Toy:
                        ShopItemCreate(item, shopToyTemplates);
                        break;
                    case ItemType.Leaf:
                        ShopItemCreate(item, shopLeafTemplates);
                        break;
                }
            }
        }
    }

    private void ShopItemCreate(ShopItem shopItem, GameObject template)
    {
        GameObject newitem = Instantiate(template) as GameObject;

        newitem.name = shopItem.item.name;
        newitem.GetComponent<Image>().sprite = shopItem.item.GetComponent<Image>().sprite;
        newitem.transform.Find("Price").gameObject.GetComponentInChildren<Text>().text = shopItem.cost.ToString();
        if(visible)
        {
            newIcon.SetActive(true);
        }
        newIcon.transform.SetParent(rewardIconTemplate.transform.parent, false);
        rewardIconList.Add(newIcon);
        Count++;
    }

        int count = 0;
        foreach(string name in data.name)
        {
            GameObject reward = ItemManager.Instance.itemList.Find(x => x.name.Contains(name));
            addReward(new Reward(reward, data.amount[count]));
            count++;
        }
        giveReward();
        RewardUIGen(template);
        foreach(GameObject item in ItemManager.Instance.separatedItemLists[(int) ItemType.Drink])
        {
            
            //Gen + Info
        }
    }

    public void ClickBuyItem()
    { //pay with normleaf
        selectedButton = EventSystem.current.currentSelectedGameObject;


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
        if(Status.Instance.rareC>=cost && canBuy)
        { //cost = 1, result: 3 drinks
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
