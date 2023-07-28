using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_move : MonoBehaviour
{
    public float fallSpeed = 0.5f;
    public float swayAmount = 0.5f;
    public float swaySpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // make the leaf fall
        transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);

        // make the leaf sway
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position = new Vector3(startPos.x + sway, transform.position.y, transform.position.z);
    }
}
