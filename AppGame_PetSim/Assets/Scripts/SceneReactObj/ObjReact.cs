using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjReact : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseOver(){
        if(gameObject.tag == "Trash"){
            Destroy(gameObject);
        }
    }
    
    void OnMouseDown(){
        if(gameObject.tag == "Marine"){
            Marine.Instance.speed[int.Parse(this.name)] = Marine.Instance.speed[int.Parse(this.name)] + 5f;
            Marine.Instance.squidInt++;
            if(Marine.Instance.squidInt == 10){
                Marine.Instance.KingSquid.SetActive(true);
            }
        }else if(gameObject.tag == "Ghost"){
            Ghost.Instance.NewFace(int.Parse(this.name));

        }else if(gameObject.tag == "spItem"){
            Marine.Instance.StartCoroutine("KingSquidAnim");
        }
    }
    

}
