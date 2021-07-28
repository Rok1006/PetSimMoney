using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//To do: 
//- set cat to stop action stay idle
//- create complete dialogue
//- when dialogue end,(ifindex == index.length sthsth, diable beginner guide)
// event happen with dialogue, fake buttns?
//sprites quide
public class BeginnerGuide : MonoBehaviour
{
    //public Queue<string> sentences;
    public string playerName;
    public float typingSpeed;
    private int index;
    public Text textDisplay;
    public GameObject NextB;
    [TextArea(3,10)]
    public string[] sentences;
  
    int guideState = 0;
    void Start()
    {
        //sentences = new Queue<string>();
        StartCoroutine(Type());
        NextB.GetComponent<Button>().interactable = true;
    }

    void Update()
    {
       switch(guideState) {
           case 0:

           break;
       }
       if(textDisplay.text == sentences[index]){
           NextB.GetComponent<Button>().interactable = true;
       }
    }
    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentences(){
        NextB.GetComponent<Button>().interactable = false;
        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else{
            textDisplay.text = "";
        }
    }
}
