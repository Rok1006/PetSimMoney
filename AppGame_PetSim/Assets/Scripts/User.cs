using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//user exp
public class User : MonoBehaviour
{
    public static User Instance;
    //fp == friendship point
    public Slider fp;
    public Text levelNum;
    public int fpValue; //current friendship value owned by user, save and load
    public int maxfpValue; //the current max value for slider, save and load
    //private float fpRate; //rate for friendship value increase
    public int level; //level of the user; increase everytime friendship value reaches the max, link to text
                      //Friendship level
    void Awake() 
    {
        Instance = this;
    }
    void Start()
    {
        fpValue = 0;
        maxfpValue = 10; //first max is 10, will increase
        //fpRate = 5; //temp
        level = 1;
    }
    void Update()
    {
        fp.value = fpValue;
        levelNum.text = level.ToString();
        fp.maxValue = maxfpValue;

        //Developer Cheat
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ExpUP(maxfpValue);
        }
    }
    public void ExpUP(int value)
    {
        //increase Exp value, put this at diff places
        fpValue += value;

        while(fpValue >= maxfpValue)
        {
            Debug.Log("Level: " + level + "max: " + maxfpValue);
            LevelUp();
        }
    }
    public void LevelUp()
    { //level up
        LevelUpManager.Instance.LevelUpPanel.SetActive(true);
        fpValue -= maxfpValue;
        level += 1; //increase one level
        maxfpValue = 10 * (int)Mathf.Round(Mathf.Pow(level, 1.5f));
        maxfpValue -= maxfpValue % 10;
    }
}
