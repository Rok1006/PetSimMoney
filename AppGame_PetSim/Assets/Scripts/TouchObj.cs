using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is about clciking on the dropped leafs and poop
public class TouchObj : MonoBehaviour
{
    public GameObject clickSplash;
    void Start()
    {
        
    }
    void Update()
    {
   
    }
    void OnMouseDown(){
    //Debug.Log("yup");
        if(this.transform.gameObject.tag == "Normal"){
            Debug.Log("normal"); 
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity);
            Status.Instance.LeafChange(CostMethod.GreenLeaf, 1);
            Destroy(this.gameObject); //later: Destroy(this.gameObject, 1f)
            Destroy(c, 1f);
            SoundManager.Instance.Bling();
        }
         if(this.transform.gameObject.tag == "Rare"){
            Debug.Log("rare");
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity); 
            Status.Instance.LeafChange(CostMethod.GoldLeaf, 1);
            Destroy(this.gameObject);
            Destroy(c, 1f);
            SoundManager.Instance.Bling();
        }
         if(this.transform.gameObject.tag == "Trash"){
            Debug.Log("trash"); 
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(c, 1f);
           SoundManager.Instance.Trash();
        }
    }
    
    void Detect(){
        }
    }

