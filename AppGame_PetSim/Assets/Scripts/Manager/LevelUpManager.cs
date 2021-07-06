using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//determeine levelup panel UI text change
//determine what items to be gifted

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance;
    public GameObject LevelUpPanel;
    public Text Level;
    public Image LeafSlot;
    public Text LeafNum;
    public Image ItemSlot;
    public Text ItemNum;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        LevelUpPanel.SetActive(false);
    }
    void Update()
    {
        Level.text = User.Instance.level.ToString();
    }
    public void ClickOK(){
        LevelUpPanel.SetActive(false);
    }
}
