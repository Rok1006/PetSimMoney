﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
//handle sound
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public GameObject CatSound;
    private AudioSource foodStep;
    public AudioSource purr;
    private AudioSource sitdown;
    private AudioSource meowing;
    private AudioSource playing;
    public GameObject ObjSound;
    private AudioSource drinkfall;
    private AudioSource foodfall;
    private AudioSource toyfall;

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

}