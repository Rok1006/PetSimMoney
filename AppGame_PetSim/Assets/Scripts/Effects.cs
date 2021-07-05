using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Effects : MonoBehaviour
{
  public GameObject cS;
    public Vector3 mousePos;
    Camera m_MainCamera;
    public static Effects Instance;
    public GameObject clickSpark;
    public GameObject cleanblings; //clean particles
    public GameObject dirtyblings; //dirty particles
    public ParticleSystem happyheart;
    public GameObject HeartEmit;

    void Awake(){
        Instance = this;
    }
    void Start()
    {
        //clickSpark.SetActive(false);
        cleanblings.SetActive(true);
        dirtyblings.SetActive(false);
    }

    void Update()
    {
        SceneEffectChange();
        // if(Input.GetMouseButton(0)){  //Input.GetKey(KeyCode.Space)
        // Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos = worldMousePos;
        // Debug.Log(mousePos);
        // //clickSpark.SetActive(true);
        // //mousePos.y = 1;
        // //clickSpark.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        // GameObject c = Instantiate(cS, mousePos, Quaternion.identity);
        // c.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        // //cS.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        // }


    }
    void SceneEffectChange(){
        if(ItemsDrop.Instance.totalTrashDropped>5){ //10
            cleanblings.SetActive(false);
            dirtyblings.SetActive(true);
        }else{
            cleanblings.SetActive(true);
            dirtyblings.SetActive(false);
        }
    }
    public void HappyEmittion(){
        happyheart.transform.position = HeartEmit.transform.position;
        happyheart.Emit(5);
    }
}
