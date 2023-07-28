using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_up_move : MonoBehaviour
{
    Transform transformy;

    void Start()
    {
        transformy = GetComponent<Transform>();
    }

    void Update()
    {
        transformy.RotateAround(transform.position, transform.up, Time.deltaTime *2 * 90f);
    }

}
