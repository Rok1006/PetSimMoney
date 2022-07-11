using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{

    public static SceneryManager Instance;
    public GameObject[] scene = new GameObject[2];//0:house 1:outside
    [Header("Effect")]
    public GameObject star;
    public GameObject bubble;
    [Header("Furnitres")]
    public GameObject CatTree;
    //public point[] = CatTree.transform.GetChild().gameObject;

    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scene[0].SetActive(true);
        star.SetActive(true);
        scene[1].SetActive(false);
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(){
        if(scene[0].activeSelf){
            scene[0].SetActive(false);
            star.SetActive(false);
            scene[1].SetActive(true);
            bubble.SetActive(true);
        }else{
            scene[0].SetActive(true);
            star.SetActive(true);
            scene[1].SetActive(false);
            bubble.SetActive(false);
        }
    }

    public Vector3 CatTreePos(int i){
        Vector3 point = CatTree.transform.GetChild(i).gameObject.transform.position;
        return point;
    }
}
