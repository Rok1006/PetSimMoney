using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is about clciking on the dropped leafs and poop
public class TouchObj : MonoBehaviour
{
    public GameObject clickSplash;
    public GameObject exp;
    public GameObject plusgreen;
    public GameObject plusgold;
    public GameObject SoundM;
    public SoundManager sm;
    void Start()
    {
        SoundM = GameObject.Find("SoundManager");
        sm = SoundM.GetComponent<SoundManager>();
        //Invoke("DestroyAuto", 5f);
    }
    void Update()
    {
        if(this.transform.gameObject.tag == "Air"){
            Destroy(this.gameObject, 2f); //later: Destroy(this.gameObject, 1f)
        }
          if(this.transform.gameObject.tag == "Normal"){ //Auto destroy after certain time
            Destroy(this.gameObject, 7f); //later: Destroy(this.gameObject, 1f)
        }else if(this.transform.gameObject.tag == "Rare"){
            Destroy(this.gameObject, 7f); //later: Destroy(this.gameObject, 1f)
        }else if(this.transform.gameObject.tag == "Trash"){
            Destroy(this.gameObject, 7f); //later: Destroy(this.gameObject, 1f)
        }
    }
    void OnMouseDown(){
    //Debug.Log("yup");
        if(this.transform.gameObject.tag == "Normal"){
            Debug.Log("normal"); 
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity);
            GameObject d = Instantiate(plusgreen, this.transform.position, Quaternion.identity);
            Status.Instance.LeafChange(CostMethod.GreenLeaf, 1);
            sm.Bling();
            Destroy(this.gameObject); //later: Destroy(this.gameObject, 1f) //put here or on top of destroy
            Destroy(d, 5f);
            Destroy(c, 1f);
        }
         if(this.transform.gameObject.tag == "Rare"){
            Debug.Log("rare");
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity); 
            GameObject d = Instantiate(plusgold, this.transform.position, Quaternion.identity);
            Status.Instance.LeafChange(CostMethod.GoldLeaf, 1);
            sm.Bling();
            Destroy(this.gameObject); //put here or on top of destroy
            Destroy(c, 1f);
            Destroy(d, 5f);
        }
         if(this.transform.gameObject.tag == "Trash"){
            User.Instance.ExpUP(5); //add three exp
            Debug.Log("trash"); 
            GameObject c = Instantiate(clickSplash, this.transform.position, Quaternion.identity);
            GameObject d = Instantiate(exp, this.transform.position, Quaternion.identity);
            sm.Trash();
            Destroy(c, 1f);
            Destroy(d, 5f);
            Destroy(this.gameObject);
            
        }
    }

    void DestroyAuto(){
        Destroy(this.gameObject);
    }
    
    void Detect(){
        }
    }

