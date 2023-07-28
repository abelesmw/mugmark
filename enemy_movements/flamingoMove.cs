using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamingoMove : MonoBehaviour
{
    //jump force
    public float jumpForceX = .5f;
    public float jumpForceY = 2f;
    public float jumpTime = 3f;
    public float startJumpTime = 0f;
    //public float onStayStartJumpTime = 0f;
    //public float onStayJumpTime = 3f;

    //Components
    Rigidbody2D flamingoRB;
    Transform flamingoTransform;
    Animator flamingoAnim;

    //groundCheck
    public Transform groundCheck;
    public LayerMask groundLayer;
    float groundCheckRadius = 0.2f;
    bool grounded = true;

    //charging and facing right
    //public float chargeTime;
    float startChargeTime;
    bool charging = false;
    bool canFlip = true;
    bool facingRight = false;
    //bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        flamingoRB = GetComponent<Rigidbody2D>();
        flamingoTransform = GetComponent<Transform>();
        flamingoAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        startJumpTime += Time.deltaTime;
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        flamingoAnim.SetBool("isGrounded", grounded);
        print(charging);

    }  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            charging = true;
            startJumpTime = 0f;
            print("onenter charging = " + charging);
            flamingoAnim.SetBool("isCharging", charging);
            if(facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            } else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("onstay charging = " + charging);

            if (facingRight && startJumpTime > jumpTime && charging)
            {
                if (other.transform.position.x < transform.position.x)
                {
                    flamingoRB.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
                    print("onJump charging = " + charging);
                    flamingoAnim.SetBool("isGrounded", false);
                    startJumpTime = -2f;
                    charging = false;
                    flamingoAnim.SetBool("isCharging", charging);

                }
                else
                {
                   // flamingoAnim.SetBool("isCharging", charging);
                    flamingoRB.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
                    print("onJump charging = " + charging);
                    flamingoAnim.SetBool("isGrounded", false);
                    startJumpTime = -2f;
                    charging = false;
                    flamingoAnim.SetBool("isCharging", charging);
                }

            }
            else if (!facingRight && startJumpTime > jumpTime && charging)
            {
                if (other.transform.position.x > transform.position.x)
                {
                    //flipFacing();
                   // flamingoAnim.SetBool("isCharging", charging);
                    flamingoRB.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
                    print("onJump charging = " + charging);
                    flamingoAnim.SetBool("isGrounded", false);
                    startJumpTime = -2f;
                    charging = false;
                    flamingoAnim.SetBool("isCharging", charging);
                }
                else
                {
                   // flamingoAnim.SetBool("isCharging", charging);
                    flamingoRB.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
                    print("onJump charging = " + charging);
                    flamingoAnim.SetBool("isGrounded", false);
                    startJumpTime = -2f;
                    charging = false;
                    flamingoAnim.SetBool("isCharging", charging);

                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //canFlip = true;
            charging = false;
            flamingoAnim.SetBool("isCharging", charging);
            //flamingoAnim.SetBool("isCharging", charging);
            //print("onExit charging = " + charging);
            //flamingoRB.velocity = new Vector2(0f, 0f);
            //flamingoAnim.SetBool("isCharging", charging);
        }
    }

    void flipFacing()
    {
        if (!canFlip)
        {
            return;
        }
        float facingX = flamingoTransform.localScale.x;
        facingX *= -1;
        flamingoTransform.localScale = new Vector3(facingX, flamingoTransform.localScale.y, flamingoTransform.localScale.z);
        facingRight = !facingRight;
    }
}
