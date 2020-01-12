using UnityEngine;

public class GameOverButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneHelper.ResetScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
