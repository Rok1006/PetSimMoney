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
    public GameObject whiteScene;

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
        if(Input.GetMouseButtonUp(0)){
            clickEffect();
        }  
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

    public void changeScene(){
        
    }

   public void clickEffect(){
        var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        GameObject c = Instantiate(clickSpark, Camera.main.ScreenToWorldPoint(screenPoint), Quaternion.identity);
        Destroy(c, 1f);
    }

    public void HappyEmittion(){
        happyheart.transform.position = HeartEmit.transform.position;
        happyheart.Emit(1);
    }
}
