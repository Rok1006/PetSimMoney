using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentManager : MonoBehaviour
{

    public enum GarmentType { Hat, Neck, Back }
    public enum Rarity { Common, Rare, SuperRare, UltraRare, SecretUltraRare }
    public static GarmentManager Instance;
    private List<GameObject> _garmentList = new List<GameObject>();
    public List<GameObject> garmentList { get { return _garmentList; } }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _garmentList = new List<GameObject>(Resources.LoadAll<GameObject>("Items/UI"));
    }
}
