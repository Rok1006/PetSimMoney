using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoadManager : MonoBehaviour
{
    private const string ResultsFile = "reward.dat";
    private string resultsFilePath;
    public static SaveData data = new SaveData();

    // Start is called before the first frame update
    void Start()
    {
        DataGen();
        resultsFilePath = Path.Combine(Application.persistentDataPath, ResultsFile);
        File.WriteAllText(resultsFilePath,
            (JsonUtility.ToJson(data,true)));
    }

    // Update is called once per frame
    private void DataGen()
    {
        for(int i = 1; i <= 100; i++)
        {
            List<string> names = new List<string>();
            List<int> amount = new List<int>();
            for(int j = 1; j <= 4; j++)
            {
                foreach(GameObject item in ItemManager.Instance.itemList)
                {
                    if(item.name == "Leaf" || item.name == "RareLeaf")
                    {
                        names.Add(item.name);
                        amount.Add(1);
                    }
                }

                foreach(GameObject item in ItemManager.Instance.itemList)
                {
                    if(item.name != "Leaf" && item.name != "RareLeaf")
                    {
                        names.Add(item.name);
                        amount.Add(1);
                    }
                }
            }
            LevelRewardData datafield = new LevelRewardData(i, names, amount);
            data.levelRewardData.Add(datafield);
            //Debug.Log("datafield: " + data.levelRewardData.Count);
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
