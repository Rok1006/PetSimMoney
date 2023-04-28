using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    [SerializeField] GameObject minigame;
    float step;
    Vector3 pos;
    Vector3 edge;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        edge = pos;
        edge.x = -20f;
    }

    // Update is called once per frame
    void Update()
    {
        step = Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, edge, step);
        if(transform.position.x <= -20f){
            transform.position = pos;
        }
    }
    void OnMouseOver(){
        if(Input.GetMouseButton(0)){
            if(!minigame.activeSelf)
            {
                minigame.SetActive(true);
            }
        }
    }

}
