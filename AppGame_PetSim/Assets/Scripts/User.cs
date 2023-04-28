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
    public Slider fpBar;
    [SerializeField] private Text statusLevelNum;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private Text panelLevelNum;
    [SerializeField] private GameObject levelUpRewardTemplate;
    private LevelRewardData currentLevelRewardData;
    public int fpValue; //current friendship value owned by user, save and load
    public int maxfpValue; //the current max value for slider, save and load
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
        if(level >= 1){
            Manager.Instance.outdoorbutton.SetActive(true);
        }else{
            Manager.Instance.outdoorbutton.SetActive(false);
        }
        statusLevelNum.text = level.ToString();

        fpBar.value = fpValue;
        fpBar.maxValue = maxfpValue;
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
        maxfpValue = (int)Mathf.Round(2f *level *level + 20f);//y=x^2+20
        
        if(level >= 10){
            Manager.Instance.outdoorbutton.SetActive(true);
        }else{
            Manager.Instance.outdoorbutton.SetActive(false);
        }

        if(level >= maxLevel)
        {
            fpValue = 0;
        }

        if(levelUpPanel.activeSelf)
        {
            levelUpPanel.SetActive(false);
        }
        levelUpPanel.SetActive(true);
        SoundManager.Instance.lvUP();
        panelLevelNum.text = level.ToString();
        foreach(LevelRewardData data in SaveLoadManager.data.levelRewardData)
        {
            if(data.level == level)
            {
                if(data != null){
                    currentLevelRewardData = data;
                }
                
            }
        }
        RewardManager.Instance.RewardGen(currentLevelRewardData, levelUpRewardTemplate);
    }
}
