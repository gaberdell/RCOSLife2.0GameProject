using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Logic : MonoBehaviour
{
        public float timer = 21.295f;


    void Update()
    {
        timer -= Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (timer <= 0)
        {
            SendMessage("Play");
            timer = 21.295f;
        }
    }

}
