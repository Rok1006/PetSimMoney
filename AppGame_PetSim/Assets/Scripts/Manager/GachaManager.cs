using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public static GachaManager Instance;
    public GameObject RedMachine;
    Animator RCMAnim;
    public GameObject BlueMachine;
    Animator BCMAnim;
    public GameObject Flash;
    Animator flashAnim;
    public GameObject Draw1Panel;
    public GameObject Draw3Panel;
    public GameObject ExchangePanel;
    private int resultState = 0;
    public GameObject[] Draw3Slot; //to put invent slot
    public GameObject[] eggTemplate = new GameObject[3];
    private GameObject[] eggUI = new GameObject[3];
    public GameObject[] drawButton = new GameObject[2]; 
    public GameObject backButton;

    public Text probabilityPreview;
    public Text[] probabilityShowList = new Text[3];
    public int _luck = 0;
    public int luck { get { return _luck; } }
    private float[] _probability = {0.05f, 0.285f, 0.665f};
    public float[] probability { get { return _probability; } }
    private List<List<Garment>> pool = new List<List<Garment>>();
    private Garment result1;
    private Garment[] result3 = new Garment[3];
    public GameObject BG;
    public GameObject closeButton;
    Animator EA;
    Animator[] EA3 = new Animator[3];
    public bool canReveal = false;
    public GameObject particles;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        RCMAnim = RedMachine.GetComponent<Animator>();
        BCMAnim = BlueMachine.GetComponent<Animator>();
        flashAnim = Flash.GetComponent<Animator>();
        Draw1Panel.SetActive(false);
        Draw3Panel.SetActive(false);
        foreach(Rarity rarity in Enum.GetValues(typeof(Rarity)))
        {
            pool.Add(new List<Garment>());
            foreach(GarmentType type in Enum.GetValues(typeof(GarmentType)))
            {
                foreach(Garment garment in GarmentManager.Instance.separatedGarmentLists[(int) type])
                {
                    pool[(int) rarity].Add(garment);
                }
            }
        }
        ProbabilityUpdate();
    }

    public Rarity DrawEgg() 
    {
        float randNum = UnityEngine.Random.Range(0.0f, 1.0f);
        Rarity resultRarity;
        if(randNum < _probability[0]) //Super Rare
        {
            _luck = 0;
            Debug.Log("Super Rare");
            resultRarity = Rarity.SuperRare;
        } 
        else if (randNum < _probability[1]) //Rare
        {
            _luck += 50;
            Debug.Log("Rare");
            resultRarity = Rarity.Rare;
        }
        else //Common   
        {
            _luck += 50;
            Debug.Log("Common");
            resultRarity = Rarity.Common;
        }
        ProbabilityUpdate();
        return resultRarity;
    }

    public void ProbabilityUpdate()
    {
        if(luck <= 1000)
        {
            _probability[0] = Mathf.Round((0.05f + (luck * 0.00005f)) * 1000.0f) / 1000.0f;
        }
        else
        {
            _probability[0] = Mathf.Round((0.1f + ((luck - 1000) * 0.0009f)) * 1000.0f) / 1000.0f;
        }
        _probability[1] = Mathf.Floor(((1.0f - _probability[0]) * 3.0f / 10.0f) * 1000.0f) / 1000.0f;
        //Debug.Log("Test: " + _probability[0].ToString() + ", " + _probability[1].ToString());
        _probability[2] = 1.0f - _probability[0] - _probability[1];
        // Debug.Log("Super Rare: " + (_probability[0] * 100).ToString() + "% Rare: " +
        //                            (_probability[1] * 100).ToString() + "% Common: " +
        //                            (_probability[2] * 100).ToString() + "%");
        probabilityPreview.text = (Mathf.Round(_probability[0]*1000)/10).ToString() + "%";
        for(int i = 0; i < 3; i++)
        {
            probabilityShowList[i].text = (Mathf.Round(_probability[i]*1000)/10).ToString() + "%";
        }
    }
