using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : MonoBehaviour
{
    // Start is called before the first frame update
    public static Marine Instance;
    public GameObject target;
    public GameObject KingSquid;
    public GameObject face1, face2;

    Vector3 oldPos, Pos;
    public GameObject[] marineObj;//jellyfish, squid, octopus;   
    public List<float> speed = new List<float>();

    public int squidInt;
    public GameObject[] inkRangePT;
    public GameObject[] ink;
    List<GameObject> INK = new List<GameObject>();

    public GameObject turtleFace1;
    public GameObject turtleFace2;
    public GameObject turtleHand1;
    public GameObject turtleHand2;


    [SerializeField] GameObject prefabsFolder;
    List<GameObject> prefabs = new List<GameObject>();
    List<GameObject> prefabsTarget = new List<GameObject>();
    float x,y,_x,z;
    int i = 0;

    List<Vector3> newPrefabsPos = new List<Vector3>();
    List<Vector3> newTargetPos = new List<Vector3>();

   // private IEnumerator coroutine;

    void Awake(){
        Instance = this;
    }
    // Update is called once per frame
    void Start(){
        oldPos = transform.position;
        StartCoroutine("NewChar");
        StartCoroutine("NewChar");
        StartCoroutine("NewChar");
        StartCoroutine("NewChar");
        StartCoroutine("NewChar");
        StartCoroutine("NewChar");
        

    }
    void Update()
    {
        for(int j = 0; j < i; j++){
            if(newTargetPos[j] == prefabs[j].transform.position){ //reach goal point
                prefabs[j].transform.position = newPrefabsPos[j];
                speed[j] = 1f;
            }else{
                prefabs[j].transform.position = Vector3.MoveTowards(prefabs[j].transform.position, newTargetPos[j], speed[j] * Time.deltaTime);
        }
        }

        if(KingSquid.activeSelf){
            Pos = CatAI.Instance.currentPos;
            Pos.y =-1f;
            KingSquid.transform.position = Vector3.MoveTowards(KingSquid.transform.position,Pos, Time.deltaTime*5f);
        }
        
        //dev cheat
        
    }

    void Spawn(){
        speed.Add(1f);
        x =  Random.Range(-1.41f, 16.91f);
        newPrefabsPos.Add(new Vector3(x,-4f,transform.position.z));
        prefabs.Add(Instantiate(RanChar(),newPrefabsPos[i],Quaternion.identity,transform.parent) as GameObject);
        prefabs[i].name = i.ToString();
        _x =  Random.Range(-1.41f, 16.91f);
        newTargetPos.Add(new Vector3(_x,10f,transform.position.z));
        prefabsTarget.Add(Instantiate(target,newTargetPos[i],Quaternion.identity,this.transform.parent) as GameObject);
        i++;
    }

    GameObject RanChar(){
        int n = Random.Range(0,marineObj.Length);
        return marineObj[n];     
    }

    GameObject RanInk(){
        int n = Random.Range(0,ink.Length);
        return ink[n];   
    }
    IEnumerator NewChar()
    { //for animation idle, sitsleep,sitawake, meow
        int t = Random.Range(0, 10);
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        Spawn();
    }

    public IEnumerator KingSquidAnim(){
        face1.SetActive(false);
        face2.SetActive(true);
        z = ink[0].transform.position.z;
        StartCoroutine("Wait1s");
        yield return new WaitForSeconds(5);
        StopCoroutine("Wait1s");
        KingSquid.transform.position = oldPos;
        KingSquid.SetActive(false);
        squidInt = 0;
        face1.SetActive(true);
        face2.SetActive(false);
    }
    IEnumerator Wait1s(){
        yield return new WaitForSeconds(0.2f);//wait for 5 sec to do the next
        x = Random.Range(inkRangePT[0].transform.position.x , inkRangePT[2].transform.position.x);
        y = Random.Range(inkRangePT[0].transform.position.y , inkRangePT[1].transform.position.y);
        INK.Add(Instantiate(RanInk(),new Vector3(x,y,0f),Quaternion.identity,prefabsFolder.transform.parent) as GameObject);
        yield return StartCoroutine("Wait1s");
    }
    public void Turtle(){
        turtleFace1.SetActive(false);
        turtleFace2.SetActive(false);
        turtleHand1.SetActive(false);
        turtleHand2.SetActive(false);
        StartCoroutine("angryTurtle");
    }
    IEnumerator angryTurtle(){
        
        yield return new WaitForSeconds(5);
        turtleFace2.SetActive(true);
        turtleHand1.SetActive(true);
        turtleHand2.SetActive(true);
        yield return new WaitForSeconds(10);
        turtleFace1.SetActive(true);
        turtleFace2.SetActive(false);
    }
}
