using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

public class Cat : MonoBehaviour
{
    public static Cat Instance;
    Animator catanim;
    float dist,disty;
    GameObject FdDk;
    public Action action;
    public GameObject itemPlaceHolder;
    public GameObject[] Decoration;
    public GameObject[] Point;
    bool selected;
    bool switchCooldown;
    public bool mochiBool,bluBool;
    Vector3 pos;
    Vector3 scale;
    void Awake(){
        Instance = this;
    }
    void Start(){
        scale = transform.localScale;
        dist = transform.position.z - Camera.main.transform.position.z;
        disty = transform.position.y;
        catanim = GetComponent<Animator>();
        Idle();
        //catanim.SetTrigger("dash");
        arrange();
    }
    void Update(){
        if(mochiBool){
                transform.localScale = scale;
            }else if(bluBool){
                transform.localScale = new Vector3(-10f,10f,10f);
        }
        if(selected == true){
            pos = Input.mousePosition;
            pos.z = dist;
            pos = Camera.main.ScreenToWorldPoint(pos);
            if(pos.x < Point[0].transform.position.x)
            {
                pos.x = Point[0].transform.position.x;
            }
            if(pos.x > Point[1].transform.position.x)
            {
                pos.x = Point[1].transform.position.x;
            }
            if(pos.y > Point[0].transform.position.y)
            {
                pos.y = Point[0].transform.position.y;
            }
            if(pos.y < Point[2].transform.position.y)
            {
                pos.y = Point[2].transform.position.y;
            }

            if(mochiBool){
                pos.y = disty;
            }
            transform.position = pos;
            
        }
    
        if(Input.GetMouseButtonUp(0)){
            selected = false;
        }
        switch(action){ //use this to determine possibility, set up a random.range to generate num accord to probability
            case Action.Idle: //idle
            //stop
                break;
            case Action.Dashing: //dashing
                catanim.SetTrigger("dash");
                break;
            case Action.Smile:
            break;
            case Action.Angry:
            break;
        }
    }
    void OnMouseOver(){
        if(Input.GetMouseButton(0)){
            selected = true;
        }
    }
    public void Idle(){
        catanim.SetTrigger("idle");
        action = Action.Idle;
    }
    void Smile(){
        catanim.SetTrigger("happy");
        StartCoroutine("SwitchShortActHappy");
        action = Action.Smile;
    }
    void Angry(){ //be angry when stats all lower than certain: use if but nt else, put action = Action.Angry
        catanim.SetTrigger("angry");
        StartCoroutine("SwitchShortActHappy");
        action = Action.Angry;
    }

    public void arrange(){
        for(int j=0; j< Decoration.Length;j++){
            SpriteRenderer[] sprites = Decoration[j].GetComponentsInChildren<SpriteRenderer>();
            for(int i=0; i<sprites.Length;i++){
                sprites[i].sortingLayerName = "UI";
                sprites[i].sortingOrder = 5;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "mochi")
        {
            var sc = col.gameObject.GetComponent<ObjDrag>();
            if( sc.score >0)
            {
                Smile();
            }else{
                Angry();
            }
            sc.Destroymochi();
        }else if (col.gameObject.tag == "Fish"){
            blublublu.Instance.FinishGame();
        }
    }

    IEnumerator SwitchShortActHappy()
    { //for shorter animation
        if(!switchCooldown)
        {
            switchCooldown = true;
            yield return new WaitForSeconds(1);//wait for 5 sec to do the next
            switchCooldown = false;
            action = Action.Dashing;
        }
    }
}