//Costumes Gacha Panel
    public void ClickDraw1(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        if(Status.Instance.LeafChange(CostMethod.GoldLeaf, -10))
        {
            DrawDisable();
            RCMAnim.SetTrigger("PressDraw"); //do a series of anim
            resultState = 1;
            
            Rarity eggType = DrawEgg();
            StartCoroutine(Draw(pool[(int) eggType], eggTemplate[(int) eggType], resultState, 1, Draw1Panel.transform));
            //Draw1Panel.SetActive(true);
            //GameObject a = Instantiate(obj, draw1Pos.transform.position, Quaternion.identity); //instanciate prefab
            //draw item and place them in that pos
        }
        else
        {
            //Not Enough
        }
    }
    public void ClickDraw3(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        if(Status.Instance.LeafChange(CostMethod.GoldLeaf, -30))
        {
            DrawDisable();
            RCMAnim.SetTrigger("PressDraw"); //do a series of anim
            resultState = 3;
            
            for (int i = 0; i < Draw3Slot.Length; i++)
            {
                Rarity eggType = DrawEgg();
                StartCoroutine(Draw(pool[(int) eggType], eggTemplate[(int) eggType], resultState, i, Draw3Slot[i].transform));
                //GameObject a = Instantiate([the generated ball color], Draw3Slot[i].transform.position, Quaternion.identity); //instanciate prefab
                //Get the child of gameobjectA and place generate an item ui in it 
            }
            Draw3Panel.SetActive(true);
            //using event: determine items, in code function decide what items to be generate accord to probability
            //create temporary slot in inspector, get the item and place it in the obj variable
        }
        else
        {
            //Not Enough
        }
    }

    IEnumerator Draw(List<Garment> garments, GameObject template, int drawType, int round, Transform pos)
    {
        yield return new WaitForSeconds(3.8f);
        int rand = UnityEngine.Random.Range(0, garments.Count);
        switch(drawType)
        {
            case 1:
                //Draw to result1
                result1 = garments[rand];
                GarmentManager.Instance.OwnGarment(result1);
                break;
            case 3:
                //Draw to result3
                result3[round] = garments[rand];
                GarmentManager.Instance.OwnGarment(result3[round]);
                break;
            default:
                //Error
                break;            
        }
        GameObject eggUI = Instantiate(template, pos) as GameObject; //where the eggs appear
        switch(drawType)
        {
            case 1:
                Animator eggAnim = eggUI.GetComponent<Animator>(); //new added in
                EA = eggAnim;  //new added in , only work with draw 1 option, not the draw 3 option
                break;
            case 3:
                Animator eggAnim3 = eggUI.GetComponent<Animator>(); //new added in
                EA3[round] = eggAnim3;  //new added in , only work with draw 1 option, not the draw 3 option
                break;
            default:
                //Error
                break;            
        }

        eggUI.GetComponent<Button>().onClick.AddListener(delegate{crackopen(round);});    
        eggUI.SetActive(true);
        eggUI.transform.parent.gameObject.SetActive(true);
    }
    public void ClickEgg(){ //do this animation before the egg disappear
        //eggAnim.SetTrigger("ClickEgg");
    }

    //Items Gacha Panel: do the same above
    public void crackopen(int i = 0){ //spine event crackopen in Showing result All
        flashAnim.SetTrigger("flash");
        SoundManager.Instance.Open();
        SoundManager.Instance.Bling();
        GameObject finalResult;
        if(resultState==1){ //draw 1
            EA.SetTrigger("ClickEgg");  //newly added: work great for draw 1
            GameObject p = Instantiate(particles, Draw1Panel.transform.position, Quaternion.identity); 
            CleanSlot(); //clear egg
            finalResult = Instantiate(result1.garment, Draw1Panel.transform) as GameObject;
            //finalResult.GetComponent<Button>().onClick.AddListener(ClosePanel); //Need to add a close button //not using
            Draw1Panel.SetActive(true);
            //Destroy(p, .6f);
        }
        else if(resultState==3)
        { //draw 2
            // GameObject[] finalResults = new GameObject[3];
            // for(int i = 0; i < 3; i++)
            // {
                GameObject p = Instantiate(particles, Draw3Slot[i].transform.position, Quaternion.identity);
                CleanSlot(i);
                finalResult = Instantiate(result3[i].garment, Draw3Slot[i].transform) as GameObject;
                //finalResult.GetComponent<Button>().onClick.AddListener(ClosePanel); //Need to add a close button //not using
            //}
            Draw3Panel.SetActive(true);
        }
    }

    private void DrawDisable()
    {
        drawButton[0].GetComponent<Button>().enabled = false;
        drawButton[1].GetComponent<Button>().enabled = false;
        probabilityPreview.transform.parent.GetComponent<Button>().enabled = false;
        if(backButton != null)
        {
            backButton.SetActive(false);
        }
    }

    private void DrawEnable()
    {
        drawButton[0].GetComponent<Button>().enabled = true;
        drawButton[1].GetComponent<Button>().enabled = true;
        probabilityPreview.transform.parent.GetComponent<Button>().enabled = true;
        if(backButton != null)
        {
            backButton.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        DrawEnable();
        RCMAnim.SetTrigger("Idle");
        CleanSlot();
        Draw1Panel.SetActive(false);
        Draw3Panel.SetActive(false);
        BG.SetActive(false);
        closeButton.SetActive(false);
    }

    private void CleanSlot()
    {
        foreach (Transform child in Draw1Panel.transform)
        {
            if (child != Draw1Panel.transform){
                //child is your child transform
                Destroy(child.gameObject,0.5f); 
            }
        }

        foreach (GameObject slot in Draw3Slot)
        {
            foreach (Transform child in slot.transform)
            {
                if (child != slot.transform){
                    //child is your child transform
                    DestroyImmediate(child.gameObject);  //add seconds after it?
                }
            }
        }
    }
    
    private void CleanSlot(int i)
    {
        foreach (Transform child in Draw3Slot[i].transform)
        {
            if (child != Draw3Slot[i].transform){
                //child is your child transform
                EA3[i].SetTrigger("ClickEgg");//doent work for the draw three 
                Destroy(child.gameObject,0.5f); //not working great
            }
        }
    }
}
