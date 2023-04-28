using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public int trashCounter;
    public int greenLeafCounter;
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
    public int[] onDt = {0, 0, 0};
    public int[] quitDt = {0, 0, 0};
    public bool received;
    [Header("Manager.cs")]
    public bool MusicIsOn;
    public bool SoundIsOn;
    [Header("DailyRewardsManager.cs")]
    public int currentday;
    public List<bool> rewardblocks;
    public List<bool> rewardchecks;
    public List<bool> daybuttons;
    public bool collected;
    [Header("SceneryManager.cs")]
    public int currentPlanet;
    [Header("AdventureManager.cs")]
    public List<int> AdventurePlanetList;
    public TimeSpan ts;
    public int planetID;
    public int adventureNUM;
    public bool isExplore;
    [Header("ScoreManager.cs")]
    public int mochiScore;
    public int bluScore;
    
}