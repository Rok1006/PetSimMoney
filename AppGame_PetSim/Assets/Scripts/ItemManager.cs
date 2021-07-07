using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType { Food, Drink, Toy, Special }
    public enum Rarity { Common, Rare, SuperRare, UltraRare, SecretUltraRare }
    public static ItemManager Instance;
    private List<GameObject> _itemList = new List<GameObject>();
    public List<GameObject> itemList { get { return _itemList; } }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _itemList = new List<GameObject>(Resources.LoadAll<GameObject>("Items/Objs"));
    }
}
