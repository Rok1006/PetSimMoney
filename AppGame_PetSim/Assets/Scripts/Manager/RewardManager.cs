using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//determeine levelup panel UI text change
//determine what items to be gifted

public class RewardManager : MonoBehaviour
{
    public enum PoolType { Common, Rare } 
    public static RewardManager Instance; 
    private GameObject rewardTemplate;
    private List<GameObject> pool = new List<GameObject>();
    private List<Reward> _rewardList = new List<Reward>();
    public List<Reward> rewardList { get { return _rewardList; } }

    public int luck = 0;
    private float[] _chances = {0.05f, 0.285f, 0.7f};
    public float[] chances { get { return _chances; } }

    void Awake() 
    {
        Instance = this;
    }
    void Update()
    {
        
    }

    public void RewardGen(GameObject template)
    {
        rewardTemplate = template;
    }

    public void ChancesUpdate()
    {
        if(luck <= 1000)
        {
            _chances[0] = Mathf.Round((0.05f + (luck * 0.00005f)) * 1000.0f) / 1000.0f;
        }
        else
        {
            _chances[0] = Mathf.Round((0.1f + (luck * 0.0009f)) * 1000.0f) / 1000.0f;
        }
        _chances[1] = Mathf.Floor(((1.0f - _chances[0]) * 3.0f / 10.0f) * 1000.0f) / 1000.0f;
        _chances[2] = 1.0f - _chances[0] - _chances[1];
    }

    public void Shuffling(ItemManager.ItemType type, PoolType pool)
    {
        foreach(GameObject item in ItemManager.Instance.itemList)
        {
            if(item.name != "Leaf" && item.name != "RareLeaf")
            {

            }
        }
    }

    public void ShufflingAll(PoolType pool)
    {
        foreach(GameObject item in ItemManager.Instance.itemList)
        {
            if(item.name != "Leaf" && item.name != "RareLeaf")
            {

            }
        }
    }

    private void draw()
    {
        //...

        reset();
    }

    public void addReward(Reward reward)
    {

    }

    public void giveReward()
    {

    }

    private void reset()
    {
        pool.Clear();
        _rewardList.Clear();
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
