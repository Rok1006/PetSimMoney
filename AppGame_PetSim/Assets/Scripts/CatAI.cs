using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
//To do: 
//1: when player do sth to interrupt, the cat return an emotion> a few second later do normal state again
 //when ?? value lower to certain amount stop this state> if players is doing sth to cause a result it stops until that action is done
    //include normal animation: idle, licking, meow
     // void FulfillingState(){ //when ?? value lower to certain amount, do this state, what lower do what first, wait! it doent need to audto do it 
    // //include animation that appears when it is happy and fulfufilled
    // }
public enum Action {Idle, Walking, SitSleeping, SitAwake, Meowing, Dashing, Playing, Smile, Touch, Angry}

public class CatAI : MonoBehaviour
{
    public static CatAI Instance;
    Animator catanim;
    public float sizeValue;
    public GameObject target;
    public GameObject itemPlaceHolder;
    public GameObject FdDk;
     public List<GameObject> toy = new List<GameObject>();
    [Header("Value")]
    public Action action;
    public float speed;
    float x;
    float y;
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
    public bool isPlayingToy = false;
    public bool switchCooldown = false;
   void Awake() {
       Instance = this;
   }
       void Start()
    {
        catanim = this.GetComponent<Animator>();
        executing = true;
        Idle(); //default state is IDLE
        target.transform.position = this.transform.position;
        oldPosition = transform.position.x;
       currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
       beingTouch = false;
        doNormal = true;
    }
    void Update()
    {
        Growth();
        CheckLowStats();
        currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        face();
        switch(action){ //use this to determine possibility, set up a random.range to generate num accord to probability
            case Action.Idle: //idle
            //stop
                break;
            case Action.Walking: //walking
                float step = speed * Time.deltaTime; //
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);//
                if(currentPos == newPos)//if cat arrive at the new pos
                { 
                    currentPos = oldPos; //that current pos become the old positon
                    RanPos(); //do random again
                }
                break;
            case Action.SitSleeping: //sitsleeping
                break;
            case Action.SitAwake: //sitawake
                break;
            case Action.Meowing: //meowing
                break;
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
                        if(toy[i]!= null)
                        {
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
            case Action.Smile:
            break;
            case Action.Touch:
            break;
            case Action.Angry:
            break;
           }
    }
    void CheckLowStats(){ //if hunger and dryness low to certain rate, be angry:have to feed 
        if(Status.Instance.hungerV/Status.Instance.hungerMax+Status.Instance.hydrationV/Status.Instance.hydrationMax<.1f){ 
            if(!isPlayingToy){
                if(0 >= toy.Count){
                    Angry();
                    doNormal = false;
                }else{
                    action = Action.Dashing;
                }
            }else{
                doNormal = true;
            }
        }else{
            doNormal = true;
        }
    }
    

    public void Growth(){
        float sizeMax = (float)Math.Log(User.Instance.level+6)*0.15f;
        float sizeMin = sizeMax*0.75f;
        sizeValue = (sizeMax-sizeMin)*Status.Instance.sizeindex+sizeMin;
        if(sizeValue < sizeMin){
            sizeValue = sizeMin;
        }
    }

    void face(){

        if(this.transform.position.x>target.transform.position.x){
            transform.localScale = new Vector2(sizeValue,sizeValue);
        }else{
            transform.localScale = new Vector2(-sizeValue,sizeValue);
        }
    }




