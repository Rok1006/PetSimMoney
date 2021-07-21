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
    public Canvas others;
    
    void Start()
    {
        top.GetComponent<Canvas>().sortingOrder = 3;
        middle.GetComponent<Canvas>().sortingOrder = 2;
        bottom.GetComponent<Canvas>().sortingOrder = 1;
        others.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void ClickTop(){
        SoundManager.Instance.Flip();
        top.GetComponent<Canvas>().sortingOrder = 3;
        middle.GetComponent<Canvas>().sortingOrder = 2;
        bottom.GetComponent<Canvas>().sortingOrder = 1;
        others.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void ClickMiddle(){
        SoundManager.Instance.Flip();
        top.GetComponent<Canvas>().sortingOrder = 1;
        middle.GetComponent<Canvas>().sortingOrder = 3;
        bottom.GetComponent<Canvas>().sortingOrder = 2;
        others.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void ClickBottom(){
        SoundManager.Instance.Flip();
        top.GetComponent<Canvas>().sortingOrder = 1;
        middle.GetComponent<Canvas>().sortingOrder = 2;
        bottom.GetComponent<Canvas>().sortingOrder = 3;
        others.GetComponent<Canvas>().sortingOrder = 0;
    } 
    public void ClickAdsPage(){
        SoundManager.Instance.Flip();
        top.GetComponent<Canvas>().sortingOrder = 2;
        middle.GetComponent<Canvas>().sortingOrder = 1;
        bottom.GetComponent<Canvas>().sortingOrder = 0;
        others.GetComponent<Canvas>().sortingOrder = 3;
    } 
}
