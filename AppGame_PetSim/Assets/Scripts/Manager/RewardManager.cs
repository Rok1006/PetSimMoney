using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//determeine levelup panel UI text change
//determine what items to be gifted

public enum PoolType { Common, Rare } 

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance; 
    private GameObject rewardIconTemplate;
    private List<GameObject> rewardIconList = new List<GameObject>();
    private List<Reward> _rewardList = new List<Reward>();
    public List<Reward> rewardList { get { return _rewardList; } }

    void Awake() 
    {
        Instance = this;
    }
    void Update()
    {
        
    }

    public void RewardGen(List<Reward> rewards, GameObject[] slots, bool visible)
    {
        _rewardList.Clear(); //clear old list
        foreach(Reward reward in rewards)
        {
            addReward(reward);
        }
        giveReward();
        RewardUIGen(slots, visible);
    }

    public void RewardGen(RewardData data, GameObject template)
    {
        _rewardList.Clear(); //clear old list
        int count = 0;
        foreach(string name in data.name)
        {
            GameObject reward = ItemManager.Instance.itemList.Find(x => x.name.Equals(name));
            addReward(new Reward(reward, data.amount[count]));
            count++;
        }
        giveReward();
        RewardUIGen(template);
    }

    private void RewardUIGen(GameObject[] slots, bool visible)
    {
        foreach (GameObject icon in rewardIconList) //clear old list
        {
            Destroy(icon);
        }
        rewardIconList.Clear();

        int Count = 0;
        foreach(GameObject slot in slots)
        {
            GameObject newIcon = Instantiate(rewardIconTemplate) as GameObject;
            newIcon.name = _rewardList[Count].item.name;
            newIcon.GetComponent<Image>().sprite = _rewardList[Count].item.GetComponent<Image>().sprite;
            newIcon.GetComponentInChildren<Text>().text = "X " + _rewardList[Count].amount.ToString();
            if(visible)
            {
                newIcon.SetActive(true);
            }
            newIcon.transform.SetParent(rewardIconTemplate.transform.parent, false);
            rewardIconList.Add(newIcon);
            Count++;
        }
    }

    private void RewardUIGen(GameObject template) //Generate icons of rewards
    {
        rewardIconTemplate = template;

        foreach (GameObject icon in rewardIconList) //clear old list
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
    }

    private void addReward(Reward reward)
    {
        _rewardList.Add(reward);
        //Debug.Log("Reward: " + _rewardList.Count);
    }

    public void giveReward()
    {
        foreach(Reward reward in rewardList)
        {
            ItemsInfo info = reward.item.GetComponent<ItemsInfo>();
            if(info.type != ItemType.Leaf)
            {
                Inventory.instance.AddToSlots(reward.item, reward.amount);
            }
            else if(info.itemID == "Leaf")
            {
                Status.Instance.LeafChange(CostMethod.GreenLeaf, reward.amount);
            }
            else if(info.itemID == "RareLeaf")
            {
                Status.Instance.LeafChange(CostMethod.GoldLeaf, reward.amount);
            }
        }
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
