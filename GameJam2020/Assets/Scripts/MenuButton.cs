using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Play button pressed.");
        StartCoroutine(ChangeLevel());
    }

    private static IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Quit()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }
}
