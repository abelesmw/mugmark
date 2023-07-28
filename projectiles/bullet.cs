using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class bullet : MonoBehaviour
    {
    Rigidbody2D myRB;
    public float speed;
    public GameObject bulletObject;
    private float bulletAliveTime;
    public float timeToBulletDeath = 1.5f;
    public PlayerController PS;
    public SpriteRenderer myRenderer;
    Transform transform;
    public float turnLeftOffset = 1f;
    public float shootUpOffset = 1f;
    float horizontalJoystick;
    float leftJoystick;
    



    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        PS = g.GetComponent<PlayerController>();
        myRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        horizontalJoystick = Input.GetAxis("HorizontalJoystick");
        leftJoystick = Input.GetAxis("LeftJoystick");

        //print(leftJoystick);

        if (PS.facingRight)
        {

            if (PS.crouch)
            {
                myRB.velocity = transform.right * speed;
            }
            

            ///shootUP
            else if (leftJoystick <= -.99f)
            {
                myRB.velocity = transform.up * speed;
                transform.Rotate(0, 0, 90);
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y + shootUpOffset, transform.position.z);
            }

            ///shoot diagonal UP
            else if (leftJoystick <= -.1f && leftJoystick > -.99f)
            {
                myRB.velocity = new Vector2(20f, 18f);
                transform.Rotate(0, 0, 40);
                transform.position = new Vector3(transform.position.x + .25f, transform.position.y + .25f, transform.position.z);
            }

            ///shoot sideways
            else if (leftJoystick > -.1f && leftJoystick <= 0.1f)
            {
                myRB.velocity = transform.right * speed;
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }

            //still sideways: includes joystick diag down without being locked

            else if (!PS.locked && leftJoystick > 0.1f && leftJoystick < .99f)
            {
                myRB.velocity = transform.right * speed;
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }

            ///shoot diagonal down
            else if (PS.locked && leftJoystick > 0.1f && leftJoystick < .99f)
            {
                myRB.velocity = new Vector2(20f, -18f);
                transform.position = new Vector3(transform.position.x, transform.position.y - .3f, transform.position.z);
                transform.Rotate(0, 0, -40);
            }

            ///shoot down
            else if (leftJoystick >= .99f)
            {
                myRB.velocity = transform.up * -speed;
                transform.Rotate(0, 0, -90);
            }

        }
        else
        {
            myRenderer.flipX = !myRenderer.flipX;
            if (PS.crouch)
            {
                myRB.velocity = transform.right * -speed;
                transform.position = new Vector3(transform.position.x - turnLeftOffset, transform.position.y, transform.position.z);
            }

            //shoot up
            else if (leftJoystick <= -.99f)
            {
                myRB.velocity = transform.up * speed;
                transform.Rotate(0, 0, -90);
                transform.position = new Vector3(transform.position.x + turnLeftOffset -.4f, transform.position.y + shootUpOffset, transform.position.z);

            }

            //shoot diagonal up left
            else if (leftJoystick <= -.1f && leftJoystick > -.99f)
            {
                myRB.velocity = new Vector2(-20f, 18f);
                transform.Rotate(0, 0, -40);
                transform.position = new Vector3(transform.position.x + turnLeftOffset - .5f, transform.position.y + .4f, transform.position.z);
            }

            //shoot left
            else if (leftJoystick > -.1f && leftJoystick <= .1f)
            {
                myRB.velocity = transform.right * -speed;
                transform.position = new Vector3(transform.position.x + turnLeftOffset - .4f, transform.position.y, transform.position.z);
            }

            //still sideways: includes joystick diag down without being locked
            else if (!PS.locked && leftJoystick > .1f && leftJoystick < .99f)
            {
                myRB.velocity = transform.right * -speed;
                transform.position = new Vector3(transform.position.x + turnLeftOffset - .4f, transform.position.y, transform.position.z);
            }

            //shoot diagonal down left
            else if (PS.locked && leftJoystick > .1f && leftJoystick < .99f)
            {
                myRB.velocity = new Vector2(-20f, -18f);
                transform.Rotate(0, 0, 40);
                transform.position = new Vector3(transform.position.x + turnLeftOffset, transform.position.y -.2f, transform.position.z);
            }

            //shoot left down
            else if (leftJoystick >= .99f)
            {
                myRB.velocity = transform.up * -speed;
                transform.Rotate(0, 0, 90);
                transform.position = new Vector3(transform.position.x + turnLeftOffset, transform.position.y, transform.position.z);
            }
        }
        

    }

    private void Update()
    {
        bulletAliveTime += Time.deltaTime;

        if (bulletAliveTime > timeToBulletDeath)
        {
            Destroy(bulletObject);
        }

    }

    
}

