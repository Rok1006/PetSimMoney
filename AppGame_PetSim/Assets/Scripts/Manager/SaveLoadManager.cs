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
   public static SaveLoadManager Instance;
    public static Data data = new Data();
    private const string ResultsFile = "userdata3.dat";
    private string _resultsFilePath;
    bool start = true;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        readData();
    }

    void Start() 
    {
        /*_resultsFilePath = Path.Combine(Application.persistentDataPath +"/", ResultsFile);
        if(!File.Exists(_resultsFilePath))
        {
            Debug.Log("CreateDirectory");
            Directory.CreateDirectory(_resultsFilePath);
        }
        else
        {
            LoadData();
        }*/
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

    internal static bool FileOrDirectoryExists(string name)
{
   return (Directory.Exists(name) || File.Exists(name));
}


    private void readData()
    {
        TextAsset file = Resources.Load<TextAsset>("Data/data");
        string serializedData = file.text;
        data = JsonConvert.DeserializeObject<Data>(serializedData);
    }

    public void CreateNewResultsFile()
    {
        SaveFile(new UserData(), _resultsFilePath);
        //LoadData();
    }

    public void SaveData()
    {
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
            foreach(Garment garment in WareroomManager.Instance._separatedGarmentLists[(int) type])
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
        userdata.currentBar = AdsManager.Instance.currentBar;
        userdata.blocks = new List<bool>();
        userdata.buttons = new List<bool>();
        //userdata.received = AdsManager.Instance.received;
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

        userdata.quitDt = AdsManager.Instance.quitDt; //date time
        userdata.onDt = AdsManager.Instance.onDt; //date time

        userdata.MusicIsOn = Manager.Instance.BGM.activeSelf;
        userdata.SoundIsOn = Manager.Instance.SoundM.activeSelf;
        //daily reward
        userdata.currentday = DailyRewardsManager.Instance.currentday;
        userdata.rewardblocks = new List<bool>();
        userdata.rewardchecks = new List<bool>();
        userdata.daybuttons = new List<bool>();
        foreach(GameObject Check in DailyRewardsManager.Instance.Checks)
        {
            if(Check.activeSelf)
            {
                userdata.rewardchecks.Add(true);
            }
            else
            {
                userdata.rewardchecks.Add(false);
            }
        }
           foreach(GameObject Block in DailyRewardsManager.Instance.Blocks)
        {
            if(Block.activeSelf)
            {
                userdata.rewardblocks.Add(true);
            }
            else
            {
                userdata.rewardblocks.Add(false);
            }
        }
        foreach(GameObject daybutton in DailyRewardsManager.Instance.Day)
        {
            if(daybutton.GetComponent<Button>().interactable)
            {
                userdata.daybuttons.Add(true);
            }
            else
            {
                userdata.daybuttons.Add(false);
            }
        }
        userdata.collected = DailyRewardsManager.Instance.collected;

        userdata.currentPlanet = SceneryManager.Instance.currentPlanet;
        userdata.AdventurePlanetList = new List<int>();
        for(int i = 0; i<AdventureManager.Instance.AdventurePlanetList.Count;i++)
        {
            userdata.AdventurePlanetList.Add(AdventureManager.Instance.AdventurePlanetList[i]);
        }
        userdata.ts = AdventureManager.Instance.ts;
        Debug.Log("userdata.ts "+ userdata.ts);
        userdata.planetID = AdventureManager.Instance.planetID;
        userdata.adventureNUM = AdventureManager.Instance.adventureNUM;
        userdata.isExplore = AdventureManager.Instance.isExplore;
        userdata.mochiScore = ScoreManager.Instance.mochiScore;
        userdata.bluScore = ScoreManager.Instance.bluScore;
        
        return userdata;
    }

    private void SaveFile(object data, string path)
    {
        SaveFile(path, data);
        Debug.Log("path" +path);
    }

    private static void SaveFile(string filePath, object data)
    {
        File.WriteAllText(filePath,
            (JsonUtility.ToJson(data,true)));
        //Debug.Log("Save file to " + filePath);
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

        AdsManager.Instance.quitDt = resultData.quitDt; //date time
        
        foreach(string name in resultData.ownGarment)
        {
            foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
            {
                foreach(Garment garment in WareroomManager.Instance._separatedGarmentLists[(int) type])
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
                foreach(Garment garment in WareroomManager.Instance._separatedGarmentLists[(int) type])
                {
                    if(garment.garment.name == name)
                    {
                        WareroomManager.Instance.currentGarment[(int) type] = garment;
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

        AdsManager.Instance.currentBar = resultData.currentBar;
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
        //AdsManager.Instance.received = resultData.received;
        AdsManager.Instance.quitDt = resultData.quitDt; //date time
        AdsManager.Instance.onDt = resultData.onDt; //date time
        Manager.Instance.BGM.SetActive(resultData.MusicIsOn);//mute = resultData.MusicIsOn;
        Manager.Instance.SoundM.SetActive(resultData.SoundIsOn);
        //daily rewards
        DailyRewardsManager.Instance.currentday = resultData.currentday;
            i = 0;
        foreach(bool check in resultData.rewardchecks)
        {
            DailyRewardsManager.Instance.Checks[i].SetActive(check);
            i++;
        }
             i = 0;
        foreach(bool block in resultData.rewardblocks)
        {
            DailyRewardsManager.Instance.Blocks[i].SetActive(block);
            i++;
        }
        i = 0;
        foreach(bool db in resultData.daybuttons)
        {
            DailyRewardsManager.Instance.Day[i].GetComponent<Button>().interactable = db;
            i++;
        }
        DailyRewardsManager.Instance.collected = resultData.collected;
        SceneryManager.Instance.currentPlanet = resultData.currentPlanet;
        AdventureManager.Instance.AdventurePlanetList = resultData.AdventurePlanetList;
        AdventureManager.Instance.ts = resultData.ts;
        Debug.Log("AdventureManager.Instance.ts " + AdventureManager.Instance.ts);
        AdventureManager.Instance.planetID = resultData.planetID;
        AdventureManager.Instance.adventureNUM = resultData.adventureNUM;
        AdventureManager.Instance.isExplore = resultData.isExplore;
        ScoreManager.Instance.mochiScore = resultData.mochiScore;
        ScoreManager.Instance.bluScore = resultData.bluScore;


        //end
        Manager.Instance.UpdateSettingPannel();
        WareroomManager.Instance.UpdateGarmentUI();
        WareroomManager.Instance.UpdateWear();
        ShopManager.Instance.ShopItemsGen();
        if(AdventureManager.Instance.isExplore){
            AdventureManager.Instance.doUpdate();
        }
        
    }

    private static T LoadData<T>(string filePath)
    {
        //Debug.Log("Load file from " + filePath);
        return JsonUtility.FromJson<T>(
            File.ReadAllText(filePath));
        
    }

    private UserData LoadResultsData()
    {
        return LoadData<UserData>(_resultsFilePath);
    }

    void OnApplicationQuit()
    {
    //Call your save routine here
        SaveData();
    }
    void OnApplicationPause(bool pauseStatus){//True if the application is paused, else False.
        if(start){
            start = !start;
        }else{
            Debug.Log("OnApplicationPause savedata");
            SaveData();
        }
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
