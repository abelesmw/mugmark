using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeEasyMove : MonoBehaviour
{
    //main
    SpriteRenderer myRenderer;
    Transform myTransform;
    private Vector2 center;
    public bool isMoving = true;

    //Beehavior
    public float RotateSpeed = 1f, Radius = 0.1f;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        myRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
        myTransform.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
            transform.position = center + offset;

            if (transform.position.x < GameObject.Find("Mugmark").transform.position.x)
            {
                transform.localScale = new Vector3(.75f, .75f, .75f);
            }
            else
            {
                transform.localScale = new Vector3(-.75f, .75f, .75f);
            }
        }

    }

    public void ToggleKnockback()
    {
        isMoving = true;
        center = transform.position;
    }
}
