using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //main
    public Rigidbody2D myRB;
    SpriteRenderer myRenderer;
    public Animator myAnim;
    Transform myTransform;
    public BoxCollider2D boxy;
    public AudioClip levelMusic;
    public GameObject mainCam;
    public GameObject forest;
    public PlayerHealth PH;
    Color flashColor = new Color(255f, 255f, 2555f, 0.5f);
    Color goodColor = new Color(255f, 255f, 2555f, 1f);
    public bool invincible = false;

    //crouch
    public bool crouch = false;

    //move
    public float maxSpeed;
    public bool facingRight = true;
    public bool canMove = true, isHit = false, deadCantMove = false;
    int facingDirection;

    //jump
    public LayerMask groundLayer, gameCleanLayer;
    public Transform groundCheck;
    public bool grounded = false, isJumping, holdingJump;
    float groundCheckRadius = 0.2f, jumpTimeCounter;
    public float jumpPower, jumpTime;

    //double jump
    public int extraJumps = 3;
    int jumpCount = 0;
    public float doubleJumpPower, doubleJumpTime;
    float doubleJumpTimeCounter;

    //dash
    public AudioClip woosh;
    public float dashSpeed, dashTime, dashGravityTime = .5f;
    float dashTimer, dashGravityTimer, dashCoolDown;
    public bool holdingDash = false, stillHoldingDash = false;

    //wall jump
    RaycastHit2D WallCheckHit;
    public LayerMask wallLayer;
    public float wallJumpTime = 0.2f, wallSlideSpeed = 0.1f, wallDistance = 0.5f, wallJumpHorizontal = 5f, wallJumpingTime;
    float wallJumpingTimer, jumpOffWallTime;
    bool isWallSliding = false, wallJumping = false;

    //attack
    public Transform attackPoint;
    public LayerMask enemyLayers;
    AudioSource playerAS;
    public AudioClip playerPunch, jumpSound;
    public int attackDamage;
    public float attackRange = 0.5f, hitTime = 1, punchTime = 1, pushBackForce;
    float hitTimer = 0, punchTimer = 0;
    public float knockBackTime = .25f;

    //shoot
    public Transform lowShootPoint;
    public Transform highShootPoint;
    public Transform lowShootPointCrouch;
    public Transform highShootPointCrouch;
    public bool shooting = false;

    //locked
    public bool locked = false;

    //game clear ground layer stores
    public float recentGroundX;
    public float recentGroundY;
    public bool gameCleaned = false;
    bool beingCleaned = false;
    private Transform currentPlatform;
    private BoxCollider2D currentCollider;
    public float gameCleanedUpOffset;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        playerAS = GetComponent<AudioSource>();
        myTransform = GetComponent<Transform>();
        playerAS.PlayOneShot(levelMusic, .65f);
        boxy = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////////////////////////////////////
        ///TIMERS UNDER UPDATE//////////////////////////
        ////////////////////////////////////////////////
        hitTimer += Time.deltaTime;
        punchTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        wallJumpingTimer += Time.deltaTime;
        dashGravityTimer += Time.deltaTime;
        myAnim.SetFloat("shootHeight", Input.GetAxis("LeftJoystick"));
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        print("can move = " + canMove);

        if (shooting)
        {         
            myAnim.SetBool("horizontalIsZero", Input.GetAxis("HorizontalJoystick") == 0);
        }

        ////////////////////////////////////////////////
        ///////STORING GROUND INFO UNDER UPDATE/////////
        ////////////////////////////////////////////////
        if (hit.collider != null && hit.transform.tag != "movingPlatform")
        {
            // check if the current platform has changed
            if (hit.collider.transform != currentPlatform)
            {
                // update the current platform and collider
                float middlePoint = hit.collider.transform.position.x + (hit.collider.transform.localScale.x) / 2;
                currentPlatform = hit.collider.transform;
                currentCollider = hit.collider as BoxCollider2D;
                newPosition = currentPlatform.position;
                newPosition.x = middlePoint;
                newPosition.y += currentCollider.size.y / 2 + gameCleanedUpOffset;
            }
        }

        if (grounded)
        {
            recentGroundX = transform.position.x;
            recentGroundY = transform.position.y + 12;
        }

        gameCleaned = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, gameCleanLayer);
        if (gameCleaned)
        {
            canMove = false;

            if (!facingRight)
            {
                facingRight = !facingRight;
                myRenderer.flipX = !myRenderer.flipX;
            }

            if (myTransform.position.x < recentGroundX)
            {
                myTransform.position = newPosition;
            }
            else
            {
                myTransform.position = newPosition;
            }

            //StartCoroutine(slowGravity());
        }

        ////////////////////////////////////////////////
        ///LOCKED UNDER UPDATE//////////////////////////
        ////////////////////////////////////////////////

        if (Input.GetButton("Lock") && grounded)
        {
            canMove = false;
            locked = true;
            myRB.velocity = new Vector2(0, myRB.velocity.y);
            myAnim.SetFloat("MoveSpeed", 0f);
        }
        if (Input.GetButtonUp("Lock"))
        {
            canMove = true;
            locked = false;
            myAnim.SetFloat("MoveSpeed", Mathf.Abs(myRB.velocity.x));
        }

        ////////////////////////////////////////////////
        ///CROUCH UNDER UPDATE//////////////////////////
        ////////////////////////////////////////////////
        
        if ((Input.GetKey(KeyCode.DownArrow) | Input.GetButton("Crouch") | (Input.GetAxis("LeftJoystick") >= 1f)) && grounded && !locked)
        {
            Crouch();
            canMove = false;
            boxy.size = new Vector2(4.127085f, 6f);
            boxy.offset = new Vector2(-0.2800665f, -7.25f);
            crouch = true;
            myRB.velocity = new Vector2(0, myRB.velocity.y);


        }

        if (Input.GetKeyUp(KeyCode.DownArrow) | Input.GetButtonUp("Crouch") | (Input.GetAxis("LeftJoystick") < 1f))
        {
            crouch = false;
            myAnim.SetBool("Crouch", crouch);
            boxy.size = new Vector2(4.127085f, 18.4889f);
            boxy.offset = new Vector2(-0.2800665f, -1.140248f);
            canMove = true;
        }
        
        ////////////////////////////////////////////////
        ///DASH UNDER UPDATE////////////////////////////
        ////////////////////////////////////////////////

        if (!holdingDash && !crouch && !locked && (Input.GetKey(KeyCode.Q) | Input.GetButton("Dash")) && facingRight && dashTimer > dashTime && !stillHoldingDash)
        {
            myAnim.SetBool("isDashing", true);
            playerAS.PlayOneShot(woosh);
            myRB.gravityScale = 0;
            holdingDash = true;
            stillHoldingDash = true;
            dashTimer = 0;
            dashGravityTimer = 0;
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
            myRB.AddForce(new Vector2(dashSpeed, 0f), ForceMode2D.Impulse);

        } else if (!holdingDash && !crouch && !locked && (Input.GetKey(KeyCode.Q) | Input.GetButton("Dash")) && !facingRight && dashTimer > dashTime && !stillHoldingDash)
        {
            myAnim.SetBool("isDashing", true);
            playerAS.PlayOneShot(woosh);
            myRB.gravityScale = 0;
            holdingDash = true;
            stillHoldingDash = true;
            dashTimer = 0;
            dashGravityTimer = 0;
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
            myRB.AddForce(new Vector2(-dashSpeed, 0f), ForceMode2D.Impulse);

        } 
        if (dashGravityTimer > dashGravityTime && !beingCleaned)
        {
            myRB.gravityScale = 1;
            myAnim.SetBool("isDashing", false);     
            holdingDash = false;
        }

        if (Input.GetKeyUp(KeyCode.Q) | Input.GetButtonUp("Dash"))
        {
            stillHoldingDash = false;
        }

        ////////////////////////////////////////////////
        ///PUNCH UNDER UPDATE///////////////////////////
        ////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.S) && punchTimer > punchTime)
        {
            Attack();
            punchTimer = 0;
            //canMove = false;
        }

        ////////////////////////////////////////////////
        //JUMP UNDER UPDATE/////////////////////////////
        ////////////////////////////////////////////////
        if (!isJumping && canMove && !locked && Input.GetAxis("Jump") > 0 && !isWallSliding && !holdingJump && !holdingDash)
        {
            if (grounded || jumpCount < extraJumps)
            {
                playerAS.PlayOneShot(jumpSound, .75f);
                holdingJump = true;
                isJumping = true;
                jumpTimeCounter = jumpTime;
                doubleJumpTimeCounter = doubleJumpTime;
                myAnim.SetBool("isGrounded", false);
                grounded = false;
                jumpCount += 1;
            }

        }

        if ((Input.GetKey(KeyCode.Space) | Input.GetButton("Jump")) && isJumping == true && !isWallSliding && !holdingDash && jumpCount == 0)
        {
            //holdingJump = false;
            if (Input.GetKey(KeyCode.Q))
            {
                jumpTimeCounter = -5f;
            }

            if (jumpTimeCounter > 0)
            {
                myRB.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        } else if ((Input.GetKey(KeyCode.Space) | Input.GetButton("Jump")) && isJumping == true && !isWallSliding && !holdingDash && jumpCount == 1)
        {
            myAnim.Play("mugmark_jump 1", -1, 0f);
            if (doubleJumpTimeCounter > 0)
            {
                myRB.velocity = Vector2.up * jumpPower * doubleJumpPower;
                doubleJumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }


            if (Input.GetKeyUp(KeyCode.Space) | Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            holdingJump = false;
            //wallJumping = false;
        }

        ///////////////////////////////////////////////
        ///WALL SLIDE UNDER UPDATE/////////////////////
        ///////////////////////////////////////////////

        if (facingRight)
        {
            WallCheckHit = Physics2D.Raycast(groundCheck.position, new Vector2(wallDistance,0), wallDistance, wallLayer);
            //Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(groundCheck.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            //Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
        }

        if (WallCheckHit /* !grounded*/ && Input.GetAxis("HorizontalJoystick") != 0 && !grounded && !holdingJump)
        {
            isWallSliding = true;
            jumpOffWallTime = Time.time + wallJumpTime;

        } else if(jumpTime < Time.time)
        {
            isWallSliding = false;
            myAnim.SetBool("isWallSliding", isWallSliding);
        }

        if (isWallSliding)
        {
            myAnim.SetBool("isWallSliding", isWallSliding);
            jumpCount = 0;
            myRB.velocity = new Vector2(myRB.velocity.x, Mathf.Clamp(myRB.velocity.y, wallSlideSpeed, float.MaxValue));
        }

        ///////////////////////////////////////////////
        ///WALL JUMP UNDER UPDATE//////////////////////
        ///////////////////////////////////////////////
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            playerAS.PlayOneShot(jumpSound);
            holdingJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myAnim.SetBool("isGrounded", false);
            grounded = false;
            if (Input.GetButtonDown("Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    jumpTimeCounter -= Time.deltaTime;
                    wallJumping = true;
                    wallJumpingTimer = 0;
                    if (facingRight)
                    {
                        myRB.velocity = new Vector2(myRB.velocity.x -wallJumpHorizontal, jumpPower);
                        //Vector2 pushDirection = new Vector2(wallJumpHorizontal, jumpPower);
                        //myRB.AddForce(pushDirection, ForceMode2D.Impulse);
                    } else
                    {
                        myRB.velocity = new Vector2(myRB.velocity.x + wallJumpHorizontal, jumpPower);
                        //myRB.AddForce(new Vector2(-wallJumpHorizontal, jumpPower), ForceMode2D.Impulse);
                    }


                }
                else
                {
                    isJumping = false;
                }
                if (wallJumpingTimer > wallJumpingTime)
                {
                    wallJumping = false;
                    wallJumpingTimer = 0f;
                }

            }

        }
        if(wallJumpingTimer > wallJumpTime)
        {
            wallJumping = false;
        }

        ////////////////////////////////////////////////
        ///FACING RIGHT & GROUNDED & MOVE UNDER UPDATE//
        ////////////////////////////////////////////////

        //RaycastHit2D hits = Physics2D.Raycast(groundCheck.position, Vector2.down, .2f, groundLayer);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, .2f, groundLayer);

        if (grounded == true)
        {
            jumpCount = 0;
        }

        myAnim.SetBool("isGrounded", grounded);

        ///////////////////RUNNING//////////////////////

        float move = Input.GetAxis("HorizontalJoystick");
        if (canMove)
        {
            if (canMove)
            {
                if (move > 0 && !facingRight)
                {
                    Flip();
                }
                else if (move < 0 && facingRight)
                {
                    Flip();
                }

                if (!wallJumping && !holdingDash && !locked)
                {

                    myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);
                    myAnim.SetFloat("MoveSpeed", Mathf.Abs(move));
                }

            }
            else
            {
                if ((!wallJumping && !holdingDash) || deadCantMove)
                {
                    myRB.velocity = new Vector2(0, myRB.velocity.y);
                    myAnim.SetFloat("MoveSpeed", 0);
                }

            }
        }
        else
        {
            myRB.velocity = new Vector2(0, myRB.velocity.y);
        }
        
    }

    ////////////////////////////////////////////////
    ///FLIP LEFT OR RIGHT///////////////////////////
    ////////////////////////////////////////////////
    void Flip()
    {
        facingRight = !facingRight;
        myRenderer.flipX = !myRenderer.flipX;
       // attackPoint.localScale = new Vector3(attackPoint.localScale.x * -1, attackPoint.localScale.y, attackPoint.localScale.z);
        attackPoint.localPosition = new Vector3(attackPoint.localPosition.x * -1, attackPoint.localPosition.y, attackPoint.localPosition.z);
        lowShootPoint.localPosition = new Vector3((lowShootPoint.localPosition.x * -1) - 8, lowShootPoint.localPosition.y, lowShootPoint.localPosition.z);
        highShootPoint.localPosition = new Vector3((highShootPoint.localPosition.x * -1) - 8, highShootPoint.localPosition.y, highShootPoint.localPosition.z);
    }

    public void toggleCanMove()
    {
       canMove = !canMove;
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    //////////////////////////////////////////////
    ///PUNCH//////////////////////////////////////
    //////////////////////////////////////////////
    void Attack()
    {
        if (punchTimer > punchTime)
        {

            //play attack animation
            myAnim.SetTrigger("Attack");

            //play attack sounds
            playerAS.PlayOneShot(playerPunch);

            //detect enemies in range of attack

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Damage enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                if (punchTimer > punchTime)
                {
                   // PushBack(enemy.transform);
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                    hitTimer = 0;
                }
            }
        }
         
    }

    void PushBack(Transform pushedObject)
    {
        Debug.Log("***enemy knocking back");
        if(pushedObject.GetComponent<beeEasyMove>() is null)
        {
            //do nothing;
        }
        else
        {
            pushedObject.GetComponent<beeEasyMove>().isMoving = false;
        }

        if(pushedObject.GetComponent<demonMove>() is null)
        {
            //do nothingreturn;
        }
        else
        {
            pushedObject.GetComponent<demonMove>().isMoving = false;
        }

        Vector2 pushDirection = new Vector2((pushedObject.position.x - transform.position.x), (pushedObject.position.y - transform.position.y)).normalized;
        if (pushDirection.y < .5f)
        {
            pushDirection.y = .5f;
            pushDirection = pushDirection.normalized;
        }
        pushDirection *= pushBackForce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);

        if (pushedObject.GetComponent<beeEasyMove>() is null)
        {
            //do nothing;
        }
        else
        {
            pushedObject.GetComponent<beeEasyMove>().Invoke("ToggleKnockback", knockBackTime);
        }

        if (pushedObject.GetComponent<demonMove>() is null)
        {
            //do nothing;
        }
        else
        {
            pushedObject.GetComponent<demonMove>().Invoke("ToggleKnockback", knockBackTime);
        }
    }

    //////////////////////////////////////////////
    //CROUCH//////////////////////////////////////
    //////////////////////////////////////////////

    void Crouch()
    {
        crouch = true;
        myAnim.SetBool("Crouch", crouch);
    }

    ///////////////////////////////////////////////
    ///PATHFINDER DISPLAY//////////////////////////
    ///////////////////////////////////////////////

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    ///////////////////////////////////////////////
    /////////IEnumerators//////////////////////////
    ///////////////////////////////////////////////

    public IEnumerator playerHit(float pushBackTime)
    {
        isHit = true;
        float startTime = Time.time;
        while(Time.time < startTime + pushBackTime)
        {
            myRenderer.color = Color.Lerp(flashColor, myRenderer.color, 2f * Time.deltaTime);            
            yield return new WaitForEndOfFrame();
            invincible = true;
        }
        isHit = false;
        invincible = false;
        myRenderer.color = goodColor; 
    }

}
