using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkGuy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private float walkSpeed;

    private float sinceWalked;

    private float multiplier = 1f;

    //MonoBehavior functions
    void Awake()
    {
        sinceWalked = Time.time;
        walkSpeed = 2;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time - sinceWalked < 5f)
        {
            anim.SetFloat("Vertical", multiplier);
            anim.SetFloat("Speed", 1f);
            anim.SetBool("isFacingForward", (multiplier < 0) ? false : true);
            body.velocity = new Vector2(0, multiplier * walkSpeed);
        }
        else
        {
            multiplier *= -1;
            sinceWalked = Time.time;
        }
    }
}