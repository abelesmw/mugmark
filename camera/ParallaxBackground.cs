﻿using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] backgrounds; // array of all the backgrounds to be parallaxed
    public float parallaxScale;
    public float parallaxReductionFactor;
    public float smoothing;

    private Vector3 previousCamPos;

    void Start()
    {
        previousCamPos = transform.position;
    }

    void Update()
    {
        float parallax = (previousCamPos.x - transform.position.x) * parallaxScale;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = transform.position;
    }
}
