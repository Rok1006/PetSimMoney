using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
public static ObjDrag Instance;
private bool selected;
public float dist;
Rigidbody2D rb;
private GameObject thisObj;
public GameObject explode;
public GameObject hungerEx;
public GameObject hydraEx;
public int statValueAdded;
private bool getAdd = true;
void Awake() {
    Instance = this;
}
 void Start() {
      dist = transform.position.z - Camera.main.transform.position.z;
       rb = GetComponent<Rigidbody2D>();
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
    Debug.Log("over");
    if(Input.GetMouseButton(0)){
        selected = true;
    }
}
//Others function non related to drag drop
 public void GetToyPos(){
     if(this.gameObject.tag == "Toy"){
        CatAI.Instance.currentToyPos = this.gameObject.transform.position;
        Debug.Log(CatAI.Instance.currentToyPos);
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
        if(Status.Instance.happyV<100){
            Status.Instance.happyV += statValueAdded;
        }
        GameObject c = Instantiate(explode, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        CatAI.Instance.toy.Remove(this.gameObject);
        Destroy(c, 1f);
    }
}
public void DestroyFoodDrink(){
    if(this.gameObject.tag == "Food"){
        if(Status.Instance.hungerV<100){
            Status.Instance.hungerV += statValueAdded;
        }
        GameObject c = Instantiate(hungerEx, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(c, 5f);
        //trigger animation: action = ?
     }
    if(this.gameObject.tag == "Drink"){
        if(Status.Instance.hydrationV<100){
            Status.Instance.hydrationV += statValueAdded;
        }
        GameObject c = Instantiate(hydraEx, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject); 
        Destroy(c, 5f);
        //trigger animation: action = ?
    }
}
public void DetectToy(){
        Debug.Log(this.gameObject.tag);
        GetToyPos(); //get the pos of the toy when drop on the floor
        CatAI.Instance.action = 6; //do playing
        CatAI.Instance.p = 0; //stop normal state
        //CatAI.Instance.executing = true;
}
void OnCollisionEnter2D(Collision2D col) {
    if(this.gameObject.tag == "Toy"){  
        if (col.gameObject.CompareTag("ItemFloor"))
        {
             if(getAdd){ //boolean: ADD TO LIST ONLY ONCE
                     GameObject t = this.gameObject; //add it into list 
                    CatAI.Instance.toy.Add(t);
                    getAdd = false;
                }
           if(!CatAI.Instance.isPlayingToy){ //still glitching need another way
                DetectToy();
           }
            
        }
    }
}
void OnTriggerEnter2D(Collider2D col) {
  
}


}
