using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Up : MonoBehaviour
{
    public GameObject coin;
    public Animator anim;
    public AudioSource audio;
    public AudioClip ding;
    float healthCount = 0;
    public SpriteRenderer rendy;

    void Start()
    {
        coin = GetComponent<GameObject>();
        audio = GetComponent<AudioSource>();
        rendy = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D mugmark)
    {
        if(mugmark != null && mugmark.gameObject.layer == 13 && healthCount < 1)
        {
            mugmark.gameObject.GetComponent<PlayerHealth>().addHealth(1);
            audio.PlayOneShot(ding, .2f);
            healthCount++;
            rendy.color = new Color(0f, 0f, 0f, 0f);
        }

    }
}
