using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cloudMove : MonoBehaviour
{
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float moveSpeed;
    public float leftMoveSpeed;
    public float maxSpeed;
    public float floatDistance = 3f;
    float distanceToStartPoint;
    Vector3 startPosition;

    public GameObject cloud;
    public GameObject lightning;
    public Transform shootPoint;

    public LayerMask mugMarkLayer;

    bool movingDown = true;
    bool canMove = true;

    float floatTimer = 0f;
    //bool reachedBottom = false;

    public Transform bottomPoint;
    public Transform topPoint;

    public float timeToShoot = 1f;
    float shootTimer = 0f;

    bool startTimer = false;
    float shootCount = 0f;


    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        localScale = transform.localScale;
        startPosition = transform.position;
    }

    void Update()
    {

        if (myTransform.position.y <= startPosition.y)
        {
            distanceToStartPoint = startPosition.y - myTransform.position.y;
        }
        else if (myTransform.position.y > startPosition.y)
        {
            distanceToStartPoint = myTransform.position.y - floatDistance;
        }

        floatTimer += Time.deltaTime;
        //print(startTimer);

        if (canMove)
        {
            if (movingDown)
            {
                moveDown();
            }
            else if (!movingDown)
            {
                moveUp();
            }

        } else
        {
            myRB.velocity = new Vector2(0,0);
        }



    }

    void moveDown()
    {
        if (distanceToStartPoint > floatDistance && myTransform.position.y < startPosition.y)
        {
            moveUp();
        }
        else
        {
            movingDown = true;
            myTransform.localScale = localScale;
            myRB.velocity = new Vector2(localScale.x * -leftMoveSpeed, localScale.y * -moveSpeed);
        }

    }
    void moveUp()
    {
        if (distanceToStartPoint > floatDistance && myTransform.position.y > startPosition.y)
        {
            moveDown();
        }
        else
        {
            movingDown = false;
            myTransform.localScale = localScale;
            myRB.velocity = new Vector2(localScale.x * -leftMoveSpeed, localScale.y * moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D mugmark)
    {
        if(mugmark != null && mugmark.gameObject.layer == 13 && shootCount < 1)
        {
            shootCount += 1;
            myAnim.SetBool("triggered", true);
            canMove = false;
            Instantiate(lightning, shootPoint.position, transform.rotation);
            Destroy(cloud, 1.55f);
            

        }
    }
}

