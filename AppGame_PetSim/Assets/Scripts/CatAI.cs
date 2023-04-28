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
public enum Action {Idle, Walking, SitSleeping, SitAwake, Meowing, Dashing, Playing, Smile, Touch, Angry, Jump, Prejump}

public class CatAI : MonoBehaviour
{
    public static CatAI Instance;
    Animator catanim;
    public float sizeValue;
    public GameObject target;
    public GameObject itemPlaceHolder;
    public GameObject[] Decoration;
    //SpriteRenderer sprites;
    public GameObject FdDk;
    public List<GameObject> toy = new List<GameObject>();
    [Header("Value")]
    public Action action;
    public float speed;
    public float timePass;
    float step;
    float x;
    float y;
    bool point1 = true;
    Vector3 wareroomPos;
    private float oldPosition = 0.0f;
    public Vector3 newPos;
    public Vector3 oldPos;
    public Vector3 currentPos;
    public Vector3 currentToyPos;
    public int p;
    [Header("Check")]
    public bool enableCat = false;
    public bool executing;
    public bool doNormal;
    public bool beingTouch; //check if the cat is being touch; default turn false
    public bool isPlayingToy = false;
    public bool caughtToy = false;
    public bool canEatDrink = true;
    public bool switchCooldown = false;
    public bool getToy;
    public bool tiredCoolDown;
    public bool doIDLE = false;
    public bool sortlayer;
    int TouchValue;// if continue touch it, it will show other action;
    MeshRenderer catMesh;
    bool angry;

