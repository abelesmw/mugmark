using UnityEngine;

    public class cloudBackground: MonoBehaviour
    {

    public GameObject Cloud;
    public float cloudTimer;
    public float timeToNextCloud;
    System.Random rand = new System.Random();
    int nextCloudDistance;
    int nextCloudHeight;
    int lastCloudPosition;

    private void Start()
    {
        
    }

    private void Update()
    {
        cloudTimer += Time.deltaTime;

        if(cloudTimer > timeToNextCloud)
        {
            Instantiate(Cloud, new Vector3(lastCloudPosition + nextCloudDistance, nextCloudHeight), Cloud.transform.rotation);
        }
    }
}

