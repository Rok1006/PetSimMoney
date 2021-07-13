using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public GameObject RedMachine;
    Animator RCMAnim;
    public GameObject BlueMachine;
    Animator BCMAnim;
    public GameObject Draw1Panel;
    public GameObject Draw3Panel;
    private int resultState = 0;
    
    public GameObject[] Draw3Slot; //to put invent slot

    private int _luck = 0;
    public int luck { get { return _luck; } }
    private float[] _probability = {0.05f, 0.285f, 0.665f};
    public float[] probability { get { return _probability; } }
    private List<GameObject> pool = new List<GameObject>();
    void Start()
    {
        RCMAnim = RedMachine.GetComponent<Animator>();
        BCMAnim = BlueMachine.GetComponent<Animator>();
        Draw1Panel.SetActive(false);
        Draw3Panel.SetActive(false);
    }

    public GarmentManager.Rarity DrawEgg() 
    {
        float randNum = Random.Range(0.0f, 1.0f);
        GarmentManager.Rarity resultRarity;
        if(randNum < _probability[0]) //Super Rare
        {
            _luck = 0;
            Debug.Log("Super Rare");
            resultRarity = GarmentManager.Rarity.SuperRare;
        } 
        else if (randNum < _probability[1]) //Rare
        {
            _luck += 50;
            Debug.Log("Rare");
            resultRarity = GarmentManager.Rarity.Rare;
        }
        else //Common   
        {
            _luck += 50;
            Debug.Log("Common");
            resultRarity = GarmentManager.Rarity.Common;
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
        Debug.Log("Test: " + _probability[0].ToString() + ", " + _probability[1].ToString());
        _probability[2] = 1.0f - _probability[0] - _probability[1];
        Debug.Log("Super Rare: " + (_probability[0] * 100).ToString() + "% Rare: " +
                                   (_probability[1] * 100).ToString() + "% Common: " +
                                   (_probability[2] * 100).ToString() + "%");
    }
//Costumes Gacha Panel
    public void ClickDraw1(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        RCMAnim.SetTrigger("PressDraw"); //do a series of anim
        resultState = 1;
        
        GarmentManager.Rarity eggType = DrawEgg();
        switch(eggType)
        {
            case GarmentManager.Rarity.Common:
                break;
            case GarmentManager.Rarity.Rare:
                break;
            case GarmentManager.Rarity.SuperRare:
                break;
        }
        //GameObject a = Instantiate(obj, draw1Pos.transform.position, Quaternion.identity); //instanciate prefab
        //draw item and place them in that pos
    }
    public void ClickDraw3(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        RCMAnim.SetTrigger("PressDraw"); //do a series of anim
        resultState = 2;
        for (int i = 0; i < Draw3Slot.Length; i++)
        {
            GarmentManager.Rarity eggType = DrawEgg();
            switch(eggType)
            {
                case GarmentManager.Rarity.Common:
                    break;
                case GarmentManager.Rarity.Rare:
                    break;
                case GarmentManager.Rarity.SuperRare:
                    break;
            }
            //GameObject a = Instantiate([the generated ball color], Draw3Slot[i].transform.position, Quaternion.identity); //instanciate prefab
            //Get the child of gameobjectA and place generate an item ui in it 
        }
        //using event: determine items, in code function decide what items to be generate accord to probability
        //create temporary slot in inspector, get the item and place it in the obj variable
    }
    //Items Gacha Panel: do the same above
    public void crackopen(){ //spine event crackopen in Showing result All
        if(resultState==1){ //draw 1
            Draw1Panel.SetActive(true);
        }else if(resultState==2){ //draw 2
            Draw3Panel.SetActive(true);
        }
    }
 
}
