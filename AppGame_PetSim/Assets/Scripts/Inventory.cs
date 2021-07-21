using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public static int pageNum = 2;
    public static int slotNum = 16;
    public static Vector3 productSize = new Vector3(0.7f, 0.7f, 0.7f);
    //public List<GameObject> Items = new List<GameObject>();
    public GameObject[] slotHolders = new GameObject[pageNum];
    public GameObject[,] inventorySlot = new GameObject[pageNum,slotNum]; //to put invent slot
    public List<ItemsCollect> collection = new List<ItemsCollect>();
    //public int fullslot;
    private int slotCapacity = 99;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //fullslot = 0;
        for(int page = 0; page < pageNum; page++)
        {
            for(int slot = 0; slot < slotNum; slot++)
            {
                inventorySlot[page, slot] = slotHolders[page].transform.GetChild(slot).gameObject;
            }
        }
    }
    void Update()
    {
        // if(add)
        // { //called in BUy script
        //     AddToSlots();
        //     add = false;
        // }
        //CheckNull(); //check if all the slots is null
        //CheckAllFull();
    }
    public void AddToSlots(GameObject itemCollected, int amount)
    {
        foreach(ItemsCollect item in collection)
        {
            if(item.item.GetComponent<ItemsInfo>().itemID == itemCollected.GetComponent<ItemsInfo>().itemID &&
               item.amount < slotCapacity) //Add to same item if the slot is not full, exceed amount does not matter
            {
                Debug.Log("A " + item.slotPlaced[0].ToString() + " " + item.slotPlaced[1].ToString());
                AddToSlots(itemCollected, amount, item.slotPlaced[0], item.slotPlaced[1]);
                return;
            }
        }

        for(int page = 0; page < inventorySlot.GetLength(0); page++)
        {
            for(int slot = 0; slot < inventorySlot.GetLength(1); slot++)
            {
                if(SlotIsEmpty(page, slot))
                {
                    Debug.Log("B: " + page.ToString() + " " + slot.ToString());
                    AddToSlots(itemCollected, amount, page, slot);
                    return;
                }
            } 
        }
    }
    public void AddToSlots(GameObject itemCollected, int amount, int page, int slot)
    {
        ItemsCollect occupiedItem = GetSlotObject(page, slot);
        if(occupiedItem != null) //Add to occupied slot
        {
            if(occupiedItem.item.GetComponent<ItemsInfo>().itemID == itemCollected.GetComponent<ItemsInfo>().itemID)
            {
                if(occupiedItem.amount + amount <= slotCapacity) //Can add amount
                {
                    occupiedItem.amount += amount;
                }
                else //amount exceeded
                {
                    int newAmount = amount - (slotCapacity - occupiedItem.amount);
                    occupiedItem.amount = slotCapacity;
                    AddToSlots(itemCollected, newAmount);
                }
                occupiedItem.item.transform.Find("Text").gameObject.GetComponent<Text>().text = "X " + occupiedItem.amount;
            }
            else //Different item
            {
                //TODO
                
            }
        }
        else //Add to empty slot
        {
            GameObject item = Instantiate(itemCollected, inventorySlot[page, slot].transform.position, Quaternion.identity)as GameObject; //instanciate items in slot pos
            collection.Add(new ItemsCollect(item, amount, page, slot));
            item.transform.Find("Text").gameObject.GetComponent<Text>().text = "X " + amount.ToString();
            //item.transform.SetParent(inventorySlot[page, slot].transform, false); //must need, make it as a child
            inventorySlot[page, slot].GetComponent<EquipmentSlot>().Initialize(item.GetComponent<DragDrop>());
            item.transform.localScale = productSize;  //new Vector3(.4f, .5f, .5f);
        }
    }
    public void RemoveFromSlot(int page, int slot)
    {
        for(int i = inventorySlot[page, slot].transform.childCount - 1; i > 0; i--)
        {
            if(!SlotIsEmpty(page, slot))
            {
                Destroy(inventorySlot[page, slot].transform.GetChild(i).gameObject);
            }
        }
        collection.Remove(collection.Find(x => x.slotPlaced[0] == page && x.slotPlaced[1] == slot));
    }
    private bool SlotIsEmpty(int page, int slot) //Simply Check emptyness
    { //check true false if every slot is empty
        return inventorySlot[page, slot].transform.childCount <= 0 ? true : false;
    }
    private ItemsCollect GetSlotObject(int page, int slot) //Check emptyness and return the item in slot
    {
        foreach(ItemsCollect item in collection)
        {
            Debug.Log(item.slotPlaced[0].ToString() + " " + item.slotPlaced[1].ToString());
            Debug.Log(page + " " + slot);
            if(item.slotPlaced[0] == page && item.slotPlaced[1] == slot)
            {
                Debug.Log("return successfully");
                return item;
            }
        }
        return null;
    }
    public int CheckOccupiedSlotNum()
    { //check and return the number of occupied slot
        int countOccupied = 0;
        for(int page = 0; page < inventorySlot.GetLength(0); page++)
        {
            for(int slot = 0; slot < inventorySlot.GetLength(1); slot++)
            {
                if(!SlotIsEmpty(page, slot))
                {
                    countOccupied++;
                    break;
                }
            } 
        }
        return countOccupied;
    }
}

public class ItemsCollect
{
    public GameObject item;
    public int amount;
    public int[] slotPlaced = new int[2];
    public ItemsCollect(GameObject item, int amount, int page, int slot)
    {
        this.item = item;
        this.amount = amount;
        this.slotPlaced = new int[]{page, slot};
    }
} 
