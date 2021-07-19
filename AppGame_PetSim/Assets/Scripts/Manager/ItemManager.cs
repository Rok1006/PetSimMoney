using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Drink, Food, Toy, Leaf}
public enum Rarity { Common, Rare, SuperRare, UltraRare, SecretUltraRare }
public enum CostMethod { GreenLeaf, GoldLeaf, Cash }

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    private List<GameObject> _itemList = new List<GameObject>();
    public List<GameObject> itemList { get { return _itemList; } }
    private List<List<GameObject>> _separatedItemLists = new List<List<GameObject>>();
    public List<List<GameObject>> separatedItemLists { get { return _separatedItemLists; } }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        LoadItemsToSeperatedLists();

    }
    private void LoadItemsToSeperatedLists()
    {
        foreach(int type in Enum.GetValues(typeof(ItemType)))
        {
            _separatedItemLists.Add(new List<GameObject>());
        }

        _itemList = new List<GameObject>(Resources.LoadAll<GameObject>("Items/UI"));
        foreach(GameObject item in itemList)
        {
            foreach(ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                if(item.GetComponent<ItemsInfo>().type == type)
                {
                    _separatedItemLists[(int) type].Add(item);
                    break;
                }
            }
        }

        //// CheckList
        // int index = 0;
        // foreach(List<GameObject> list in _separatedItemLists)
        // {
        //     Debug.Log(index.ToString() + ":");
        //     foreach(GameObject item in list)
        //     {
        //         Debug.Log(item.GetComponent<ItemsInfo>().itemName);
        //     }
        //     index++;
        // }
    }
}
