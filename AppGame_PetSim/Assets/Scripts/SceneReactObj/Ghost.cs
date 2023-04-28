using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static Ghost Instance;
    public GameObject target;
    float fadeOutTime = 3f;
    public GameObject[] ghostObj;
    public GameObject[] normalFace;
    public GameObject[] ScaryFace;
    List<GameObject> prefabsFace = new List<GameObject>();
    List<bool> isChange = new List<bool>();

    GameObject prefabsFolder;
    List<GameObject> prefabs = new List<GameObject>();
    List<GameObject> prefabsTarget = new List<GameObject>();
    float x,y,_x;
    int i = 0;
    
    List<Vector3> newPrefabsPos = new List<Vector3>();
    List<Vector3> newTargetPos = new List<Vector3>();
    List<Vector3> currentPos = new List<Vector3>();

    void Awake(){
        Instance = this;
    }
    // Update is called once per frame
    void Start(){
        GameObject prefabsFolder = this.transform.GetChild(0).gameObject;
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
            currentPos[j] = prefabs[j].transform.position;
            if(newTargetPos[j] == currentPos[j]){ //reach goal point
                prefabs[j].transform.position = newPrefabsPos[j];
                    Destroy(prefabsFace[j]);
                    GameObject tmp = prefabs[j].transform.GetChild(0).gameObject;
                    prefabsFace[j]=Instantiate(RanFace(),prefabsFace[j].transform.position,Quaternion.identity,tmp.transform.parent) as GameObject;
                    SpriteRenderer[] sprites = prefabs[j].GetComponentsInChildren<SpriteRenderer>();
                    isChange[j]=false;
                    for(int i = 0; i < sprites.Length; i++){
                        Color tmpColor = sprites[i].color;
                        tmpColor.a = 1f;
                        sprites[i].color = tmpColor;
                }
            }else{
                prefabs[j].transform.position = Vector3.MoveTowards(prefabs[j].transform.position, newTargetPos[j], Time.deltaTime);
        }
        }
        //dev cheat
        if(Input.GetKeyDown(KeyCode.T))
        {
            Spawn();
        }
        
    }
    void Spawn(){
        isChange.Add(false);
        x =  Random.Range(-1.41f, 16.91f);
        newPrefabsPos.Add(new Vector3(x,-4f,transform.position.z));
        prefabs.Add(Instantiate(RanChar(),newPrefabsPos[i],Quaternion.identity,transform.parent) as GameObject);
        GameObject tmp = prefabs[i].transform.GetChild(0).gameObject;
        prefabsFace.Add(Instantiate(RanFace(),newPrefabsPos[i],Quaternion.identity,tmp.transform.parent) as GameObject);
        prefabsFace[i].transform.position = tmp.transform.position;
        currentPos.Add(newPrefabsPos[i]);
        prefabs[i].name = i.ToString();
        _x =  Random.Range(-1.41f, 16.91f);
        newTargetPos.Add(new Vector3(_x,10f,transform.position.z));
        prefabsTarget.Add(Instantiate(target,newTargetPos[i],Quaternion.identity,this.transform.parent) as GameObject);
        if (_x > x){ // direction:looking right
            prefabs[i].transform.localScale = new Vector3(-prefabs[i].transform.localScale.x, prefabs[i].transform.localScale.y, prefabs[i].transform.localScale.z);
        }
        i++;
    }

    public void NewFace(int num){
        if(!isChange[num]){
            isChange[num]=true;
            Destroy(prefabsFace[num]);
            GameObject tmp = prefabs[num].transform.GetChild(0).gameObject;
            prefabsFace[num]=Instantiate(RanScareFace(),prefabsFace[num].transform.position,Quaternion.identity,tmp.transform.parent) as GameObject;
            doFadeOUT(num); 
        }
        
    }

    public void doFadeOUT(int num){
        SpriteRenderer[] sprites = prefabs[num].GetComponentsInChildren<SpriteRenderer>();
        //StartCoroutine (spriteRanColor(sprites[0]));
        //sprites[0].color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1f);
        StartCoroutine (spriteFadeOUT(sprites[0]));
        StartCoroutine (spriteFadeOUT(sprites[sprites.Length - 1]));
        
    }
    GameObject RanChar(){
        int n = Random.Range(1,ghostObj.Length);
        return ghostObj[n];
    }
    GameObject RanFace(){
        int n = Random.Range(1,normalFace.Length);
            return normalFace[n];
    }
    GameObject RanScareFace(){
            int n = Random.Range(1,ScaryFace.Length);
            return ScaryFace[n];   
    }
    IEnumerator NewChar()
    { //for animation idle, sitsleep,sitawake, meow
        int t = Random.Range(0, 10);
        yield return new WaitForSeconds(t);//wait for 5 sec to do the next
        Spawn();
    }

    IEnumerator spriteFadeOUT(SpriteRenderer _sprite){
        Color tmpColor = _sprite.color;
        while(tmpColor.a > 0f){
            tmpColor.a -= Time.deltaTime /fadeOutTime;
            _sprite.color = tmpColor;
            if(tmpColor.a <= 0f)
                tmpColor.a = 0.0f;
            yield return null;
        }
        _sprite.color = tmpColor;       
    }
}
