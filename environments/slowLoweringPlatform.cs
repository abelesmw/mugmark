using System.Collections;
using UnityEngine;

public class slowLoweringPlatform : MonoBehaviour
{
    public float speed = 4f;
    private float topPoint;
    private float bottomPoint;
    private Transform player;
    private bool movingDown = false;
    private bool movingUp = false;

    void Start()
    {
        topPoint = transform.position.y;
        bottomPoint = topPoint - 2.5f;
    }

    void Update()
    {
        if (movingDown)
        {
            if (transform.position.y > bottomPoint)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
            else
            {
                movingDown = false;
            }
        }
        else if (movingUp)
        {
            if (transform.position.y < topPoint)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                movingUp = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            movingDown = true;
            movingUp = false;
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            movingUp = true;
            movingDown = false;
            other.transform.parent = null;
        }
    }
}
