using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Left : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public bool pressleft = false;
    public GameObject Player;
    public static Left Instance;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData){
        pressleft = true;
    }
     public void OnPointerUp(PointerEventData eventData){
        pressleft = false;
    }
}
