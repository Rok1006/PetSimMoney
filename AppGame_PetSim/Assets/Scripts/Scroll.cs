using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public static Scroll Instance;
    private Vector2 resetPosition;
    // Start is called before the first frame update
    private float speed = 2f;
    private float localSpeed;
    int i =0;
    float y;
    Vector2 oldPos;
    [SerializeField] bool BG = false;
    [SerializeField] bool status = false;

    void Awake(){
        Instance = this ;
        oldPos = this.transform.position;
        localSpeed = Time.deltaTime;
    }


    // Update is called once per frame
    void Start()
    {
        if(BG){
            BGscroll();
        }else if(status){
            Statusscroll();
        }
    }

    void Statusscroll(){
        y = Status.Instance.happyV;
        this.transform.position = new Vector2( oldPos.x, oldPos.y + y);
    }

    void BGscroll(){
        this.transform.position = new Vector2(this.transform.position.x,localSpeed*speed*i);
        i++;
        if(this.transform.position.y > 25.7f){
            this.transform.position = oldPos;
            i=0;
        } 
        StartCoroutine("BGWait"); 
    }
    IEnumerator StatusWait(){
        yield return new WaitForSeconds(0.2f);
        Statusscroll();
    }

    IEnumerator BGWait(){
        yield return new WaitForSeconds(0.2f);
        BGscroll();
    }

}
