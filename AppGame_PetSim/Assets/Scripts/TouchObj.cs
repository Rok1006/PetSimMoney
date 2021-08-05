using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is about clciking on the dropped leafs and poop
public class TouchObj : MonoBehaviour
{
    public GameObject clickSplash;
    public GameObject SoundM;
    public SoundManager sm;
    void Start()
    {
        SoundM = GameObject.Find("SoundManager");
        sm = SoundM.GetComponent<SoundManager>();
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
            sm.Bling();
            Destroy(this.gameObject); //later: Destroy(this.gameObject, 1f)
            Destroy(c, 1f);
        }
         if(this.transform.gameObject.tag == "Rare"){
            Debug.Log("rare");
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity); 
            Status.Instance.LeafChange(CostMethod.GoldLeaf, 1);
            sm.Bling();
            Destroy(this.gameObject);
            Destroy(c, 1f);
        }
         if(this.transform.gameObject.tag == "Trash"){
            Debug.Log("trash"); 
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity);
            sm.Trash();
            Destroy(this.gameObject);
            Destroy(c, 1f);
        }
    }
    
    void Detect(){
        }
    }

