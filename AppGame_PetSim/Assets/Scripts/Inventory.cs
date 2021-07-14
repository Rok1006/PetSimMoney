using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    //public List<GameObject> Items = new List<GameObject>();
    public GameObject[,] inventorySlot = new GameObject[3,16]; //to put invent slot
    public List<ItemsCollect> collection = new List<ItemsCollect>();
    public bool[] full;  //check if slots are full
    public Vector3 productSize;
    public int fullslot;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fullslot = 0;
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
        for(int page = 0; page < inventorySlot.GetLength(0); page++)
        {
            for(int slot = 0; slot < inventorySlot.GetLength(1); slot++)
            {
                if(SlotIsEmpty(page, slot))
                {
                    AddToSlots(itemCollected, amount, page, slot);
                    break;
                }
            } 
        }
    }
    public void AddToSlots(GameObject itemCollected, int amount, int page, int slot)
    {
        collection.Add(new ItemsCollect(itemCollected, amount, page, slot));
        GameObject item = Instantiate(itemCollected, inventorySlot[page, slot].transform.position, Quaternion.identity)as GameObject; //instanciate items in slot pos
        item.transform.SetParent(inventorySlot[page, slot].transform, false); //must need, make it as a child
        item.transform.localScale = productSize;  //new Vector3(.4f, .5f, .5f);
    }
    public void RemoveFromSlot(int page, int slot)
    {
        for(int i = inventorySlot[page, slot].transform.childCount - 1; i > 0; i--)
        {
            Destroy(inventorySlot[page, slot].transform.GetChild(i).gameObject);
        }
        collection.Remove(collection.Find(x => x.slotPlaced[0] == page && x.slotPlaced[1] == slot));
    }
    private bool SlotIsEmpty(int page, int slot)
    { //check true false if every slot is empty
        return inventorySlot[page, slot].transform.childCount <= 0 ? true : false;
    }
    public int CheckOccupiedSlot()
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
