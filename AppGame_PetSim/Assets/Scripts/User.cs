using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//user exp
public class User : MonoBehaviour
{
    public static User Instance;
    public Slider exp;
    public Text levelNum;
    public int expValue;
    public int MaxValue; //the current max value for slider, save and load
    private int interval; //rate for exp increase
    public int currentExp; //current exp owned by user, save and load
    public int Level; //level of the user; increase everytime exp reaches the max, link to text
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        currentExp = 0;
        MaxValue = 10; //first max is 10, will increase
        interval = 5; //temp
        Level = 1;
    }
    void Update()
    {
        exp.value = currentExp;
        levelNum.text = Level.ToString();
        exp.maxValue = MaxValue;
        if(currentExp==MaxValue){
            LevelUpManager.Instance.LevelUpPanel.SetActive(true);
            LevelUp();
            MaxExpIncrease();
            currentExp = 0;
        }

        //Developer Cheat
        if(Input.GetKeyDown(KeyCode.Space)){
            ExpUP();
        }
    }
    public void ExpUP(){ //increase Exp value, put this at diff places
        if(currentExp<MaxValue){
            currentExp+=5;
        }
    }
    public void LevelUp(){ //level up
        Level += 1; //increase one level
    }
    public void MaxExpIncrease(){
        if(MaxValue<100){
          MaxValue +=5;   
        }
       
    }


}
