using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    GameObject randomCloud;
    public GameObject grayCloudPrefab;
    public GameObject darkGrayCloudPrefab;
    public float timeToNextCloud = 2f;
    public float cloudTimer = 0f;
    public float distanceToNextCloud = 5f;
    System.Random rand = new System.Random();
    System.Random rand2 = new System.Random();
    int xRand;
    int cloudRand;

    private GameObject lastCloud;

    private void Start()
    {
        randomCloud = cloudPrefab;
    }

    void Update()
    {
        cloudTimer += Time.deltaTime;
        xRand = rand.Next(1, 7);
        cloudRand = rand2.Next(1, 3);
        randomClouds();

        if (cloudTimer > timeToNextCloud)
        {
            Vector3 spawnPosition;

            if (lastCloud == null)
            {
                spawnPosition = new Vector3(80, 2f, 0);
            }
            else
            {
                spawnPosition = lastCloud.transform.position + Vector3.right * (distanceToNextCloud + xRand);
            }

            lastCloud = Instantiate(randomCloud, spawnPosition, Quaternion.identity);
            cloudTimer = 0f;
        }
    }

    public void randomClouds()
    {
        switch (cloudRand)
        {
            case 1:
                randomCloud = cloudPrefab;
                break;
            case 2:
                randomCloud = grayCloudPrefab;
                break;
            case 3:
                randomCloud = darkGrayCloudPrefab;
                break;
        }
    }
}