   void Awake() {
       Instance = this;
   }
       void Start()
    {
        wareroomPos = transform.position;
        catMesh = GetComponent<MeshRenderer>();
        catanim = GetComponent<Animator>();
        executing = true;  
        //Idle(); //default state is IDLE
        target.transform.position = this.transform.position;
        oldPosition = transform.position.x;
        //oldPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);

        beingTouch = false;
        switchCooldown = false;
        doNormal = true;
        NormalState();
        //Prejump();
        CheckLowStats();
    }
    void Update()
    {    
        step = speed * Time.deltaTime;
        if(sortlayer){
            onClickWareroom();
        }
        if(enableCat){  //active the cat animation after disable
            CheckToyList();
            NormalState();
            enableCat = false;
        }
        Growth();
        if (transform.position.x > oldPosition){ // direction:looking right
         transform.localScale = new Vector3(-sizeValue,sizeValue,sizeValue);
        }else{
        transform.localScale = new Vector3(sizeValue,sizeValue,sizeValue); }
        oldPosition = transform.position.x;
        currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        switch(action){ //use this to determine possibility, set up a random.range to generate num accord to probability
            case Action.Idle: //idle
            //stop
                break;
            case Action.Walking: //walking
                //float step = speed * Time.deltaTime;
                
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
            case Action.Playing: //playing (Happiness has been increased by the toy)\
                if(!tiredCoolDown){
                    StartCoroutine("Tired");
                }
                if(!caughtToy){
                    timePass += Time.deltaTime;
                }           
                MoveTowardToy();               
                if(currentPos == currentToyPos)
                { //if cat arrive at the toy pos, do play animation
                    caughtToy = true;
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
                }
                 break;
            case Action.Smile:
            break;
            case Action.Touch:
            break;
            case Action.Angry:
            break;
            case Action.Prejump:
                if(currentPos == newPos){
                    Jump();
                }
                target.transform.position = SceneryManager.Instance.CatTreePos(0);//CatTree Point0
                newPos = target.transform.position;
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step *1.25f);
            break;
            case Action.Jump:
                 
                if(point1){
                    target.transform.position =SceneryManager.Instance.CatTreePos(1);//point1
                    newPos = target.transform.position;
                    if(currentPos == newPos){
                        target.transform.position = SceneryManager.Instance.CatTreePos(2);//point2
                        newPos = target.transform.position;
                        point1 = false;
                    }
                }
                //float step3 = speed * Time.deltaTime *2f;
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step *5f);//
            break;
           }
    }
    void CheckLowStats(){ //if hunger and dryness low to certain rate, be angry:have to feed
        if(toy.Count == 0){
            if(Status.Instance.hungerV/Status.Instance.hungerMax < .2f || Status.Instance.hydrationV/Status.Instance.hydrationMax<.2f){
                StartCoroutine("Angrying");
                angry = true;
            }else{
                angry = false;
                doNormal = true;
            }
        }else{StartCoroutine("doNothing");}
    }
    

    public void Growth(){
        float sizeMax = (float)Math.Log(User.Instance.level+20)*0.1f;
        float sizeMin = sizeMax*0.75f;
        sizeValue = (sizeMax-sizeMin)*Status.Instance.sizeindex+sizeMin;
        if(sizeValue < sizeMin){
            sizeValue = sizeMin;
        }
    }




    public void Idle()
    {
        if(!doIDLE){
            canEatDrink = true;
            Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
            Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100); //1%  increase more the value , lesser it decrease
            Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 50); //2%
            Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 100);
        }
        action = Action.Idle;
        catanim.SetTrigger("idle");
        StartCoroutine("SwitchAct"); 
    }

    public void Walking()
    {
        canEatDrink = false;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 100);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 100);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 66);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 50);
        action = Action.Walking;
        StartCoroutine("Walk");
    }

    public void SitSleeping()
    {
        //isPlayingToy = false;//turn true to stop it from detecting
        canEatDrink = false;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 150);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 200);
        Status.Instance.StatsChange(StatsType.Energy, Status.Instance.energyMax/30); //30
        action = Action.SitSleeping;
         catanim.SetTrigger("sitsleep");
         StartCoroutine("SwitchAct");
    }

    public void SitAwake()
    {
        canEatDrink = true;
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
        canEatDrink = true;
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
        canEatDrink = false;
        isPlayingToy = true;
        Status.Instance.StatsChange(StatsType.Hydration, -Status.Instance.hydrationMax / 50);
        Status.Instance.StatsChange(StatsType.Hunger, -Status.Instance.hungerMax / 50);
        Status.Instance.StatsChange(StatsType.Happiness, -Status.Instance.happyMax / 100);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 50); //33
        action = Action.Dashing;
    }
    private void Play()
    {
        canEatDrink = false;
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
        action = Action.Smile;
    }
    private void Touch(){
        if(TouchValue >= 10 && angry == false){
            Debug.Log("touch2");
            catanim.SetTrigger("touch2");
        }else{
            Debug.Log("touch");
            catanim.SetTrigger("touch");
        }
        StartCoroutine("SwitchShortAct");
        action = Action.Touch;
    }
    private void Angry(){ //be angry when stats all lower than certain: use if but nt else, put action = Action.Angry
        catanim.SetTrigger("angry");
        action = Action.Angry;
        doNormal = false;
    }

    private void Prejump(){ //be angry when stats all lower than certain: use if but nt else, put action = Action.Angry
        catanim.SetTrigger("walk");
        action = Action.Prejump;
    }

    private void Jump(){
        catanim.SetTrigger("jump");
        action = Action.Jump;
        StartCoroutine("Jumping");
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

        if(doNormal && !switchCooldown){
        p = Random.Range(0,100); //int p = 
        if(p>0 && p<70){
            Walking();
        }else if(p>=70 && p<77){
            Idle();
        }else if(p>=77 && p<84){
            SitAwake();
        }else if(p>=84 && p<93){
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
        switchCooldown = true;
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        switchCooldown = false; 
        if(!doIDLE){
            isPlayingToy = false;
            NormalState();//after wait for 5 sec do random generate, detect value to change to different state
            CheckToyList();
        }else{
            Idle();
        }
        
    }

     IEnumerator Angrying()
    { //for shorter animation
        catanim.SetTrigger("angry");
        action = Action.Angry;
        doNormal = false;
        yield return new WaitForSeconds(1.333f);
        if(!doIDLE){
          CheckLowStats();  
        }
        
    }
    
    IEnumerator doNothing()
    { //for shorter animation
        yield return new WaitForSeconds(1.333f);
        CheckLowStats();
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
        getToy= true;
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
        timePass = 0f;
        switchCooldown = false;
        doNormal = true;
        isPlayingToy = false;
        caughtToy = false;
        getToy = false;
        
        NormalState(); //back to normal
        CheckToyList(); //shd be after Normal state
         //if dont wanna allow it to detect toy after play comment this;
    }

    IEnumerator Tired()
    { //for animation idle, sitsleep,sitawake, meow
        tiredCoolDown = true;
        Status.Instance.StatsChange(StatsType.Happiness, Status.Instance.happyMax / 200);
        Status.Instance.StatsChange(StatsType.Energy, -Status.Instance.energyMax / 100);
        yield return new WaitForSeconds(1);//wait for 5 sec to do the next
        tiredCoolDown = false;
    }

    IEnumerator Jumping()
    { //walking
        catanim.SetTrigger("jump");
        yield return new WaitForSeconds(2);//wait for 5 sec to do the next
        NormalState(); 
        CheckToyList();
    }

    IEnumerator ResetTouchValue()
    { //walking
        yield return new WaitForSeconds(3);//wait for 5 sec to do the next
        TouchValue--;
    }


    void CheckToyList()
    {
        for (int i = 0; i < toy.Count; i++)
        {
            if(toy[i]!= null){
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
        float step1 = speed * Time.deltaTime*(1f+timePass*0.15f); //*Status.Instance.energyV *0.01f; //energy affect its speed
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentToyPos, step1);//
    }
    void OnMouseDown()
    {
        if(!isPlayingToy){
            ItemsDrop.Instance.TouchGenerator();    
            beingTouch = true;
            Touch();
            Effects.Instance.HappyEmittion();
            Status.Instance.StatsChange(StatsType.Happiness, Status.Instance.happyMax / 100);
            User.Instance.ExpUP(1); 
            TouchValue++;
            StartCoroutine("ResetTouchValue");
        }
           
     
    }
    void OnMouseUp()
    {
        beingTouch = false; 
    }

    public void onClickWareroom(){

            transform.position = wareroomPos;
            catMesh.sortingLayerName = "UI";
            catMesh.sortingOrder = 4;
            for(int j=0; j< Decoration.Length;j++){
                SpriteRenderer[] sprites = Decoration[j].GetComponentsInChildren<SpriteRenderer>();
                for(int i=0; i<sprites.Length;i++){
                    //sprites = Decoration[i].GetComponentsInChildren<SpriteRenderer>();
                    sprites[i].sortingLayerName = "UI";
                    sprites[i].sortingOrder = 5;
                }
            }

    }

    public void onCloseWareroom(){
        catMesh.sortingLayerName = "Character";
            catMesh.sortingOrder = 0;
            for(int j=0; j< Decoration.Length;j++){
                SpriteRenderer[] sprites = Decoration[j].GetComponentsInChildren<SpriteRenderer>();
                for(int i=0; i<sprites.Length;i++){
                    //sprites = Decoration[i].GetComponentsInChildren<SpriteRenderer>();
                    sprites[i].sortingLayerName = "Items";
                    sprites[i].sortingOrder = 0;
                }
            }
        sortlayer = false;
    }





    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Food") || col.gameObject.CompareTag("Drink") || col.gameObject.CompareTag("spItem"))
        {
            Smile();
            FdDk = col.gameObject.transform.parent.gameObject;
            var sc = FdDk.GetComponent<ObjDrag>();
            sc.DestroyFoodDrink();
            //change action to happy
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Turtle")
        {
            Debug.Log("hi turtle");
            Marine.Instance.Turtle();
        }
    }
}
