using UnityEngine;

public class move_side_to_side : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveDistance = 5f;
    public float bottomPosition = 3;
    public float topPosition = 7;
    public bool bee = false;
    SpriteRenderer myRenderer;

    private bool movingUp = false;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingUp)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);           

            if (transform.position.x >= topPosition)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            if (bee)
            {
                myRenderer.flipX = !myRenderer.flipX;
            }
            if (transform.position.x <= bottomPosition)
            {
                movingUp = true;
            }
        }
    }
}