using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum GarmentType { Hat, Neck, Back }

public class WareroomManager : MonoBehaviour
{
    public static WareroomManager Instance;
    public List<GameObject> _garmentList = new List<GameObject>();
    public List<GameObject> garmentList { get { return _garmentList; } }
    public List<List<Garment>> _separatedGarmentLists = new List<List<Garment>>();
    public List<List<Garment>> separatedGarmentLists { get { return _separatedGarmentLists; } }    
    public GameObject[] GarmentUITemplate = new GameObject[3];
    public List<List<GameObject>> garmentUIList = new List<List<GameObject>>();
    public Garment[] currentGarment; 
    public GameObject[] wearing;
    public GameObject[] garmentPlaceHolder = new GameObject[3];
    
    void Awake()
    {
        Instance = this;
        LoadGarmentsToSeperatedLists();
        LoadGarmentsUI();
        currentGarment = new Garment[Enum.GetValues(typeof(GarmentType)).Length];
        wearing = new GameObject[Enum.GetValues(typeof(GarmentType)).Length];
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
                    Garment theGarment = new Garment(garment, type);
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

    public bool OwnGarment(Garment garment)
    {
        int index = separatedGarmentLists[(int) garment.type].IndexOf(garment);
        if(_separatedGarmentLists[(int) garment.type][index].owned)
        {
            return false; //Garment already owned
        }
        else
        {
            _separatedGarmentLists[(int) garment.type][index].owned = true;
            UpdateGarmentUI();
            return true; //Own successfully
        }
    }

    public bool EquipGarment(Garment garment)
    {
        int index = separatedGarmentLists[(int) garment.type].IndexOf(garment);
        if(_separatedGarmentLists[(int) garment.type][index].eqiupped || !_separatedGarmentLists[(int) garment.type][index].owned)
        {
            return false; //Garment already equipped or player do not own it
        }
        else
        {
            UnequipGarment(garment.type);
            _separatedGarmentLists[(int) garment.type][index].eqiupped = true;
            currentGarment[(int) garment.type] = _separatedGarmentLists[(int) garment.type][index];
            GameObject wear = Instantiate(garment.garment.GetComponent<GarmentInfo>().GeneratedItem, garmentPlaceHolder[(int) garment.type].transform) as GameObject;
            wearing[(int) garment.type] = wear;
            UpdateGarmentUI();
            return true; //Equipped successfully
        }
    }

    private void UnequipGarment(GarmentType type)
    {
        foreach(Garment garment in _separatedGarmentLists[(int) type])
        {
            if(garment.eqiupped)
            {
                UnequipGarment(garment);
            }
        }
    }

    private void UnequipGarment(Garment garment)
    {
        if(garment.eqiupped && garment.owned)
        {
            garment.eqiupped = false;
            currentGarment[(int) garment.type] = null;
            DestroyImmediate(wearing[(int) garment.type]);
            UpdateGarmentUI();
        }
    }

    public void OnClickEquipment(){
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string name = button.transform.parent.name;
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            foreach(Garment garment in _separatedGarmentLists[(int) type])
            {
                if(garment.garment.name == name)
                {
                    int index = separatedGarmentLists[(int) garment.type].IndexOf(garment);
                    if(_separatedGarmentLists[(int) garment.type][index].eqiupped)//Garment already equipped
                    {
                        UnequipGarment(garment);
                    }else if(_separatedGarmentLists[(int) garment.type][index].owned)//owned Garment
                    {
                        EquipGarment(garment);
                    }else{break;}
                }
            }
        }
    }

    public void OnClickEquip()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string name = button.transform.parent.name;
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            foreach(Garment garment in _separatedGarmentLists[(int) type])
            {
                if(garment.garment.name == name)
                {
                    EquipGarment(garment);
                }
            }
        }
    }

    public void OnClickUnequip()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string name = button.transform.parent.name;
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            foreach(Garment garment in _separatedGarmentLists[(int) type])
            {
                if(garment.garment.name == name)
                {
                    UnequipGarment(garment);
                }
            }
        }
    }

    public void UpdateGarmentUI()
    {
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            int index = 0;
            foreach(Garment garment in _separatedGarmentLists[(int) type])
            {
                ///////TOOODOOOOOO
                garmentUIList[(int) type][index].transform.Find("GarmentPlaceholder/LockBlock").gameObject.SetActive(
                     !_separatedGarmentLists[(int) type][index].owned);
                garmentUIList[(int) type][index].transform.Find("Equipped").gameObject.SetActive(
                     _separatedGarmentLists[(int) type][index].eqiupped);
                index++;
            }
        }
    }

    public void UpdateWear()
    {
        for(int i = 0; i < 3; i++)
        {
            if(currentGarment[i] != null)
            {
                DestroyImmediate(wearing[i]);
                wearing[i] = Instantiate(currentGarment[i].garment.GetComponent<GarmentInfo>().GeneratedItem,
                    garmentPlaceHolder[(int) currentGarment[i].type].transform) as GameObject;
            }
        }
    }
}

public class Garment
{
    public GameObject garment;
    public GarmentType type;
    public bool eqiupped;
    public bool owned;
    public Garment(GameObject garment, GarmentType type)
    {
        this.garment = garment;
        this.type = type;
        eqiupped = false;
        owned = false;
    }
}
