using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shooter : MonoBehaviour
{
    Rigidbody2D myRB;
    Transform myTransform;
    Animator myAnim;
    Vector3 localScale;
    public GameObject bullet;
    public Transform lowShootingPoint;
    public Transform highShootingPoint;
    public Transform lowShootingPointCrouch;
    public Transform highShootingPointCrouch;
    public float waitTime = .02f;
    private float lastShootTime;
    private float shootCounter = 2f;
    public AudioClip shot;
    AudioSource bulletAS;
    public PlayerController PS;

    void Start()
    {
        bulletAS = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnim = GetComponent<Animator>();
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        PS = g.GetComponent<PlayerController>();
    }

    void Update()
    {
        lastShootTime += Time.deltaTime;

        if (PS.crouch)
        {
            if ((Input.GetKey(KeyCode.E) | Input.GetButton("Fire1")) && lastShootTime > waitTime && shootCounter % 2 == 0)
            {
                Instantiate(bullet, lowShootingPointCrouch.position, transform.rotation);
                bulletAS.PlayOneShot(shot, 0.1f);
                lastShootTime = 0;
                shootCounter++;
                PS.myAnim.SetBool("shooting", true);
            }

            else if ((Input.GetKey(KeyCode.E) | Input.GetButton("Fire1")) && lastShootTime > waitTime && shootCounter % 2 != 0)
            {
                Instantiate(bullet, highShootingPointCrouch.position, transform.rotation);
                bulletAS.PlayOneShot(shot, 0.1f);
                lastShootTime = 0;
                shootCounter++;
                PS.myAnim.SetBool("shooting", true);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                PS.myAnim.SetBool("shooting", false);
                PS.shooting = false;
            }

        }
        else if (PS.crouch == false)
        {
            if ((Input.GetKey(KeyCode.E) | Input.GetButton("Fire1")) && lastShootTime > waitTime && shootCounter % 2 == 0)
            {
                Instantiate(bullet, lowShootingPoint.position, transform.rotation);
                bulletAS.PlayOneShot(shot, 0.1f);
                lastShootTime = 0;
                shootCounter++;
                PS.shooting = true;
                PS.myAnim.SetBool("shooting", true);
            }

            else if ((Input.GetKey(KeyCode.E) | Input.GetButton("Fire1")) && lastShootTime > waitTime && shootCounter % 2 != 0)
            {
                Instantiate(bullet, highShootingPoint.position, transform.rotation);
                bulletAS.PlayOneShot(shot, 0.1f);
                lastShootTime = 0;
                shootCounter++;
                PS.myAnim.SetBool("shooting", true);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                PS.myAnim.SetBool("shooting", false);
                PS.shooting = false;
            }
        }

    }
}
