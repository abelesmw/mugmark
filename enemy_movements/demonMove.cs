using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonMove : MonoBehaviour
{
    //main
    Animator myAnim;
    Vector3 localScale;
    Rigidbody2D rb;
    public GameObject player;
    float distanceToPlayer;
    public float walkDistance = 5f;
    float distanceToStartPoint;
    public float attackRange;
    Vector3 startPosition;
    public bool isMoving = true;


    //Demon behavior
    public float moveSpeed = 3f, moveSpeedMultiplier;
    bool movingRight = true, triggered = false;

    void Start()
    {
        startPosition = transform.position;
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceToStartPoint = Vector3.Distance(startPosition, transform.position);

        if (isMoving)
        {
            if (distanceToPlayer < attackRange)
            {
                startPosition = transform.position;
                myAnim.SetBool("Triggered", true);
                if (player.transform.position.x < transform.position.x)
                {
                    if (movingRight)
                    {
                        flipFacing();
                        localScale.x = -.6f;
                        rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                    }
                    else
                    {
                        localScale.x = -.6f;
                        rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                    }

                }
                else
                {
                    if (movingRight)
                    {
                        localScale.x = .6f;
                        rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                    }
                    else
                    {
                        flipFacing();
                        localScale.x = .6f;
                        rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                    }

                }
            }
            else
            {
                myAnim.SetBool("Triggered", false);
                if (movingRight && !triggered)
                {
                    moveRight();               
                }
                else if (!movingRight && !triggered)
                {
                    moveLeft();
                }
            }

        }
    }

    void moveRight()
    {
        if(distanceToStartPoint > walkDistance && transform.position.x > startPosition.x)
        {
            moveLeft();
        }
        else
        {
            movingRight = true;
            localScale.x = .6f;
            transform.localScale = localScale;
            rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
        }

    }
    void moveLeft()
    {
        if (distanceToStartPoint > walkDistance && transform.position.x < startPosition.x)
        {
            moveRight();
        }
        else
        {
            movingRight = false;
            localScale.x = -.6f;
            transform.localScale = localScale;
            rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
        }
    }

    void flipFacing()
    {
        float facingX = transform.localScale.x;
        facingX *= -1;
        transform.localScale = new Vector3(facingX, transform.localScale.y, transform.localScale.z);
        movingRight = !movingRight;
    }

    public void ToggleKnockback()
    {
        isMoving = true;
        startPosition = transform.position;
    }

    /* private void OnTriggerEnter2D(Collider2D other)
     {

         if(other.tag == "Player")
         {
             triggered = true;
             leftPoint.SetParent(transform);
             rightPoint.SetParent(transform);

             if(other.transform.position.x < transform.position.x)
             {
                 if (movingRight)
                 {
                     flipFacing();
                     localScale.x = -.6f;
                     rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                 }

                 else
                 {
                     localScale.x = -.6f;
                     rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                 }

             } else
             {
                 if (movingRight)
                 {
                     localScale.x = .6f;
                     rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                 }
                 else
                 {
                     flipFacing();
                     localScale.x = .6f;
                     rb.velocity = new Vector2(localScale.x * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
                 }

             }
         }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.tag == "Player")
         {
             triggered = false;
             leftPoint.SetParent(null);
             rightPoint.SetParent(null);

             if (movingRight)
             {
                 if(other.transform.position.x > transform.position.x)
                 {
                     flipFacing();
                 }
             }
             else
             {
                if(other.transform.position.x < transform.position.x)
                 {
                     flipFacing();
                 }
             }
         }
     }*/

    /* if (transform.position.x > rightPoint.position.x)
     {
         movingRight = false;
     }
     if (transform.position.x < leftPoint.position.x)
     {
         movingRight = true;
     }
     if (movingRight && !triggered)
     {
         moveRight();
     }
     else if(!movingRight && !triggered)
     {
         moveLeft();
     }

     if (leftPoint.transform.position.x > rightPoint.transform.position.x)
     {

         Vector3 temp = rightPoint.transform.position;
         rightPoint.position = leftPoint.transform.position;
         leftPoint.transform.position = temp;
     }*/




}
