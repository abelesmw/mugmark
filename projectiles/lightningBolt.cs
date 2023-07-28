using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBolt : MonoBehaviour
{
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float moveSpeed;
    public float maxSpeed;
    public float timeToColor = 2f;
    float colorTimer = 0f;
    public SpriteRenderer myRenderer;
    public PolygonCollider2D collido;


    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        localScale = transform.localScale;
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.color = new Color(0f, 0f, 0f, 0f);
        collido = GetComponent<PolygonCollider2D>();
        collido.enabled = false;
    }

    void Update()
    {

        colorTimer += Time.deltaTime;
        myRB.velocity = new Vector2(myRB.velocity.x, localScale.y * -moveSpeed);
        if (colorTimer > timeToColor)
        {
            collido.enabled = true;
            myRenderer.color = new Color(1f, 1f, 1f, 1f);
        }


    }
}