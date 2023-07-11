/*
Alex Mattoni - 11/01/2022

Basic timer script that plays an audio clip (attached via the
Unity inspector) through a self-created AudioSource and AudioListener.

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_timer : MonoBehaviour
{
    public float timer = 0;
    public AudioClip battleTheme;
    public AudioSource source1;
    public AudioListener listener1;

    void Start()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        listener1 = gameObject.AddComponent<AudioListener>();
    }

    // Called every frame (decrement timer by the elapsed
    // time between frames, every frame)
    void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
    }
    // When colliding with something, if the timer has run out,
    // play the given sound
    void OnCollisionEnter2D(Collision2D col)
    {
        if (timer <= 0)
        {
            timer = 21.295f;
            source1.PlayOneShot(battleTheme);
        }
    }

}
