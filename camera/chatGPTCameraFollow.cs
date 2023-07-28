using UnityEngine;

public class chatGPTCameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 1f;
    public float targetHeight = 4f;
    private float originalHeight;
    public float startX;
    public float startGoingUp = 110f;
    public float finishGoingUp = 140f;

    void Start()
    {
        originalHeight = transform.position.y;
    }

    void Update()
    {
        if (player.position.x >= startX && (player.position.x < startGoingUp || player.position.x >= finishGoingUp))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.position.x;
            transform.position = newPosition;
        }
        else if (player.position.x >= startGoingUp && player.position.x < finishGoingUp)
        {
            float playerX = Mathf.InverseLerp(startGoingUp, finishGoingUp, player.position.x);
            float targetY = Mathf.Lerp(originalHeight, targetHeight, playerX);
            Vector3 newPosition = new Vector3(player.position.x, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }
        else if (player.position.x < startX)
        {
            //do nothing
        }
    }
}
