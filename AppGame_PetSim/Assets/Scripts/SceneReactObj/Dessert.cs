using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dessert : MonoBehaviour
{
    [SerializeField] Animator pudding;
    // Start is called before the first frame update
    void Start()
    {
        pudding.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator puddingAnim(){
        pudding.enabled = true;
        for(float i = 5; i >0 ; i--){
            pudding.speed = i *0.1f;
            yield return new WaitForSeconds(1);
        }
        pudding.enabled = false;
    }
}
