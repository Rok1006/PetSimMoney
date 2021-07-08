using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public GameObject CapsuleMachine;
    Animator CMAnim;
    void Start()
    {
        CMAnim = CapsuleMachine.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void ClickDraw(){ //pressing the buttons to begin drawing animation , for the draw 1 item button
        CMAnim.SetTrigger("PressDraw"); //do a series of anim
        //using event: determine items, in code function decide what items to be generate accord to probability
        //create temporary slot in inspector, get the item and place it in the obj variable
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