    public void Idle()
    {
        isPlayingToy = false;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100); //1%  increase more the value , lesser it decrease
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 50); //2%
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 100);
        action = Action.Idle;
        catanim.SetTrigger("idle");
        StartCoroutine("SwitchAct"); 
    }

    public void Walking()
    {
        isPlayingToy = false;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 66);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 50);
        action = Action.Walking;
        StartCoroutine("Walk");
    }

    public void SitSleeping()
    {
        isPlayingToy = true;//turn true to stop it from detecting
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 150);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 200);
        Status.Instance.StatsChange(StatsType.Energy, Status.Instance.energyMax); //30
        action = Action.SitSleeping;
         catanim.SetTrigger("sitsleep");
         StartCoroutine("SwitchAct");
    }

    public void SitAwake()
    {
        isPlayingToy = true;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 50);
        Status.Instance.StatsChange(StatsType.Energy, Status.Instance.energyMax / 15); //original 200
        action = Action.SitAwake;
        catanim.SetTrigger("sitawake");
        StartCoroutine("SwitchAct");
    }
    public void Meowing()
    {
        isPlayingToy = false;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 66);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 66);
        action = Action.Meowing;
        catanim.SetTrigger("meow");
        StartCoroutine("SwitchAct");
    }
    public void Dashing()
    {
        executing = true;
        isPlayingToy = true;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 50);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 50);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 100);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 50); //33
        action = Action.Dashing;
    }
    private void Play()
    {
        isPlayingToy = true;
        doNormal = false; //for when playing is executing, normal switch dont execute
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 50);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 50);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 33);
        action = Action.Playing;
        //StartCoroutine("Playing");
    }

    private void Smile(){
        catanim.SetTrigger("happy");
        StartCoroutine("SwitchShortActHappy");
        StopCoroutine("SwitchShortAct");
        StopCoroutine("SwitchAct");
        StopCoroutine("Walk");
        action = Action.Smile;
    }
    private void Touch(){
        catanim.SetTrigger("touch");
        StartCoroutine("SwitchShortAct");
        StopCoroutine("SwitchShortActHappy");
        StopCoroutine("SwitchAct");
        StopCoroutine("Walk");
        action = Action.Touch;
    }
    private void Angry(){ //be angry when stats all lower than certain: use if but nt else, put action = Action.Angry
        catanim.SetTrigger("angry");
        action = Action.Angry;
        doNormal = false;
        CheckToyList();
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
        if(doNormal){//} && !switchCooldown){
        int energyV = 100-(int)Status.Instance.energyV;
        Debug.Log("normalState");
        p = Random.Range(0,81+p); //int p = 
        if(p>0 && p<30){
            Walking();
        }else if(p>=30 && p<43){
            Idle();
        }else if(p>=43 && p<62){
            SitAwake();
        }else if(p>=62 && p<81){
            Meowing();
        }else{// if(p>=81 && p<100){
            SitSleeping();
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
        //Debug.Log(t);
        switchCooldown = true;
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        switchCooldown = false;
        NormalState();//after wait for 5 sec do random generate, detect value to change to different state
        CheckToyList();
    }
     IEnumerator SwitchShortActHappy()
    { //for shorter animation
        if(!switchCooldown)
        {
            switchCooldown = true;
            yield return new WaitForSeconds(2f);//wait for 5 sec to do the next
            switchCooldown = false;
            NormalState();//after wait for 5 sec do random generate, detect value to change to different state
            CheckToyList();
        }
    }
    IEnumerator SwitchShortAct() //for touch
    { //for shorter animation
        if(!switchCooldown)
        {
            switchCooldown = true;
            yield return new WaitForSeconds(.5f);//wait for 5 sec to do the next
            switchCooldown = false;
            NormalState();//after wait for 5 sec do random generate, detect value to change to different state
            CheckToyList();
        }
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
        ItemsDrop.Instance.TouchGenerator();
        beingTouch = true;
        Touch();
        Effects.Instance.HappyEmittion();
        Status.Instance.StatsChange(StatsType.Happiness, Status.Instance.happyMax / 100);
        User.Instance.ExpUP(1);      
     
    }
    void OnMouseUp()
    {
        beingTouch = false; 
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Food") || col.gameObject.CompareTag("Drink"))
        {
            Smile();
            FdDk = col.gameObject.transform.parent.gameObject;
            var sc = FdDk.GetComponent<ObjDrag>();
            sc.DestroyFoodDrink();
            //change action to happy
        }
    }
   
}
