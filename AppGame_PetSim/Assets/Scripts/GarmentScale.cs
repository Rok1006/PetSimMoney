using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentScale : MonoBehaviour
{
    private float sizeIndex = 6f;
    private float sizeValue;
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] private bool isBigger;
    [SerializeField] private float offsetX = 1;
    [SerializeField] private float offsetY = 1;

    [SerializeField] private float offsetXX = 0;
    [SerializeField] private float offsetYY = 0;

    void Start(){
        transform.position = new Vector2(transform.position.x+offsetXX ,transform.position.y+offsetYY);
    }
    void Update()
    {
        if(isBigger){
            sizeValue = CatAI.Instance.sizeValue * sizeIndex *2f;
        }else{
            sizeValue = CatAI.Instance.sizeValue * sizeIndex;
        }
        transform.localScale = new Vector3(sizeValue * offsetX,sizeValue* offsetY,sizeValue);

        // /transform.position = new Vector2(transform.position.x+offsetXX ,transform.position.y+offsetYY);
    }
}
