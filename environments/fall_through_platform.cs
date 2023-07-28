using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_through_platform : MonoBehaviour
{
    Collider2D collider;
    float leftJoystick;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        leftJoystick = Input.GetAxis("LeftJoystick");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetAxis("Jump") > 0 && leftJoystick >= .99f)
            {
                collider.enabled = false;
                print("player pressed down and jump");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collider.enabled = true;
        }

    }

}