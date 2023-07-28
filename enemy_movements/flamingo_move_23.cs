using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class flamingo_move_23: MonoBehaviour
    {
    Rigidbody2D flamingoRB;
    Transform flamingoTransform;
    Animator flamingoAnim;
    public GameObject pinkBall;
    public Transform shootPoint;

    float jumpTimer = 0;
    public float timeToNextJump = 4f;

    bool facingRight = false;
    float jumpCounter = 0;
    float nextIdleTimer = 0;

    public float shootRadius;
    float distanceToMugmark;


    void Start()
    {
        flamingoRB = GetComponent<Rigidbody2D>();
        flamingoTransform = GetComponent<Transform>();
        flamingoAnim = GetComponent<Animator>();
        jumpTimer = 3f;
    }

    private void Update()
    {
        distanceToMugmark = transform.position.x - GameObject.Find("Mugmark").transform.position.x;
        jumpTimer += Time.deltaTime;
        nextIdleTimer += Time.deltaTime;
        
        if ((distanceToMugmark < shootRadius) && (distanceToMugmark > -shootRadius) && jumpTimer > timeToNextJump)
        {
            flamingoAnim.SetBool("isCharging", true);
            jumpTimer = 0;
            jumpCounter++;
            nextIdleTimer = 0;
            StartCoroutine(ExampleCoroutine());
          
        }

        if (transform.position.x < GameObject.Find("Mugmark").transform.position.x)
        {
            transform.localScale = new Vector3(-.25f, .25f, .25f);
        }
        else
        {
            transform.localScale = new Vector3(.25f, .25f, .25f);
        }

    }
    public IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.725f);
        Instantiate(pinkBall, shootPoint.position, transform.rotation);
        flamingoAnim.SetBool("isCharging", false);
    }

}

