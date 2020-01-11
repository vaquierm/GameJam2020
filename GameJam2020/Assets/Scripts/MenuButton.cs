using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Play button pressed.");
        SceneHelper.NextScene();
    }

    public void Quit()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }
}
