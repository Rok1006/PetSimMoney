using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public GameObject RewardPopUpPanel;
    public Image rewardImage; //image replace, place the image slot ui 
    public Text rewardText; //place the text ui here
    public Sprite Reward;
    int RewardNum;



    public int mochiScore;
    public int bluScore;

    // Start is called before the first frame update
    void Awake(){
        Instance = this;
    }

    public void RewardImageGenerate(Sprite img){ //add to button
        rewardImage.GetComponent<Image>().sprite = img; 
    }

    public void CollectGift(int num){ //link to collect button
        RewardPopUpPanel.SetActive(true);
        rewardImage.GetComponent<Image>().sprite = Reward; 
        rewardText.GetComponent<Text>().text = "X " + num.ToString();
        
        Status.Instance.LeafChange(CostMethod.GoldLeaf, num);
    }
            






}
