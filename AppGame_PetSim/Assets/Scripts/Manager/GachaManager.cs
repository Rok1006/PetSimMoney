using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public GameObject CapsuleMachine;
    public GameObject Draw1Panel;
    public GameObject Draw3Panel;
    private int resultState = 0;
    public GameObject draw1Pos;
    Animator CMAnim;
    public GameObject[] Draw3Slot; //to put invent slot
    void Start()
    {
        CMAnim = CapsuleMachine.GetComponent<Animator>();
        Draw1Panel.SetActive(false);
        Draw3Panel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void ClickDraw1(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        CMAnim.SetTrigger("PressDraw"); //do a series of anim
        resultState = 1;
        //GameObject a = Instantiate(obj, draw1Pos.transform.position, Quaternion.identity); //instanciate prefab
        //draw item and place them in that pos
    }
    public void ClickDraw3(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        CMAnim.SetTrigger("PressDraw"); //do a series of anim
        resultState = 2;
         for (int i = 0; i < Draw3Slot.Length; i++)
        {
            //GameObject a = Instantiate([the generated ball color], Draw3Slot[i].transform.position, Quaternion.identity); //instanciate prefab
            //Get the child of gameobjectA and place generate an item ui in it 
        }
        //using event: determine items, in code function decide what items to be generate accord to probability
        //create temporary slot in inspector, get the item and place it in the obj variable
    }
    public void crackopen(){ //spine event crackopen in Showing result All
        if(resultState==1){ //drAW 1
            Draw1Panel.SetActive(true);
        }else if(resultState==2){ //draw 2
            Draw3Panel.SetActive(true);
        }
    }
    void ShowResults(){
        //if(item type == common)
         //trigger this animation CMAnim.SetTrigger("Red");
        //in event crackopen: add sound, link it to inventory do the generate
        //GameObject j = Instanciate in another invent thing?? shd we open another invent to on off clothes?

        //if(item type == Rare) 
        //trigger this animation CMAnim.SetTrigger("Orange"); 

        //if(item type == SuperRare) 
        //trigger this animation CMAnim.SetTrigger("Yellow/Gold"); 
    }
}
