using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class frog_move : MonoBehaviour
    {

    //Components
    Rigidbody2D frogRB;
    Transform frogTransform;
    Animator frogAnim;
    private bool jumpedLeft = false;

    //groundCheck
    public Transform groundCheck;
    public LayerMask groundLayer;
    float groundCheckRadius = 0.2f;
    bool grounded = true;

    //jump force
    public float jumpForceX = .5f;
    public float jumpForceY = 2f;
    public float jumpTime = 3f;
    public float startJumpTime = 0f;
    public float lengthOfJump = 1f;

    //startTime for frogs
    public float timeToFirstJump;
    float firstJumpTimer;
    bool firstJumped = false;

    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>();
        frogAnim = GetComponent<Animator>();
        frogTransform = GetComponent<Transform>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        startJumpTime += Time.deltaTime;
        firstJumpTimer += Time.deltaTime;

        if (grounded && !jumpedLeft && startJumpTime >= lengthOfJump && firstJumpTimer > timeToFirstJump)
        {
            frogRB.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
            jumpedLeft = true;
            startJumpTime = 0;
        }

        else if (grounded && jumpedLeft && startJumpTime >= lengthOfJump)
        {
            frogRB.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
            jumpedLeft = false;
            startJumpTime = 0;
        }

        if (!grounded)
        {
            frogAnim.SetBool("grounded", false);
        }
        else
        {
            frogAnim.SetBool("grounded", true);
        }
    }
}

