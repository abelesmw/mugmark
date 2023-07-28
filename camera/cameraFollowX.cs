using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowX : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    Transform camera;
    Vector3 cameraVector, playerVector;
    public float followSpeed = 1f;

    void Start()
    {
         camera = GetComponent<Transform>();

        //camera.position - Player.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Player.position.x >= 0 && Player.position.x <= 220)
        {
            camera.position = new Vector3(Player.position.x, camera.position.y, camera.position.z);
        }
    }
}
