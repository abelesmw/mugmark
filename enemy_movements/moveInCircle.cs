using UnityEngine;

public class moveInCircle : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 1f;

    void Update()
    {
        float x = radius * Mathf.Cos(Time.time * speed);
        float y = radius * Mathf.Sin(Time.time * speed);

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }
}
