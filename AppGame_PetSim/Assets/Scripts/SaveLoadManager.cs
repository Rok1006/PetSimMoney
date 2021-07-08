using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    private const string ResultsFile = "reward.dat";
    private string resultsFilePath;
    public static SaveData data = new SaveData();

    // Start is called before the first frame update
    void Start()
    {
        DataGen();
        var serializedData = JsonConvert.SerializeObject(data);
        //var newPerson = JsonConvert.DeserializeObject<Person>(serializedPerson);
        resultsFilePath = Path.Combine(Application.persistentDataPath, ResultsFile);
        //File.WriteAllText(resultsFilePath, serializedData);
    }

    // Update is called once per frame
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
}

public class SaveData
{
    public List<LevelRewardData> levelRewardData;

    public SaveData()
    {
        levelRewardData = new List<LevelRewardData>();
    }
}
