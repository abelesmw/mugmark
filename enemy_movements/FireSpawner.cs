using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject miniFirePrefab;
    public float spawnInterval = 5f;
    private float timeSinceLastSpawn;
    public Transform target;
    public float distanceToPlayer = 20f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastSpawn = 4;
    }

    void Update()
    {
        if(transform.position.x - target.position.x < distanceToPlayer)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                Instantiate(miniFirePrefab, transform.position, Quaternion.identity);
                timeSinceLastSpawn = 0f;
            }
        }

    }
}

