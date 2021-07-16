using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is for determinging the rise low value of cat, and related slider
public enum StatsType { Happiness, Energy, Hunger, Hydration }

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
    [SerializeField] private float[] statsValue = new float[4]{100, 100, 100, 100};
    [SerializeField] private float[] statsMax = new float[4]{100, 100, 100, 100};
    public float happyV { get { return statsValue[0]; } }
    public float happyMax { get { return statsMax[0]; } }
    public float energyV { get { return statsValue[1]; } }
    public float energyMax { get { return statsMax[1]; } }
    public float hungerV { get { return statsValue[2]; } }
    public float hungerMax { get { return statsMax[2]; } }
    public float hydrationV { get { return statsValue[3]; } }
    public float hydrationMax { get { return statsMax[3]; } }
    [Header("ItemsCount")]
    public int rareC = 0; //owned rare leaf
    public int normalC = 0; //owned normal leaf
    private static float decreaseRate = 0.00173611f; // 16 hours = 57600 seconds -> 100 / 57600 = 0.00173611% / second
    void Awake() 
    {
        Instance = this;
        InvokeRepeating("SteadyDecrease", 0.0f, 1f);
    }
 
    void Start()
    {

    }

    void Update()
    {
        happiness.value = happyV / happyMax;
        energy.value = energyV / energyMax;
        hunger.value = hungerV / hungerMax;
        hydration.value = hydrationV / hydrationMax;

        UpdateValue();

        DevCheat();
    }
    void SteadyDecrease()
    { 
        for(int i = 0; i < statsValue.Length; i++)
        {
            if(statsValue[i] > 0) 
            {
                statsValue[i] -= decreaseRate;
            }
        }
    }
    public bool StatsChange(StatsType type, float value) //return ture if the value added successfully
    {
        int typeNum = (int) type;
        if(value < 0) //Decrease
        {
            if(statsValue[typeNum] + value <= 0) //stat will decrease over 0
            {
                statsValue[typeNum] = 0;
            }
            else
            {
                statsValue[typeNum] += value;
            }
            return true;
        }
        else //Increase
        {
            if(statsValue[typeNum] >= statsMax[typeNum]) //stat is full
            {
                return false; //cannot increase
            }
            else if(statsValue[typeNum] + value >= statsMax[typeNum]) //stat will increase over Max
            {
                statsValue[typeNum] = statsMax[typeNum];
            }
            else
            {
                statsValue[typeNum] += value;
            }
            return true;
        }
    }

    public void UpdateValue(){
        norT.text = normalC.ToString();
        rareT.text = rareC.ToString();
        Store_norT.text = normalC.ToString();
        Store_rarT.text = rareC.ToString();
        Gacha_norT.text = normalC.ToString();
        Gacha_rarT.text = rareC.ToString();
    }
    //To do:
    //- if trash in the scene is larger than certain amount the happy value lower in a slightly faster way, other wise its normal speed
    void DevCheat(){
        if(Input.GetKey(KeyCode.O))
        {
            normalC += 1;
        }
        if(Input.GetKey(KeyCode.P))
        {
            rareC += 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            for(int i = 0; i < statsValue.Length; i++)
        {
            if(statsValue[i] > 0) 
            {
                statsValue[i] -= 1f;
            }
        }
        }
    }
}
