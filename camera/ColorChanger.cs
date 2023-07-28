using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Camera camera;
    public Transform target;
    public float colorChangeSpeed = 0.5f;
    public float endColorPosition;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Color startColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        Color endColor = Color.black;
        float t = Mathf.Clamp01(target.position.x / endColorPosition);
        if(transform.position.x > 46)
        {
            camera.backgroundColor = Color.Lerp(startColor, endColor, t * colorChangeSpeed);
        }
        
    }
}
