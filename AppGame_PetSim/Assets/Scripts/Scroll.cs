using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public static Scroll Instance;
    private Vector2 resetPosition;
    // Start is called before the first frame update
    private float speed = 2f;
    float i =0;
    Vector2 oldPos;

    
    void Awake(){
        Instance = this ;
        oldPos = transform.position;//this.transform.position;
    }

    void Start()
    {     
        BGscroll();
    }

    void BGscroll(){
        transform.position = new Vector2(transform.position.x,speed*i );
        i += Time.deltaTime;
        if(transform.position.y > 25f){//25f){
            transform.position = oldPos;
            i=0;
        } 

        StartCoroutine("BGWait"); 
    }
    IEnumerator BGWait(){
        yield return new WaitForSeconds(0.05f);
        BGscroll();
    }
}
