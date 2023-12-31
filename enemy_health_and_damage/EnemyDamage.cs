﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage, damageRate, pushBackForce;
    float nextDamage;

    void Start()
    {
        nextDamage = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && nextDamage < Time.time) {
            PlayerHealth thePlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            //PushBack(other.transform);
        }
    }

     void PushBack(Transform pushedObject)
    {
        Vector2 pushDirection = new Vector2((pushedObject.position.x - transform.position.x), (pushedObject.position.y - transform.position.y)).normalized;
        if(pushDirection.y < .5f)
        {
            pushDirection.y = .5f;
            pushDirection = pushDirection.normalized;
        }
        pushDirection *= pushBackForce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
