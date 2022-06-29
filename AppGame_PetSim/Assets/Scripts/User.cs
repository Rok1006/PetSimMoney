using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using System;
//user exp
public class User : MonoBehaviour
{
    public static User Instance;
    //fp == friendship point
    public Slider fp;
    [SerializeField] private Text statusLevelNum;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private Text panelLevelNum;
    [SerializeField] private GameObject levelUpRewardTemplate;
    private LevelRewardData currentLevelRewardData;
    public int fpValue = 10; //current friendship value owned by user, save and load
    public int maxfpValue = 10; //the current max value for slider, save and load
    //private float fpRate; //rate for friendship value increase
    public int level = 0; //level of the user; increase everytime friendship value reaches the max, link to text
                      //Friendship level
    public static int maxLevel = 40;
    public bool beginnerGuide;
    public GameObject BeginnerG;
    public GameObject BGUI;
    public BeginnerGuide BG;
    void Awake() 
    {
        BG = BeginnerG.GetComponent<BeginnerGuide>();
        Instance = this;
         if(User.Instance.beginnerGuide){ //if already finish tutorial
            BGUI.SetActive(false);
            BG.enabled = false;
            BG.DisableAllFingers();
            BG.NoGuide();
        }
    }
    void Start()
    {

    }
    void Update()
    {
        fp.value = fpValue;
        statusLevelNum.text = level.ToString();
        fp.maxValue = maxfpValue;

        //Developer Cheat
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ExpUP(maxfpValue);
        }
    }
    public void ExpUP(int value) //call this function 
    {
        if(level < maxLevel)
        {
            //increase Exp value, put this at diff places
            fpValue += value;

            while(fpValue >= maxfpValue && level != maxLevel)
            {
                Debug.Log("Level: " + level + " max: " + maxfpValue);
                LevelUp();
            }
        }
    }
    public void LevelUp()
    {   //level up

        fpValue =0;
        level++; //increase one level
        maxfpValue = (int)Mathf.Round((float)Math.Log(level)*20+level*0.3f+10);//y=20logx+0.3x+10
        
        if(level >= maxLevel)
        {
            fpValue = 0;
        }

        if(levelUpPanel.activeSelf)
        {
            levelUpPanel.SetActive(false);
        }
        levelUpPanel.SetActive(true);
        panelLevelNum.text = level.ToString();
        foreach(LevelRewardData data in SaveLoadManager.data.levelRewardData)
        {
            if(data.level == level)
            {
                currentLevelRewardData = data;
            }
        }
        RewardManager.Instance.RewardGen(currentLevelRewardData, levelUpRewardTemplate);
    }
}
