using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{

    public static SceneryManager Instance;
    public GameObject[] scene = new GameObject[2];//0:house 1:outside
    
    bool switchbool = false;
    public int currentPlanet = 0;
    int _currentPlanet;
    [Header("Effect")]
    public GameObject star;
    public GameObject bubble;
    [Header("Furnitres")]
    public GameObject CatTree;
    //public point[] = CatTree.transform.GetChild().gameObject;

    void Awake(){
        Instance = this;
    }

    void Update(){
    }

    public void switchPlanet(int _currentPlanet){
        Debug.Log("_currentPlanet "+_currentPlanet);
        scene[_currentPlanet].SetActive(true);
        scene[currentPlanet].SetActive(false);
        currentPlanet = _currentPlanet;
    }


    public void switchScene(){
        if(switchbool){
        scene[currentPlanet].SetActive(true);
        switchbool = false;
        }else{
        scene[currentPlanet].SetActive(false);
        switchbool = true;
        }   
    }
    public Vector3 CatTreePos(int i){
        Vector3 point = CatTree.transform.GetChild(i).gameObject.transform.position;
        return point;
    }

    public bool outside(){
        return scene[currentPlanet].activeSelf;
    }

}
