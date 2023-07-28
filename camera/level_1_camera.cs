using UnityEngine;

public class level_1_camera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 1f;
    private float originalHeight;
    public float startX;

    public float targetHeight1 = 4f;
    public float startGoingUp1 = 110f;
    public float finishGoingUp1 = 140f;

    public float targetHeightMiddle;
    public float startGoingUpMiddle;
    public float finishGoingUpMiddle;

    public float targetHeight2;
    public float startGoingUp2;
    public float finishGoingUp2;

    public PlayerController PS;

    void Start()
    {
        originalHeight = transform.position.y;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        PS = g.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.position.x >= startX && (player.position.x < startGoingUp1 || player.position.x >= finishGoingUp1)
            && (player.position.x < startGoingUp2 || player.position.x > finishGoingUp2)
            && (player.position.x < startGoingUpMiddle || player.position.x > finishGoingUpMiddle))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.position.x;
            transform.position = newPosition;
        }
        else if (player.position.x >= startGoingUp1 && player.position.x < finishGoingUp1)
        {
            float playerX = Mathf.InverseLerp(startGoingUp1, finishGoingUp1, player.position.x);
            float targetY = Mathf.Lerp(originalHeight, targetHeight1, playerX);
            Vector3 newPosition = new Vector3(player.position.x, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }

        else if (player.position.x >= startGoingUpMiddle && player.position.x < finishGoingUpMiddle)
        {
            float playerX = Mathf.InverseLerp(startGoingUpMiddle, finishGoingUpMiddle, player.position.x);
            float targetY = Mathf.Lerp(targetHeight1, targetHeightMiddle, playerX);
            Vector3 newPosition = new Vector3(player.position.x, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }

        /////ORIGINAL HEIGHT IS THE PROBLEM, NEED NEW VARIABLE FOR NEW HEIGHT
        else if (player.position.x >= startGoingUp2 && player.position.x < finishGoingUp2)
        {
             float playerX = Mathf.InverseLerp(startGoingUp2, finishGoingUp2, player.position.x);
             float targetY = Mathf.Lerp(targetHeightMiddle, targetHeight2, playerX);
             Vector3 newPosition = new Vector3(player.position.x, targetY, transform.position.z);
             transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);           
        }

        else if (player.position.x < startX)
        {
            //do nothing
        }
    }
}
