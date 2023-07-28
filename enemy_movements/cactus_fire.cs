using UnityEngine;
using System.Collections;

public class cactus_fire : MonoBehaviour
{
    public Transform target;
    public float upSpeed = .25f;
    public float speed = 5f;
    private bool flyLeft = false;
    private float currentDistance;
    private float distanceToSubtract;
    public float subtractingDistance = 10f;
    private float positionToFlyX;
    private float positionToFlyY;

    void Start()
    {
        StartCoroutine(Fly());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        positionToFlyX = target.position.x;
        positionToFlyY = target.position.y;
    }

    private void Update()
    {
        currentDistance = transform.position.x - target.position.x;
        distanceToSubtract = currentDistance - subtractingDistance;
    }

    IEnumerator Fly()
    {
        //Fly up
        float timeToReachTop = .25f;
        float startTime = Time.time;
        while (Time.time - startTime < timeToReachTop)
        {
            float progress = (Time.time - startTime) / timeToReachTop;
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * upSpeed, progress);
            yield return null;
        }

        //Slowly rotate
        for (float i = 0; i <= 90; i += 6)
        {
            transform.eulerAngles = new Vector3(0, 0, i);
            yield return new WaitForSeconds(0.02f);
        }

        //Fly towards target
        while (!flyLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(positionToFlyX - 10, positionToFlyY -3, 0), speed * Time.deltaTime);
            if (transform.position.x <= positionToFlyX - 10)
            {
                flyLeft = true;
            }
            yield return null;
        }
        //Fly Left
        while (flyLeft)
        {
            transform.position += (transform.right * -30f) * Time.deltaTime;
            yield return null;
        }
    }
}
