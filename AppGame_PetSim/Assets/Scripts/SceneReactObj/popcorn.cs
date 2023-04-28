using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popcorn : MonoBehaviour
{
    public List<GameObject> PrefabsList = new List<GameObject>();
    public GameObject[] Effect;
    public GameObject[] Face;
    public GameObject jackpot;
    Vector3 Pos;
    Quaternion Rotation;
    GameObject Folder;
    Color color,newColor;
    int clickNUM;
    ParticleSystem main1, main2;
    
    void Start()
    {
        Pos = Effect[0].transform.position;
        Rotation = Effect[0].transform.rotation;
        Folder = this.transform.Find("GameObject").gameObject;
    }
    void OnMouseDown(){
        StartCoroutine("randomFace");
        SoundManager.Instance.testing();
        clickNUM++;
        if(clickNUM == 5){
            StartCoroutine("FadeOUT");
        }
        PrefabsList.Add(Instantiate(Effect[RandomINT()],Pos, Rotation, Folder.transform.parent));
    }

    int RandomINT(){
        return Random.Range(0,2);
    }
    IEnumerator randomFace(){
        Face[0].SetActive(false);
        int i = Random.Range(1,Face.Length);
        Face[i].SetActive(true);
        yield return new WaitForSeconds(2);
        Face[i].SetActive(false);
        Face[0].SetActive(true);
    }
    IEnumerator FadeOUT(){
        jackpot.SetActive(true);

        for(int i = 0; i <PrefabsList.Count;i++){
            Destroy(PrefabsList[i]);
        }
        clickNUM = 0;
        yield return new WaitForSeconds(8);
        jackpot.SetActive(false);
    }
}
