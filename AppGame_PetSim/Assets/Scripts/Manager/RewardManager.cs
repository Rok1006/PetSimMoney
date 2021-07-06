using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//determeine levelup panel UI text change
//determine what items to be gifted

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    private GameObject rewardTemplate;
    private List<Reward> rewardList = new List<Reward>();

    void Awake() {
        Instance = this;
    }
    void Update()
    {

    }

    public void RewardGen(GameObject template)
    {
        rewardTemplate = template;
    }
}

public class Reward
{
    public GameObject item;
    public int amount;

    public Reward(GameObject reward, int amount)
    {
        item = reward;
        this.amount = amount;
    }
}
