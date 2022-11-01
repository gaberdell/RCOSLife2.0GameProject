using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_timer : MonoBehaviour
{
    public float timer = 21.295f;

    // Called every frame (decrement timer by the elapsed
    // time between frames, every frame)
    void Update()
    {
        timer -= Time.deltaTime;
    }
    // When colliding with something, if the timer has run out,
    // play the given sound
    void OnCollisionEnter2D(Collision2D col)
    {
        if (timer <= 0)
        {
            SendMessage("Play");
            timer = 21.295f;
        }
    }

}
