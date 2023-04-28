using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    Vector3 Pos;
    Rigidbody2D rb;

    public void gameStart() {
        Pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.left * 50;
    }
}
