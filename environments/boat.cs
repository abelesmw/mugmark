using UnityEngine;

public class boat : MonoBehaviour
{
    public float speed = 3f;
    private bool moving = false;
    private Transform Player;


    //Tracks the collission enter between Player and boat
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moving = true;
            collision.transform.parent = transform;
        }
    }

    //Tracks the collision exit between Player and boat
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    //Checks if the the moving variable is true, and if so it moves the boat to the right at the speed of the speed variable
    //Checks if the player is not null, and positions the player right above the boat
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            /*if (Player != null)
            {
                Player.position = transform.position + new Vector3(0, 1, 0);
            } */
        }
    }
}
