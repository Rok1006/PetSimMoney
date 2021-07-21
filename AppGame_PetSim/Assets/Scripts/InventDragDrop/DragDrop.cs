using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
//put this in ui that is being dragged
//also when items is dragged out of invent, it becomes gameobject

public class DragDrop : MonoBehaviour, IInitializePotentialDragHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
  //private CanvasGroup canvasGroup;
    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;
    public bool FollowCursor {get; set;} = true;
    public Vector3 StartPosition;
    public bool CanDrag { get; set;} = true;
//Detect overlapping and dropping items to cat stuff
    public RectTransform thisUI;  //this UI
    public RectTransform dropPlace; //bg slot UI
    public bool inDropArea;
    Vector3 mousePos;
    private bool generate;
    public float dist;
    private void Awake(){
        rectTransform = GetComponent<RectTransform>(); 
        // canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start() {
        inDropArea = false;
        generate = false;
        dist = transform.position.z - Camera.main.transform.position.z;
    }
     public void OnInitializePotentialDrag(PointerEventData eventData){
        // Debug.Log("begindragging");
        // canvasGroup.alpha = .7f;
        // canvasGroup.blocksRaycasts = false;
        StartPosition = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData){
        if(!CanDrag){
            return;
        }
        OnBeginDragHandler?.Invoke(eventData);
        // Debug.Log("begindragging");
        // canvasGroup.alpha = .7f;
        // canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData){
         if(!CanDrag){
            return;
        }
        OnDragHandler?.Invoke(eventData);
        if(FollowCursor){
            rectTransform.anchoredPosition += eventData.delta/1.2f; //not very accurate
        }
        //Debug.Log(canvas.scaleFactor);
        // rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor; //     can divide the canvas scale after delta if not accurate: / canvas.scaleFactor
    }
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("releaseed");
        // canvasGroup.alpha = 1f;
        // canvasGroup.blocksRaycasts = true;
        if (!CanDrag)
		{
			return;
		}

		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);

		DropArea dropArea = null;

		foreach (var result in results)
		{
			dropArea = result.gameObject.GetComponent<DropArea>();

			if (dropArea != null)
			{
				break;
			}
		}
		if (dropArea != null)
		{
			if (dropArea.Accepts(this))
			{
				dropArea.Drop(this);
				OnEndDragHandler?.Invoke(eventData, true);
				return;
			}
		}
        if(!inDropArea){
            rectTransform.anchoredPosition = StartPosition; //create bool here 
            OnEndDragHandler?.Invoke(eventData, false);
        }
        if(inDropArea){
            Debug.Log("Given");
            InsObjOnMousePos();
            //if it is a toy, send location info to other script
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        // Debug.Log("Clicked");
    }
    void Update() {
         if (rectOverlaps(dropPlace, thisUI)) //just change them oppositely to work better
        {
           //Debug.Log("HoveringInInventory");
           inDropArea = false;
        }else{
            //Debug.Log("GivingtoCat");
            inDropArea = true;
        }
    }
     bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);
        
        return rect1.Overlaps(rect2);
    }

    void InsObjOnMousePos(){
        //if(Input.GetMouseButton(0)){  //Input.GetKey(KeyCode.Space)
        if(Input.GetMouseButtonUp(0)){
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = worldMousePos; //final pos after release click
            mousePos.z = dist;
            Debug.Log(mousePos); //some how the object always instantiate at the center
            generate = true;
            if(generate){
                //Debug.Log("ItemID: " + ItemsInfo.Instance.itemID); //make sure it only do once
                //Debug.Log("ItemName: " + ItemsInfo.Instance.itemName); //make sure it only do once
                var sc = this.gameObject.GetComponent<ItemsInfo>();
                GameObject i = Instantiate(sc.generatedItem, mousePos, Quaternion.identity);
                i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y, 0);
                Destroy(this.gameObject); //destroy this ui
                //Inventory.instance.fullslot -= 1;
                generate = false;
            }
            
        }
    }

  
    
}
