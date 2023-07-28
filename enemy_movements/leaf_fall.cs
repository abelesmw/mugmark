using System.Collections;
using UnityEngine;

public class leaf_fall : MonoBehaviour
{
    public GameObject leafPrefab;
    public GameObject small_gust;
    public Transform mugMark;
    public float minInterval = 2f;
    public float maxInterval = 3;

    public int minDistance = 3;
    public int maxDistance = 6;
    public float leafHeight;
    private float leafInFrontDistance;
    public float startLeaves;
    public float gust_height;
    public bool in_front_leaf;

    private Camera mainCamera;

    System.Random rand = new System.Random();
    int xRand;

    private float nextLeafTime;

    void Start()
    {
        nextLeafTime = Time.time + Random.Range(minInterval, maxInterval);
        leafInFrontDistance = Random.Range(minDistance, maxDistance);
        mainCamera = Camera.main;
    }

    void Update()
    {
        xRand = rand.Next(minDistance, maxDistance);
        float topViewY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y;
        if (mugMark.position.x > startLeaves && mugMark.position.x < 49)
        { 
            if (Time.time > nextLeafTime)
            {
                // instantiate leaf object in front of mugMark
                Vector3 leafPosition = new Vector3(mugMark.position.x + xRand, mugMark.position.y + leafHeight, mugMark.position.z);
                Instantiate(leafPrefab, leafPosition, Quaternion.identity);
                var cloneGust = Instantiate(small_gust, new Vector3(leafPosition.x, topViewY - .9f, 0), Quaternion.Euler(new Vector3(0,0,38.53f)));                
                Destroy(cloneGust, .7f);

                // set the next time to instantiate a leaf
                nextLeafTime = Time.time + Random.Range(minInterval, maxInterval);
            }
        } else if (mugMark.position.x >= 49 && mugMark.position.x < 54)
        {
            if (Time.time > nextLeafTime)
            {
                if (in_front_leaf)
                {
                    Vector3 leafPosition = new Vector3(mugMark.position.x + xRand, mugMark.position.y + leafHeight + 4, mugMark.position.z);
                    Instantiate(leafPrefab, leafPosition, Quaternion.identity);
                    var cloneGust = Instantiate(small_gust, new Vector3(leafPosition.x, topViewY - .9f, 0), Quaternion.Euler(new Vector3(0, 0, 38.53f)));
                    Destroy(cloneGust, .7f);
                    nextLeafTime = Time.time + Random.Range(minInterval, maxInterval);
                }
                else
                {
                    //doNothing
                }
            }
        } else if (mugMark.position.x >= 54)
        {
            if (Time.time > nextLeafTime)
            {
                Vector3 leafPosition = new Vector3(mugMark.position.x + xRand, mugMark.position.y + leafHeight, mugMark.position.z);
                Instantiate(leafPrefab, leafPosition, Quaternion.identity);
                var cloneGust = Instantiate(small_gust, new Vector3(leafPosition.x, topViewY - .9f, 0), Quaternion.Euler(new Vector3(0, 0, 38.53f)));
                Destroy(cloneGust, .7f);

                // set the next time to instantiate a leaf
                nextLeafTime = Time.time + Random.Range(minInterval, maxInterval);
            }
        }
    }
}