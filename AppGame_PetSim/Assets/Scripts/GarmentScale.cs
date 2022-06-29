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
    void Update()
    {
        if(isBigger){
            sizeValue = CatAI.Instance.sizeValue * sizeIndex *2f;
        }else{
            sizeValue = CatAI.Instance.sizeValue * sizeIndex;
        }
        transform.localScale = new Vector3(sizeValue * offsetX,sizeValue,sizeValue);
    }
}
