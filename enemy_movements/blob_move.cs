using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blob_move : MonoBehaviour
{
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float moveSpeed;
    public float maxSpeed;


    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        localScale = transform.localScale;
    }

    void Update()
    {

        myRB.velocity = new Vector2(localScale.x * moveSpeed, myRB.velocity.y);
        
    }
}