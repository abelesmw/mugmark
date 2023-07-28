using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blob_ai : MonoBehaviour
{
    Rigidbody2D myRB;
    Animator myAnim;
    Transform myTransform;
    Vector3 localScale;
    public float moveSpeed;
    public float maxSpeed;

    public GameObject player;
    public GameObject blob;
    public GameObject cloudHomie;

    //float blobCount = 0;

    public float timeToNextBlob = 2;
    public float timeToNextCloud = 4;
    public float blobTimer;
    public float cloudTimer;
    public float blobDeathTimer;

    bool passedBlobs = false;
    bool passedClouds = false;

    System.Random rand = new System.Random();
    int yRand;
    int xRand;
    int cloudYRand;
    int cloudXRand;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        localScale = transform.localScale;
    }

    void Update()
    {
        yRand = rand.Next(1, 3);
        xRand = rand.Next(10, 14);
        cloudXRand = rand.Next(14, 17);
        cloudYRand = rand.Next(5, 9);
        blobTimer += Time.deltaTime;
        blobDeathTimer += Time.deltaTime;
        cloudTimer += Time.deltaTime;

        if(player.transform.position.x > 20 && player.transform.position.x < 45 && blobTimer > timeToNextBlob && !passedBlobs)
        {
            Instantiate(blob, new Vector3(player.transform.position.x + xRand, yRand), blob.transform.rotation);
            blobTimer = 0;
        }
        else if (player.transform.position.x > 45 && player.transform.position.x <= 80 && blobTimer > timeToNextBlob && !passedBlobs)
        {
            Instantiate(blob, new Vector3(player.transform.position.x + xRand, yRand + 3), blob.transform.rotation);
            blobTimer = 0;
        }
        else if (player.transform.position.x > 80 && player.transform.position.x <= 114 && cloudTimer > timeToNextCloud && !passedClouds)
        {
            Instantiate(cloudHomie, new Vector3(player.transform.position.x + 17, 7.5f), cloudHomie.transform.rotation);
            cloudTimer = 0;
            
        }
        else if (player.transform.position.x > 114 && player.transform.position.x <= 118 && cloudTimer > timeToNextCloud && !passedClouds)
        {
            Instantiate(cloudHomie, new Vector3(player.transform.position.x + 16, 9f), cloudHomie.transform.rotation);
            cloudTimer = 0;

        }
        else if (player.transform.position.x > 118 && player.transform.position.x <= 132 && cloudTimer > timeToNextCloud && !passedClouds)
        {
            Instantiate(cloudHomie, new Vector3(player.transform.position.x + 15, 10.5f), cloudHomie.transform.rotation);
            cloudTimer = 0;

        }
        else if(player.transform.position.x > 132 && player.transform.position.x <=195 && cloudTimer > timeToNextCloud && !passedClouds && blobTimer > timeToNextBlob && !passedBlobs )
        {
            Instantiate(cloudHomie, new Vector3(player.transform.position.x + 13, 13f), cloudHomie.transform.rotation);
            cloudTimer = 0;
            Instantiate(blob, new Vector3(player.transform.position.x + xRand - 1.5f, yRand + 9), blob.transform.rotation);
            blobTimer = 0;
        }
        else if (player.transform.position.x > 195)
        {
            passedClouds = true;
            passedBlobs = true;
        }
    }
}