using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
     public int speed;
     public bool moveLeft;
     public bool moveRight;
     public Rigidbody2D rb;
    Vector2 movement;
    // public float speed2;
    Vector2 velocity;
    //float valueX;


    void Start()
    {
        // moveLeft = false;
        // moveRight = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);  //wont lerp  , time.deltatime
    }
    void Update() {

        //movement  = Vector2.zero;

        // if(Input.GetKey(KeyCode.A) || moveLeft ){//Left.Instance.pressleft == true
        //     movement += Vector2.left;
        //     //this.transform.Translate(new Vector2(-0.5f, 0) * speed * Time.deltaTime); 
        //     moveLeft = false;
        // }
        //  if(Input.GetKey(KeyCode.D)|| moveRight){
        //      //this.transform.position.x +=0.1f;
        //     //this.transform.Translate(new Vector2(0.5f, 0) * speed * Time.deltaTime); 
        //     // moveRight = false;
        // }
        movement = Vector2.zero;
        if (Input.GetKey(KeyCode.D) ) //RTouchMovement.Instance.pressright == true
        {
            //movement += Vector2.right;
            // anim.SetBool("Walk", true);
        }

        if (Input.GetKey(KeyCode.A) || Left.Instance.pressleft == true) //same
        {
            movement += Vector2.left;
            // anim.SetBool("Walk", true);
        }

        if (movement.x == 0)   //for animation just check the teacher sample
        {
        }
    

        // if (movement.x > 0)
        // {
        //     transform.eulerAngles = new Vector3(0, 0, 0);
        // }
        // else if (movement.x < 0)
        // {
        //     transform.eulerAngles = new Vector3(0, 180, 0);
        // }
        // if (isGrounded == true)
        // {
        //     if (spawnDust == true)
        //     {
        //         GameObject d = Instantiate(dust, EmitSpot.transform.position, Quaternion.identity);
        //         Destroy(d, 0.6f);
        //         spawnDust = false;
        //     }
        // }
        // else
        // {
        //     spawnDust = true;
        // }
       
    
    }
    public void ClickLeft(){
        moveLeft = true;
       //movement += Vector2.left;
       // this.transform.Translate(new Vector2(-0.5f, 0) * speed * Time.deltaTime); //this one is not moving smoothly
    }
    public void ClickRight(){
        // moveRight = true;
        //this.transform.Translate(new Vector2(0.5f, 0) * speed * Time.deltaTime);
    }
}

