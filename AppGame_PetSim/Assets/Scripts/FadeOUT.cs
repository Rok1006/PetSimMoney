using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOUT : MonoBehaviour
{
    public static FadeOUT Instance;
    public float fadeOutTime;

    void Awake(){
        Instance = this;
    }

    public void doFadeOUT(){
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < sprites.Length; i++){
        StartCoroutine (spriteFadeOUT(sprites[i]));
        }
    }

    public void doFadeIN(){
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < sprites.Length; i++){
        StartCoroutine (spriteFadeIN(sprites[i]));
        }
    }
    
    IEnumerator spriteFadeOUT(SpriteRenderer _sprite){
        Color tmpColor = _sprite.color;
        while(tmpColor.a > 0f){
            tmpColor.a -= Time.deltaTime /fadeOutTime;
            _sprite.color = tmpColor;
            if(tmpColor.a <= 0f)
                tmpColor.a = 0.0f;
            
            yield return null;
        }
        _sprite.color = tmpColor;
    }

    IEnumerator spriteFadeIN(SpriteRenderer _sprite){
        Color tmpColor = _sprite.color;
        while(tmpColor.a < 1f){
            tmpColor.a += Time.deltaTime /fadeOutTime;
            _sprite.color = tmpColor;
            if(tmpColor.a >= 1f)
                tmpColor.a = 1f;
            
            yield return null;
        }
        _sprite.color = tmpColor;
    }
}
