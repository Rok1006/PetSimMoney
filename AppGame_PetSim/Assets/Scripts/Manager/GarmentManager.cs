using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GarmentType { Hat, Neck, Back }

public class GarmentManager : MonoBehaviour
{
    public static GarmentManager Instance;
    private List<GameObject> _garmentList = new List<GameObject>();
    public List<GameObject> garmentList { get { return _garmentList; } }
    private List<List<Garment>> _separatedGarmentLists = new List<List<Garment>>();
    public List<List<Garment>> separatedGarmentLists { get { return _separatedGarmentLists; } }    
    public GameObject[] GarmentUITemplate = new GameObject[3];
    public List<List<GameObject>> garmentUIList = new List<List<GameObject>>();
    void Awake()
    {
        Instance = this;
        LoadGarmentsToSeperatedLists();
        LoadGarmentsUI();
    }

    private void LoadGarmentsToSeperatedLists()
    {
        foreach(int type in Enum.GetValues(typeof(GarmentType)))
        {
            _separatedGarmentLists.Add(new List<Garment>());
        }

        _garmentList = new List<GameObject>(Resources.LoadAll<GameObject>("Garments/UI"));
        Sorting.QuickSortGarment(_garmentList, 0, _garmentList.Count - 1);
        foreach(GameObject garment in garmentList)
        {
            foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
            {
                if(garment.GetComponent<GarmentInfo>().type == type)
                {
                    Garment theGarment = new Garment(garment);
                    _separatedGarmentLists[(int) type].Add(theGarment);
                    break;
                }
            }
        }
    }

    private void LoadGarmentsUI()
    {
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            garmentUIList.Add(new List<GameObject>());
            foreach(Garment garment in _separatedGarmentLists[(int) type])
            {
                GameObject newIcon = Instantiate(GarmentUITemplate[(int) type]) as GameObject;
                newIcon.name = garment.garment.name;
                newIcon.transform.Find("GarmentPlaceholder").GetComponent<Image>().sprite = garment.garment.GetComponent<Image>().sprite;
                newIcon.transform.Find("Name").GetComponentInChildren<Text>().text = garment.garment.GetComponent<GarmentInfo>().itemName;
                newIcon.SetActive(true);
                
                newIcon.transform.SetParent(GarmentUITemplate[(int) type].transform.parent, false);
                garmentUIList[(int) type].Add(newIcon);
            }
        }
    }
}

public class Garment
{
    public GameObject garment;
    public bool eqiuped;
    public bool own;
    public Garment(GameObject garment)
    {
        this.garment = garment;
        eqiuped = false;
        own = false;
    }
}
