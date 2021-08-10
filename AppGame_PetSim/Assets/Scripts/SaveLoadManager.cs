using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    public static Data data = new Data();
    private const string ResultsFile = "userdata.dat";
    private string _resultsFilePath;


    // Start is called before the first frame update
    void Awake()
    {
        //DataGen();
        //var serializedData = JsonConvert.SerializeObject(data);
        readData();
        //File.WriteAllText(resultsFilePath, serializedData);
    }

    void Start() 
    {
        _resultsFilePath = Path.Combine(Application.persistentDataPath, ResultsFile);

        if (!File.Exists(_resultsFilePath))
        {
            CreateNewResultsFile();
        }
        else
        {
            LoadData();
        }
    }

    private void readData()
    {
        TextAsset file = Resources.Load<TextAsset>("Data/data");
        string serializedData = file.text;
        data = JsonConvert.DeserializeObject<Data>(serializedData);
        //Resources.
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

    public void CreateNewResultsFile()
    {
        SaveFile(new UserData(), _resultsFilePath);
        //LoadData();
    }

    public void SaveData()
    {
        //var resultData = LoadResultsData();
        //resultData.incorrectSwipes += incorrectSwipes;
        UserData resultData = GetUserData();
        SaveFile(resultData, _resultsFilePath);
    }

    private UserData GetUserData()
    {
        UserData userdata = new UserData();
        userdata.level = User.Instance.level;
        userdata.maxfpValue = User.Instance.maxfpValue;
        userdata.fpValue = User.Instance.fpValue;
        userdata.beginnerGuide = User.Instance.beginnerGuide;

        userdata.statsValue = Status.Instance.statsValue;
        userdata.statsMax = Status.Instance.statsMax;
        userdata.leafC = Status.Instance.leafC;

        userdata.ownGarment = new List<string>();
        userdata.currentGarment = new List<string>();
        foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
        {
            foreach(Garment garment in GarmentManager.Instance._separatedGarmentLists[(int) type])
            {
                if(garment.owned)
                {
                    userdata.ownGarment.Add(garment.garment.name);
                }
                if(garment.eqiupped)
                {
                    userdata.currentGarment.Add(garment.garment.name);
                }
            }
        }

        userdata.collection = new List<string>();
        userdata.amount = new List<int>();
        userdata.page = new List<int>();
        userdata.slot = new List<int>();
        foreach(ItemsCollect collect in Inventory.instance.collection)
        {
            userdata.collection.Add(collect.item.GetComponent<ItemsInfo>().itemID);
            userdata.amount.Add(collect.amount);
            userdata.page.Add(collect.slotPlaced[0]);
            userdata.slot.Add(collect.slotPlaced[1]);
        }

        userdata.luck = GachaManager.Instance._luck;
////Ads Manager 
        userdata.blocks = new List<bool>();
        userdata.buttons = new List<bool>();
        //userdata.currentBar = AdsManager.Instance.currentBar; //save and load the num of current bar not working??
        foreach(GameObject bar in AdsManager.Instance.bars)
        {
            if(bar.activeSelf)
            {
                userdata.blocks.Add(true);
            }
            else
            {
                userdata.blocks.Add(false);
            }
        }

        foreach(GameObject ClaimButton in AdsManager.Instance.ClaimButtons)
        {
            if(ClaimButton.GetComponent<Button>().interactable)
            {
                userdata.buttons.Add(true);
            }
            else
            {
                userdata.buttons.Add(false);
            }
        }

        return userdata;
    }

    private void SaveFile(object data, string path)
    {
        SaveFile(path, data);
    }

    private static void SaveFile(string filePath, object data)
    {
        File.WriteAllText(filePath,
            (JsonUtility.ToJson(data,true)));
        Debug.Log("Save file to " + filePath);
    }

    public void LoadData()
    {
        var resultData = LoadResultsData();
        User.Instance.level = resultData.level;
        User.Instance.maxfpValue = resultData.maxfpValue;
        User.Instance.fpValue = resultData.fpValue;
        User.Instance.beginnerGuide = resultData.beginnerGuide;

        Status.Instance.statsValue = resultData.statsValue;
        Status.Instance.statsMax = resultData.statsMax;
        Status.Instance.leafC = resultData.leafC;
        
        foreach(string name in resultData.ownGarment)
        {
            foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
            {
                foreach(Garment garment in GarmentManager.Instance._separatedGarmentLists[(int) type])
                {
                    if(garment.garment.name == name)
                    {
                        garment.owned = true;
                    }
                }
            }
        }

        foreach(string name in resultData.currentGarment)
        {
            foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
            {
                foreach(Garment garment in GarmentManager.Instance._separatedGarmentLists[(int) type])
                {
                    if(garment.garment.name == name)
                    {
                        GarmentManager.Instance.currentGarment[(int) type] = garment;
                        garment.eqiupped = true;
                    }
                }
            }
        }

        int i = 0;
        foreach(string name in resultData.collection)
        {
            foreach(ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                foreach(GameObject item in ItemManager.Instance._separatedItemLists[(int) type])
                {
                    if(item.name == name)
                    {
                        //Debug.Log("i: " + i.ToString() + " slot[i]: " + resultData.slot[i].ToString());
                        Inventory.instance.AddToSlots(item, resultData.amount[i], resultData.page[i], resultData.slot[i]);
                    }
                }
            }
            i++;
        }

        GachaManager.Instance._luck = resultData.luck;

        i = 0;
        foreach(bool block in resultData.blocks)
        {
            AdsManager.Instance.bars[i].SetActive(block);
            i++;
        }

        i = 0;
        foreach(bool button in resultData.buttons)
        {
            AdsManager.Instance.ClaimButtons[i].GetComponent<Button>().interactable = button;
            i++;
        }

        GarmentManager.Instance.UpdateGarmentUI();
        GarmentManager.Instance.UpdateWear();
        ShopManager.Instance.ShopItemsGen();
    }

    private static T LoadData<T>(string filePath)
    {
        Debug.Log("Load file from " + filePath);
        return JsonUtility.FromJson<T>(
            File.ReadAllText(filePath));
        
    }

    private UserData LoadResultsData()
    {
        return LoadData<UserData>(_resultsFilePath);
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

    void OnApplicationQuit()
    {
    //Call your save routine here
    SaveData();
    }
}

public class Data
{
    public List<LevelRewardData> levelRewardData;

    public Data()
    {
        levelRewardData = new List<LevelRewardData>();
    }
}
