using UnityEngine;

public class move_in_semi_circle : MonoBehaviour
{
    public float speed = 5f;
    public float radius = 5f;
    private float angle = 0f;
    private float direction = 1f;

    private void Update()
    {
        angle += speed * Time.deltaTime * direction;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, transform.position.y, z);
        if (angle >= Mathf.PI)
        {
            direction = -1f;
        }
        else if (angle <= 0f)
        {
            direction = 1f;
        }
    }
}
