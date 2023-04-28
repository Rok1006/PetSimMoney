using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    SpriteRenderer image;
    // Start is called before the first frame update
    void Start(){
        image = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown(){
        image.color =  new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f),1f);
    }
}
