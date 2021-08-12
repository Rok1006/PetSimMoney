using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    [Header("User.cs")]
    public int level;
    public int maxfpValue;
    public int fpValue;
    public bool beginnerGuide;
    [Header("Status.cs")]
    public float[] statsValue;
    public float[] statsMax;
    public int[] leafC;
    [Header("GarmentManager.cs")]
    public List<string> ownGarment;
    public List<string> currentGarment; 
    [Header("Inventory.cs")]
    public List<string> collection;
    public List<int> amount;
    public List<int> page;
    public List<int> slot;
    [Header("GachaManager.cs")]
    public int luck;
    [Header("AdsManager.cs")]
    public int currentBar;
    public List<bool> blocks;
    public List<bool> buttons;
    [Header("Manager.cs")]
    public bool MusicIsOn;
    public bool SoundIsOn;
    //public int currentBar;
}