using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Airship : MonoBehaviour
{
    public static Airship Instance;
    float i =0;
    float y,t;

    Vector2 oldPos;
    Vector3 oldScale;
    [SerializeField] GameObject[] goalPT;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject yellowLight;
    int goalPTindex = 0;
    
    //animation
    [SerializeField]GameObject BG;
    [SerializeField]GameObject switchplanet;//switchplanet
    public GameObject explore;//airship explore anime
    public GameObject success;//explore success anime

    void Awake(){
        Instance = this ;
        oldPos = transform.position;//this.transform.position;
        oldScale = transform.localScale;
    }

    public void exploreAnim(){
        BG.SetActive(true);
        explore.SetActive(true);
        //BG.GetComponent<Button>().interactable = true;
    }
    public IEnumerator switchPlanet(){
        BG.SetActive(true);
        switchplanet.SetActive(true);
        yield return new WaitForSeconds(2);
        BG.SetActive(false);
        switchplanet.SetActive(false);
        Manager.Instance.ClickCloseAdventure();
    }

    public IEnumerator exploreAnim2(){
        yield return new WaitForSeconds(4);
        explore.SetActive(false);
        BG.SetActive(false);
        Manager.Instance.ClickCloseAdventure();
    }
    public IEnumerator successAnim(){
        BG.SetActive(true);
        //BG.GetComponent<Button>().interactable = false;
        success.SetActive(true);
        yield return new WaitForSeconds(5);
        success.SetActive(false);
        BG.SetActive(false);
        AdventureManager.Instance.RewardPannel.SetActive(true);
    }
}
