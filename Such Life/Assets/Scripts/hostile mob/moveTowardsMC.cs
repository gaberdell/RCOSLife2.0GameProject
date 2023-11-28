using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowardsMC : MonoBehaviour
{

    private Transform player;
    public float speed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("MC").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }
}
