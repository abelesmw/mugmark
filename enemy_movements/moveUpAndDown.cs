using UnityEngine;

public class moveUpAndDown : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveDistance = 5f;
    public float bottomPosition = 3;
    public float topPosition = 7;

    private bool movingUp = false;

    void Update()
    {
        if (movingUp)
        {
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
            if (transform.position.y >= topPosition)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
            if (transform.position.y <= bottomPosition)
            {
                movingUp = true;
            }
        }
    }
}