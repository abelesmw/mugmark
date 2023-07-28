using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class boulder_move: MonoBehaviour
    {
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float moveSpeed;
    public float maxSpeed;
    public float floatDistance = 3f;
    float distanceToStartPoint;
    Vector3 startPosition;

    public float localScaleMultipler = 1f;

    bool movingDown = true;

    float floatTimer = 0f;
    bool reachedBottom = false;

    public Transform bottomPoint;
    public Transform topPoint;


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
        distanceToStartPoint = Vector3.Distance(startPosition, transform.position);       
        floatTimer += Time.deltaTime;
        //myRB.velocity = new Vector2(myRB.velocity.x, localScale.y * - moveSpeed);

        if (movingDown)
        {
            moveDown();
        }
        else if (!movingDown)
        {
            moveUp();
        }



    }

    void moveDown()
    {
        if (distanceToStartPoint > floatDistance && transform.position.y < startPosition.y)
        {
            moveUp();
        }
        else
        {
            movingDown = true;
           // localScale.x = .6f;
            transform.localScale = localScale;
            myRB.velocity = new Vector2(myRB.velocity.x, localScale.y * localScaleMultipler * -moveSpeed);
        }

    }
    void moveUp()
    {
        if (distanceToStartPoint > floatDistance && transform.position.y > startPosition.y)
        {
            moveDown();
        }
        else
        {
            movingDown = false;
           // localScale.x = -.6f;
            transform.localScale = localScale;
            myRB.velocity = new Vector2(myRB.velocity.x, localScale.y * localScaleMultipler * moveSpeed);
        }
    }
}

