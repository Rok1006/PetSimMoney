using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this is to manage the store page and product info?
public class StoreManager : MonoBehaviour
{
    public Canvas top;
    public Canvas middle;
    public Canvas bottom;
    
    void Start()
    {
        top.GetComponent<Canvas>().sortingOrder = 2;
        middle.GetComponent<Canvas>().sortingOrder = 1;
        bottom.GetComponent<Canvas>().sortingOrder = 0;
    }

    void Update()
    {
        
    }
    public void ClickTop(){
        top.GetComponent<Canvas>().sortingOrder = 2;
        middle.GetComponent<Canvas>().sortingOrder = 1;
        bottom.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void ClickMiddle(){
        top.GetComponent<Canvas>().sortingOrder = 0;
        middle.GetComponent<Canvas>().sortingOrder = 2;
        bottom.GetComponent<Canvas>().sortingOrder = 1;
    }
    public void ClickBottom(){
        top.GetComponent<Canvas>().sortingOrder = 0;
        middle.GetComponent<Canvas>().sortingOrder = 1;
        bottom.GetComponent<Canvas>().sortingOrder = 2;
    } 
}
