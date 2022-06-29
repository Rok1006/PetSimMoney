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

    void Awake(){
        Instance = this ;
    }
    void Start()
    {
        resetPosition = this.transform.position;
        localSpeed = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x,localSpeed*speed*i);
        i++;
        if(this.transform.position.y > 25.7f){
            this.transform.position = resetPosition;
            i=0;
        }
    }


}
