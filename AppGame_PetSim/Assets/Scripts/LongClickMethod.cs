using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickMethod : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown = false;
    bool switchCooldown;
    float pointerDownTimer;
    [SerializeField] float requiredHoldTime = 0.5f;
    public UnityEvent onLongClick;
    public UnityEvent onShortClick;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void OnPointerDown(PointerEventData eventData){
        pointerDown = true;
    }
    public void OnPointerUp(PointerEventData eventData){
        //Reset();
        pointerDown = false;
    }
    // Update is called once per frame
    void Update()
    {

        if(pointerDown){
            pointerDownTimer += Time.deltaTime;
            switchCooldown = true;
            if(pointerDownTimer >= requiredHoldTime){       //Long press
                if(onLongClick != null){
                    onLongClick.Invoke(); 
                }               
                Reset();
            }
        }else if(switchCooldown){
            if(onShortClick != null){
                onShortClick.Invoke(); 
            } 
            Reset();
        }
    }


    void Reset(){
        pointerDown = false;
        pointerDownTimer = 0;
        switchCooldown = false;
    }

    public void OnClickItem()
    {
        if(Inventory.instance.PopUpItem != null)
        {
            Inventory.instance.itemSelected = gameObject;
            Inventory.instance.OnItemUseClick();
        }
    }

    public void itemInfo()
    {
        if(Inventory.instance.PopUpItem != null)
        {
            GameObject pop = Inventory.instance.PopUpItem;
            pop.transform.Find("ItemUIPlacement").gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            pop.transform.Find("ItemName").gameObject.GetComponent<Text>().text = gameObject.GetComponent<ItemsInfo>().itemName;
            pop.transform.Find("Descript").gameObject.GetComponent<Text>().text = gameObject.GetComponent<ItemsInfo>().description;
            Inventory.instance.itemSelected = gameObject;
            pop.SetActive(true);
        }
    }

}
