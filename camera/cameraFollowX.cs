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

    private void LateUpdate()
    {
            
            //camera.position = Vector3.Lerp(transform.position, new Vector3(Player.position.x, Player.position.y, -10f), .1f);
            //camera.position = Vector3.Lerp(camera.position, new Vector3(Player.position.x, Player.position.y, Player.position.z), smoothing * Time.deltaTime);
        

        // camera.position = Vector3.Lerp(camera.position, playerVector, smoothing * Time.deltaTime);
        /* if(Player.position.x >= 15 && Player.position.x < 90)
         {
             camera.position = new Vector3(Player.position.x, camera.position.y, camera.position.z);
         }*/

    }
}
