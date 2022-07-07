using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
public GameObject emitSpot;
public ItemsInfo info;
private bool selected;
public float dist;
Rigidbody2D rb;
private GameObject thisObj;
public GameObject explode;
public GameObject hungerEx;
public GameObject hydraEx;
public GameObject plusHeart;
private bool getAdd = true;
 public GameObject SoundM;
public SoundManager sm;

void Start() {
    dist = transform.position.z - Camera.main.transform.position.z;
    rb = GetComponent<Rigidbody2D>();
    emitSpot = GameObject.Find("HeartEmit");
    SoundM = GameObject.Find("SoundManager");
    sm = SoundM.GetComponent<SoundManager>();
}
 //dragdrop
void Update(){
    if(selected == true){
        var pos = Input.mousePosition;
        pos.z = dist;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
 
    if(Input.GetMouseButtonUp(0)){
        selected = false;
    }
}
void OnMouseOver(){
if(CatAI.Instance.currentPos != CatAI.Instance.currentToyPos){
    if(Input.GetMouseButton(0)){
        selected = true;
    }}

}
//Others function non related to drag drop
 public void GetToyPos(){
     if(this.gameObject.tag == "Toy"){
        CatAI.Instance.currentToyPos = this.gameObject.transform.position;
     }
    }
public void ToySnap(){
    if(this.gameObject.tag == "Toy"){
    this.gameObject.transform.position = CatAI.Instance.itemPlaceHolder.transform.position;
    this.gameObject.transform.parent = CatAI.Instance.itemPlaceHolder.transform; 
    rb.gravityScale = 0;
    }
}
public void DestroyToy(){
    if(this.gameObject.tag == "Toy"){
        info.Use();
        GameObject c = Instantiate(explode, this.transform.position, Quaternion.identity);
        GameObject d = Instantiate(plusHeart, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        CatAI.Instance.toy.Remove(this.gameObject);
        Destroy(c, 1f);
        Destroy(d, 5f);
    }
}
public void DestroyFoodDrink(){
    if(this.gameObject.tag == "Food"){
        info.Use();
        GameObject c = Instantiate(hungerEx, emitSpot.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(c, 5f);
        //trigger animation: action = ?
     }else if(this.gameObject.tag == "Drink"){
        info.Use();
        GameObject c = Instantiate(hydraEx, emitSpot.transform.position, Quaternion.identity);
        Destroy(this.gameObject); 
        Destroy(c, 5f);
        //trigger animation: action = ?
    }else{
        info.Use();
        Destroy(this.gameObject);
    }
}
public void DetectToy(){
        CatAI.Instance.Dashing(); //do playing
        //CatAI.Instance.p = 0; //stop normal state
        //CatAI.Instance.executing = true;
}
void OnCollisionEnter2D(Collision2D col) {
    if(this.gameObject.tag == "Toy"){ 
        if (col.gameObject.CompareTag("ItemFloor"))
        {
            GetToyPos();
            sm.ToyFall();
             if(getAdd){ //boolean: ADD TO LIST ONLY ONCE
                     GameObject t = this.gameObject; //add it into list 
                    CatAI.Instance.toy.Add(t);
                    getAdd = false;
                }
                if(!CatAI.Instance.getToy){
                    GetToyPos();
                }
           if(!CatAI.Instance.isPlayingToy){ //still glitching need another way
                DetectToy();
           }
            
        }
    }
    if(this.gameObject.tag == "Drink"){  
        if (col.gameObject.CompareTag("ItemFloor"))
        {
            sm.DrinkFall();
        }
    }
      if(this.gameObject.tag == "Food"){  
        if (col.gameObject.CompareTag("ItemFloor"))
        {
            sm.FoodFall();
        }
    }
}

void OnTriggerEnter2D(Collider2D col) {
  
}


}
