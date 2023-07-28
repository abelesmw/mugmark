using UnityEngine;
using System.Collections;

public class go_to_grass_level : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject objectToEnable;
    public GameObject pressXtoEnable;
    public GameObject objectToDisable;

    private void OnTriggerStay2D(Collider2D player)
    {
        pressXtoEnable.SetActive(true);

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressXtoEnable.SetActive(false);
    }

    IEnumerator FadeToBlack()
    {
        objectToEnable.SetActive(true);
        objectToDisable.SetActive(false);
        yield return new WaitForSeconds(.25f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
