using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pink_ball_move : MonoBehaviour
{
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float timeToColor = 2f;
    float colorTimer = 0f;
    public SpriteRenderer myRenderer;
    public CircleCollider2D collido;

    public float forceY;
    public float forceX;

    public GameObject p;

    float distanceToPlayerX;
    float distanceToPlayerY;

    private void Start()
    {
        
        p = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayerX = transform.position.x - p.transform.position.x;
        distanceToPlayerY = transform.position.y - p.transform.position.y;

        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();

        myRB.AddForce(new Vector2(forceX - distanceToPlayerX, (forceY - distanceToPlayerY) + 3), ForceMode2D.Impulse);
    }

    private void Update()
    {

    }

}

