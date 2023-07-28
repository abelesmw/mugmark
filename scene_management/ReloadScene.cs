using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    public void Reload()
    {
        print("button clicked");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
