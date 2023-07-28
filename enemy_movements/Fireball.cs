using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force = 5f;
    public float interval = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ApplyForce", 0f, interval);
    }

    void ApplyForce()
    {
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}

