﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//To do: 
//1: when player do sth to interrupt, the cat return an emotion> a few second later do normal state again
 //when ?? value lower to certain amount stop this state> if players is doing sth to cause a result it stops until that action is done
    //include normal animation: idle, licking, meow
     // void FulfillingState(){ //when ?? value lower to certain amount, do this state, what lower do what first, wait! it doent need to audto do it 
    // //include animation that appears when it is happy and fulfufilled
    // }
public class CatAI : MonoBehaviour
{
    public enum Action {Idle, Walking, SitSleeping, SitAwake, Meowing, Dashing, Playing}
    public static CatAI Instance;
    Animator catanim;
    public GameObject target;
    public GameObject itemPlaceHolder;
    public GameObject FdDk;
     public List<GameObject> toy = new List<GameObject>();
    [Header("Value")]
    public Action action;
    public float speed;
    float x;
    float y;
    float x1;
    float y1;
    float z1;
    private float oldPosition = 0.0f;
    public Vector3 newPos;
    public Vector3 oldPos;
    public Vector3 currentPos;
    public Vector3 currentToyPos;
    public int p;
    [Header("Check")]
    public bool executing;
    public bool doNormal;
    public bool beingTouch; //check if the cat is being touch; default turn false
    public bool canTouch; //if cat is sleeping it cant be touch; default turn true
    public bool isPlayingToy = false;
    public bool canEatDrink = true;
   void Awake() {
       Instance = this;
   }
       void Start()
    {
        catanim = this.GetComponent<Animator>();
        executing = true;
        Idle(); //default state is IDLE
        target.transform.position = this.transform.position;

        x1 = this.transform.localScale.x;
        y1 = this.transform.localScale.y;
        z1 = this.transform.localScale.z;
        oldPosition = transform.position.x;
        //oldPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);

        beingTouch = false;
        canTouch = true;
        doNormal = true;
    }
    void Update()
    {
        if (transform.position.x > oldPosition) // direction:looking right
        { transform.localScale = new Vector3(-x1,y1,z1); }
        if (transform.position.x < oldPosition) // direction:he's looking left
        { transform.localScale = new Vector3(x1,y1,z1); }
        oldPosition = transform.position.x;
        currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        //
        if(!beingTouch){
        switch(action){ //use this to determine possibility, set up a random.range to generate num accord to probability
            case Action.Idle: //idle
            if(executing)
                {
                    catanim.SetTrigger("idle");
                    StartCoroutine("SwitchAct"); 
                    executing = false;
                }
                break;
            case Action.Walking: //walking
                float step = speed * Time.deltaTime; //
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);//
                if(currentPos == newPos)//if cat arrive at the new pos
                { 
                    currentPos = oldPos; //that current pos become the old positon
                    RanPos(); //do random again
                }
                if(executing)
                {
                    StartCoroutine("Walk");
                    executing = false;
                } break;
            case Action.SitSleeping: //sitsleeping
                if(executing)
                {
                    catanim.SetTrigger("sitsleep");
                    StartCoroutine("SwitchAct");
                    executing = false;
                } break;
            case Action.SitAwake: //sitawake
                if(executing)
                {
                    catanim.SetTrigger("sitawake");
                    StartCoroutine("SwitchAct");
                    executing = false;
                } break;
            case Action.Meowing: //meowing
                if(executing)
                {
                    catanim.SetTrigger("meow");
                    StartCoroutine("SwitchAct");
                    executing = false;
                } break;
            case Action.Dashing: //dashing
                catanim.SetTrigger("dash");
                Play();
                break;
            case Action.Playing: //playing (Happiness has been increased by the toy)
                MoveTowardToy();
                if(currentPos == currentToyPos)
                { //if cat arrive at the toy pos, do play animation
                    for (int i = 0; i < toy.Count; i++) //go through the list
                    {
                        if(toy[i]!= null){
                            var sc = toy[i].GetComponent<ObjDrag>();
                            sc.ToySnap(); 
                            break;
                        }
                    }
                    if(executing)
                    {
                        StartCoroutine("Playing");
                        executing = false;
                    } 
                } break;
           }
        } //beingtouch
    }
    public void Idle()
    {
        isPlayingToy = false;
        canEatDrink = true;
        canTouch = true;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Happiness, -Status.Instance.happyMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Energy, -Status.Instance.energyMax / 100);
        action = Action.Idle;
    }

    public void Walking()
    {
        isPlayingToy = false;
        canEatDrink = false;
        canTouch = false;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Happiness, -Status.Instance.happyMax / 66);
        Status.Instance.StatsChange(Status.StatsType.Energy, -Status.Instance.energyMax / 50);
        action = Action.Walking;
    }

    public void SitSleeping()
    {
        isPlayingToy = true;//turn true to stop it from detecting
        canEatDrink = false;
        canTouch = false;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 150);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 200);
        Status.Instance.StatsChange(Status.StatsType.Energy, Status.Instance.energyMax / 50);
        action = Action.SitSleeping;
    }

    public void SitAwake()
    {
        isPlayingToy = true;
        canEatDrink = true;
        canTouch = false;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Happiness, -Status.Instance.happyMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Energy, Status.Instance.energyMax / 200);
        action = Action.SitAwake;
    }
    public void Meowing()
    {
        isPlayingToy = false;
        canEatDrink = true;
        canTouch = true;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Happiness, -Status.Instance.happyMax / 66);
        Status.Instance.StatsChange(Status.StatsType.Energy, -Status.Instance.energyMax / 66);
        action = Action.Meowing;
    }
    public void Dashing()
    {
        executing = true;
        canTouch = false;
        canEatDrink = false;
        isPlayingToy = true;
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Happiness, -Status.Instance.happyMax / 100);
        Status.Instance.StatsChange(Status.StatsType.Energy, -Status.Instance.energyMax / 33);
        action = Action.Dashing;
    }

    private void Play()
    {
        isPlayingToy = true;
        canEatDrink = false;
        canTouch = false;
        doNormal = false; //for when playing is executing, normal switch dont execute
        Status.Instance.StatsChange(Status.StatsType.Hydration, -Status.Instance.hydrationMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Hunger, -Status.Instance.hungerMax / 50);
        Status.Instance.StatsChange(Status.StatsType.Energy, -Status.Instance.energyMax / 33);
        action = Action.Playing;
    }

    void RanPos()
    {  	//the range where the cat will walk around at
        x =  Random.Range(-1.41f, 16.91f);   //-2,29
        y = Random.Range(-6.3f, -2.78f);   //-12.8,-4
        target.transform.position = new Vector2(x, y);
        newPos = target.transform.position; //inserting the new random pos into this newPos variable
    }
    public void NormalState()
    { //randomize action, change probability when more animation
        if(doNormal){
        p = Random.Range(0,100); //int p = 
        if(p>0 && p<30){
            Walking();
        }else if(p>=30 && p<43){
            Idle();
        }else if(p>=43 && p<62){
            SitSleeping();
        }else if(p>=62 && p<81){
            SitAwake();
        }else if(p>=81 && p<100){
            Meowing();
        }//action = Random.Range(1,6); //uncomment this for simpler randomize
            executing = true; //turn true again 
        }
    }
    public void NeedyState()
    { //when hydration and want food
    }
    public void BoredSleepyState()
    { //when happiness and energy is low
    }
    IEnumerator SwitchAct()
    { //for animation idle, sitsleep,sitawake, meow
        int t = Random.Range(5,10);
        Debug.Log(t);
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        NormalState();//after wait for 5 sec do random generate, detect value to change to different state
        CheckToyList();
    }
    IEnumerator Walk()
    { //walking
        catanim.SetTrigger("walk");
        RanPos();
        yield return new WaitForSeconds(10);//wait for 5 sec to do the next
        NormalState(); 
        CheckToyList();
    }
    IEnumerator Playing()
    { //playing
        Debug.Log("finish executing"); 
        catanim.SetTrigger("play");
        yield return new WaitForSeconds(7);//before it finish waiting the state changes
                   for (int i = 0; i < toy.Count; i++) //go through the list 
                {
                    if(toy[i]!= null){
                         var sc = toy[i].GetComponent<ObjDrag>();
                        sc.DestroyToy();
                        break;
                    }
                }
        //Destroy(itemPlaceHolder.transform);
        doNormal = true;
        isPlayingToy = false;
        
        NormalState(); //back to normal
        CheckToyList(); //shd be after Normal state
         //if dont wanna allow it to detect toy after play comment this;
    }
    void CheckToyList()
    {
              for (int i = 0; i < toy.Count; i++)
                {
                    if(toy[i]!= null){
                        Debug.Log("did");
                       var sc = toy[i].GetComponent<ObjDrag>();
                        sc.DetectToy(); //works 
                    }
                }
    }
    void stopMeow()
    { //end state
        catanim.SetTrigger("stopmeow");
    }
     void Stand()
     { //end state
        catanim.SetTrigger("stand");
    }
    void MoveTowardToy()
    {
        float step1 = speed * Time.deltaTime; //
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentToyPos, step1);//
    }
    void OnMouseDown()
    {
        //if(canTouch)
        //{
            beingTouch = true;
            catanim.SetTrigger("touch");
            Effects.Instance.HappyEmittion();
            Status.Instance.StatsChange(Status.StatsType.Happiness, Status.Instance.happyMax / 100);
        //}
    }
    void OnMouseUp()
    {
        beingTouch = false; 
        canTouch = false;
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Food") || col.gameObject.CompareTag("Drink"))
        {
            FdDk = col.gameObject.transform.parent.gameObject;
            var sc = FdDk.GetComponent<ObjDrag>();
            sc.DestroyFoodDrink();
        }
    }
   
}
