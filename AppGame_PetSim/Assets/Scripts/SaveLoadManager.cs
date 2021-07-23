using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    public static Data data = new Data();

    // Start is called before the first frame update
    void Awake()
    {
        //DataGen();
        //var serializedData = JsonConvert.SerializeObject(data);
        readData();
        //File.WriteAllText(resultsFilePath, serializedData);
    }

    private void readData()
    {
        TextAsset file = Resources.Load<TextAsset>("Data/data");
        string serializedData = file.text;
        data = JsonConvert.DeserializeObject<Data>(serializedData);
        // foreach(LevelRewardData reward in data.levelRewardData)
        // {
        //     Debug.Log("Level: " + reward.level);
        //     string ids = "";
        //     foreach(string id in reward.name)
        //     {
        //         ids = ids + " " + id;
        //     }
        //     Debug.Log("rewardIDs: " + ids);
        //     string amounts = "";
        //     foreach(int n in reward.amount)
        //     {
        //         amounts = amounts + " " + n.ToString();
        //     }
        //     Debug.Log("Amounts: " + amounts);
        // }
    }

    /*
    private void DataGen()
    {
        for(int i = 1; i <= 100; i++)
        {
            List<string> names = new List<string>();
            List<int> amount = new List<int>();

            foreach(GameObject item in ItemManager.Instance.itemList)
            {
                if(item.name == "Leaf" || item.name == "RareLeaf")
                {
                    names.Add(item.name);
                    amount.Add(1);
                }
            }
            int Count = 0;
            foreach(GameObject item in ItemManager.Instance.itemList)
            {
                if(Count == 4)
                {
                    break;
                }
                else
                {
                    Count++;
                }
                if(item.name != "Leaf" && item.name != "RareLeaf")
                {
                    names.Add(item.name);
                    amount.Add(1);
                }
            }
            LevelRewardData datafield = new LevelRewardData(i, names, amount);
            data.levelRewardData.Add(datafield);
            Debug.Log("datafield: " + data.levelRewardData.Count);
        }
    }
    */
}

public class Data
{
    public List<LevelRewardData> levelRewardData;

    public Data()
    {
        levelRewardData = new List<LevelRewardData>();
    }
}
