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
    private GameObject rewardIconTemplate;
    private List<GameObject> rewardIconList = new List<GameObject>();
    private List<GameObject> pool = new List<GameObject>();
    private List<Reward> _rewardList = new List<Reward>();
    public List<Reward> rewardList { get { return _rewardList; } }

    public int luck = 0;
    private float[] _probability = {0.05f, 0.285f, 0.7f};
    public float[] probability { get { return _probability; } }

    void Awake() 
    {
        Instance = this;
    }
    void Update()
    {
        
    }

    public void RewardGen(RewardData data, GameObject template)
    {
        int count = 0;
        foreach(string name in data.name)
        {
            GameObject reward = ItemManager.Instance.itemList.Find(x => x.name.Contains(name));
            addReward(new Reward(reward, data.amount[count]));
            count++;
        }
        giveReward();
        RewardUIGen(template);
    }

    public void RewardUIGen(GameObject template)
    {
        rewardIconTemplate = template;

        foreach (GameObject icon in rewardIconList)
        {
            Destroy(icon);
        }
        rewardIconList.Clear();

        foreach(Reward reward in _rewardList)
        {
            GameObject newIcon = Instantiate(rewardIconTemplate) as GameObject;
            newIcon.name = reward.item.name;
            newIcon.GetComponent<Image>().sprite = reward.item.GetComponent<Image>().sprite;
            newIcon.GetComponentInChildren<Text>().text = "X " + reward.amount.ToString();
            newIcon.SetActive(true);
            newIcon.transform.SetParent(rewardIconTemplate.transform.parent, false);
            rewardIconList.Add(newIcon);
        }
        _rewardList.Clear();
    }

    public void ProbabilityUpdate()
    {
        if(luck <= 1000)
        {
            _probability[0] = Mathf.Round((0.05f + (luck * 0.00005f)) * 1000.0f) / 1000.0f;
        }
        else
        {
            probability[0] = Mathf.Round((0.1f + (luck * 0.0009f)) * 1000.0f) / 1000.0f;
        }
        _probability[1] = Mathf.Floor(((1.0f - _probability[0]) * 3.0f / 10.0f) * 1000.0f) / 1000.0f;
        _probability[2] = 1.0f - _probability[0] - _probability[1];
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
        _rewardList.Add(reward);
        //Debug.Log("Reward: " + _rewardList.Count);
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
