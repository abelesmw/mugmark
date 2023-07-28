using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public float leftBound = -5.0f;
    public float rightBound = 5.0f;

    private bool movingRight = true;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Move the platform left or right
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= originalPosition.x + rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= originalPosition.x + leftBound)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Flamigo"))
        {
            // If the player is on the platform, move the player along with the platform
            if(collision.transform.position.y > transform.position.y)
            {
                collision.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the player is no longer on the platform, unparent the player from the platform
            collision.transform.parent = null;
        }
    }
}