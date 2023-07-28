using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerPlatform : MonoBehaviour
{
    public float moveAmount = 1f;
    public float moveDuration = 1f;
    private Vector3 startingPosition;
    private bool isMoving = false;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            collision.transform.parent = transform;
            StartCoroutine(MoveObject());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true;
            collision.transform.parent = null;
            StartCoroutine(MoveBack());
        }
    }

    public IEnumerator MoveObject()
    {
        float currentTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);

        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, currentTime / moveDuration);
            yield return null;
        }
        isMoving = false;
    }

    public IEnumerator MoveBack()
    {
        float currentTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);

        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, currentTime / moveDuration);
            yield return null;
        }
        isMoving = false;
    }
}
