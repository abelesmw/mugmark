using UnityEngine;
using UnityEngine.SceneManagement;

public class go_to_world_map : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("world_map");
        }
    }
}
