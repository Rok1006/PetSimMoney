using System.Collections;
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
    public static CatAI Instance;
    Animator catanim;
    public GameObject target;
    public GameObject itemPlaceHolder;
    public GameObject FdDk;
     public List<GameObject> toy = new List<GameObject>();
    [Header("Value")]
    public int action;
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
    public enum PossibleState    //the each state, when player do some function to interrupt this changes to diff state
   { IDLE,Walking,Sleeping,Awake,Meowing
   };
   public PossibleState currentState = PossibleState.IDLE; //not using it currently
   void Awake() {
       Instance = this;
   }
       void Start()
    {
        catanim = this.GetComponent<Animator>();
        executing = true;
        action = 1; //default state is IDLE
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
            case 1: //idle
            isPlayingToy = false;
            canEatDrink = true;
            canTouch = true;
            Status.Instance.HydrationDecrease();
            Status.Instance.HungerDecrease();
            Status.Instance.HappinessDecrease();
                if(executing){
                catanim.SetTrigger("idle");
                StartCoroutine("SwitchAct"); 
                executing = false;
                } break;
            case 2: //walking
            isPlayingToy = false;
            canEatDrink = false;
            canTouch = false;
            Status.Instance.HydrationDecrease();
            Status.Instance.HungerDecrease();
            Status.Instance.HappinessDecrease();
            Status.Instance.EnergyDecrease();
                float step = speed * Time.deltaTime; //
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);//
                if(currentPos == newPos){ //if cat arrive at the new pos
                currentPos = oldPos; //that current pos become the old positon
                RanPos(); //do random again
                }
                if(executing){
                StartCoroutine("Walk");
                executing = false;
                } break;
            case 3: //sitsleeping
            isPlayingToy = true;//turn true to stop it from detecting
            canEatDrink = false;
            canTouch = false;
            Status.Instance.HydrationDecrease();
            Status.Instance.EnergyIncrease();
                if(executing){
                catanim.SetTrigger("sitsleep");
                StartCoroutine("SwitchAct");
                executing = false;
                } break;
            case 4: //sitawake
            isPlayingToy = true;
            canEatDrink = true;
            canTouch = false;
            Status.Instance.HydrationDecrease();
            Status.Instance.HungerDecrease();
            Status.Instance.HappinessDecrease();
            Status.Instance.EnergyIncrease();
                if(executing){
                catanim.SetTrigger("sitawake");
                StartCoroutine("SwitchAct");
                executing = false;
                } break;
            case 5: //meowing
            isPlayingToy = false;
            canEatDrink = true;
            canTouch = true;
            Status.Instance.HydrationDecrease();
            Status.Instance.HungerDecrease();
            Status.Instance.HappinessDecrease();
            Status.Instance.EnergyDecrease();
                if(executing){
                catanim.SetTrigger("meow");
                StartCoroutine("SwitchAct");
                executing = false;
                } break;
            case 6: //dashing
            executing = true;
            canTouch = false;
            canEatDrink = false;
            isPlayingToy = true;
                 catanim.SetTrigger("dash");
                 action = 7;
            break;
            case 7: //playing
            Status.Instance.HydrationDecrease();
            Status.Instance.HungerDecrease();
            Status.Instance.EnergyDecrease();
            isPlayingToy = true;
            canEatDrink = false;
            canTouch = false;
            doNormal = false; //for when playing is executing, normal switch dont execute
                MoveTowardToy();
                if(currentPos == currentToyPos){ //if cat arrive at the toy pos, do play animation
                      for (int i = 0; i < toy.Count; i++) //go through the list
                {
                    if(toy[i]!= null){
                         var sc = toy[i].GetComponent<ObjDrag>();
                        sc.ToySnap(); 
                        break;
                    }
                }
                     if(executing){
                    StartCoroutine("Playing");
                       executing = false;
                    } 
                } break;
           }
        } //beingtouch
    }
    void RanPos(){  	
        x =  Random.Range(-1.41f, 16.91f);   //-2,29
        y = Random.Range(-6.3f, -2.78f);   //-12.8,-4
        target.transform.position = new Vector2(x, y);
        newPos = target.transform.position; //inserting the new random pos into this newPos variable
    }
    public void NormalState(){ //randomize action, change probability when more animation
        if(doNormal){
        p = Random.Range(0,100); //int p = 
        if(p>0 && p<30){
            action = 2;
        }else if(p>=30 && p<43){
            action = 1;
        }else if(p>=43 && p<62){
            action = 3;
        }else if(p>=62 && p<81){
            action = 4;
        }else if(p>=81 && p<100){
            action = 5;
        }//action = Random.Range(1,6); //uncomment this for simpler randomize
            executing = true; //turn true again 
        }
    }
    public void NeedyState(){ //when hydration and want food
    }
    public void BoredSleepyState(){ //when happiness and energy is low
    }
    IEnumerator SwitchAct(){ //for animation idle, sitsleep,sitawake, meow
        int t = Random.Range(5,10);
        Debug.Log(t);
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        NormalState();//after wait for 5 sec do random generate, detect value to change to different state
        CheckToyList();
    }
    IEnumerator Walk(){ //walking
        catanim.SetTrigger("walk");
        RanPos();
        yield return new WaitForSeconds(10);//wait for 5 sec to do the next
        NormalState(); 
        CheckToyList();
    }
    IEnumerator Playing(){ //playing
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
    void CheckToyList(){
              for (int i = 0; i < toy.Count; i++)
                {
                    if(toy[i]!= null){
                        Debug.Log("did");
                       var sc = toy[i].GetComponent<ObjDrag>();
                        sc.DetectToy(); //works 
                    }
                }
    }
    void stopMeow(){ //end state
        catanim.SetTrigger("stopmeow");
    }
     void Stand(){ //end state
        catanim.SetTrigger("stand");
    }
    void MoveTowardToy(){
        float step1 = speed * Time.deltaTime; //
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentToyPos, step1);//
    }
    void OnMouseDown(){
        if(canTouch){
        beingTouch = true;
        catanim.SetTrigger("touch");
        Effects.Instance.HappyEmittion();
        Status.Instance.valueUP = true;
        Status.Instance.HappinessIncrease();
        }
    }
    void OnMouseUp(){
        beingTouch = false; 
        canTouch = false;
        Status.Instance.valueUP = false;
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Food") || col.gameObject.CompareTag("Drink"))
        {
            FdDk = col.gameObject.transform.parent.gameObject;
            var sc = FdDk.GetComponent<ObjDrag>();
            sc.DestroyFoodDrink();
        }
    }
   
}
