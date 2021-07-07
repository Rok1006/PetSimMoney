using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script is for determinging the rise low value of cat, and related slider
public class Status : MonoBehaviour
{
    public static Status Instance;
    [Header("UI")]
    public Slider happiness;
    public Slider energy;
    public Slider hunger;
    public Slider hydration;
    public Text norT;
    public Text rareT;
    public Text Store_norT;
    public Text Store_rarT;
    public Text Gacha_norT;
    public Text Gacha_rarT;
    [Header("StatusValue")]
    public float happyV;
    public float energyV;
    public float hungerV;
    public float hydrationV;
    [Header("ItemsCount")]
    public int rareC; //owned rare leaf
    public int normalC; //owned normal leaf
    public bool valueUP;
    void Awake() {
        Instance = this;
    }
 
    void Start()
    {
        happyV = 100;
        energyV = 100;
        hungerV = 100;
        hydrationV = 100;
        //----Owned Value
        rareC = 0;
        normalC = 0;
        valueUP = false;
    }

    void Update()
    {
        happiness.value = happyV;
        energy.value = energyV;
        hunger.value = hungerV;
        hydration.value = hydrationV;

        //SteadyDecrease();
        norT.text = normalC.ToString();
        rareT.text = rareC.ToString();
        Store_norT.text = normalC.ToString();
        Store_rarT.text = rareC.ToString();
        Gacha_norT.text = normalC.ToString();
        Gacha_rarT.text = rareC.ToString();

        DevCheat();
        
    }
    void SteadyDecrease(){ //0.001f
    if(happyV<100 || energyV < 100 || hungerV < 100 || hydrationV <100)
        happyV -= 0.01f;
        energyV -= 0.01f;
        hungerV -= 0.01f;
        hydrationV -= 0.01f;
    }
    public void HappinessIncrease(){
        if(happyV<100 && valueUP){
            happyV += 3f;
        }
    }
     public void HappinessDecrease(){
        if(happyV>0 ){
            happyV -= 0.01f;
        }
    }
    public void HungerIncrease(){
        if(hungerV<100){
            hungerV += 3f;
        }
    }
     public void HungerDecrease(){
        if(hungerV>0){
            hungerV -= 0.01f;
        }
    }
    public void HydrationIncrease(){
        if(hydrationV<100){
            hydrationV += 3f;
        }
    }
     public void HydrationDecrease(){
        if(hydrationV>0){
            hydrationV -= 0.01f;
        }
    }
    public void EnergyIncrease(){
        if(energyV<100){
            energyV += .05f;
        }
    }
      public void EnergyDecrease(){
        if(energyV>0){
            energyV -= 0.01f;
        }
    }

    public void UpdateValue(){
        norT.text = normalC.ToString();
        rareT.text = rareC.ToString();
        Store_norT.text = normalC.ToString();
        Store_rarT.text = rareC.ToString();
    }
    //To do:
    //- if trash in the scene is larger than certain amount the happy value lower in a slightly faster way, other wise its normal speed
    void DevCheat(){
        if(Input.GetKey(KeyCode.O)){
            normalC += 1;
        }else if(Input.GetKey(KeyCode.P)){
            rareC += 1;
        }
    }
}
