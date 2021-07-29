using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
//handle sound
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public GameObject CatSound;
    private AudioSource foodStep;
    private AudioSource purr;
    private AudioSource sitdown;
    private AudioSource meowing;
    private AudioSource playing;
    public GameObject ObjSound;
    private AudioSource drinkfall;
    private AudioSource foodfall;
    private AudioSource toyfall;
    public GameObject EffectSound;
    private AudioSource click;
    private AudioSource flip;
    private AudioSource bling;
    private AudioSource trash;
    private AudioSource buy;
    public GameObject GachaSound;
    private AudioSource pull;
    private AudioSource draw;
    private AudioSource ball;
    private AudioSource flyby;
    private AudioSource crack;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        //cat sound
        AudioSource[] catAudios = CatSound.GetComponents<AudioSource>();
        foodStep = catAudios[0];
        meowing = catAudios[1];
        purr = catAudios[2];
        sitdown = catAudios[3];
        playing = catAudios[4];
        //obj sound
        AudioSource[] objAudios = ObjSound.GetComponents<AudioSource>();
        drinkfall = objAudios[0];
        foodfall = objAudios[1];
        toyfall = objAudios[2];
        //Effect sound
        AudioSource[] effectAudios = EffectSound.GetComponents<AudioSource>();
        click = effectAudios[0];
        flip = effectAudios[1];
        bling = effectAudios[2];
        trash = effectAudios[3];
        buy = effectAudios[4];
        //Gacha sound
        AudioSource[] gachaAudios = GachaSound.GetComponents<AudioSource>();
        pull = gachaAudios[0];
        draw = gachaAudios[1];
        ball = gachaAudios[2];
        flyby = gachaAudios[3];
        crack = gachaAudios[4];
    }
    //cat sound
    public void footstep(){
        foodStep.Play();
    }
    public void meow(){
        meowing.Play(); //keep looping
    }
    public void hulu(){ //when touch
        purr.Play();
    }
    public void Stophulu(){ //when touch
        purr.Stop();
    }
    public void sit(){ //when touch
        sitdown.Play();
    }
    public void play(){
        playing.Play();
    }
    //obj sound
    public void DrinkFall(){
        drinkfall.Play();
    }
    public void FoodFall(){
        foodfall.Play();
    }
    public void ToyFall(){
        toyfall.Play();
    }
    public void Click(){
        click.Play();
    }
    public void Flip(){
        flip.Play();
    }
    public void Bling(){
        bling.Play();
    }
    public void Trash(){
        trash.Play();
    }
    //gacha sound
    public void PressDraw(){
        pull.Play();
    }
    public void drawingsound(){
        draw.Play();
    }
    public void ballLand(){
        ball.Play();
    }
    public void Flyby(){
        flyby.Play();
        //Manager.Instance.BGCover.SetActive(true);
    }
    public void Open(){
        crack.Play();
    }
    public void crackopen(){
        bling.Play();
        Manager.Instance.BGCover.SetActive(true);
        Manager.Instance.CrossCloseEgg.SetActive(true);
    }
    public void Buy(){
        buy.Play();
    }

}
