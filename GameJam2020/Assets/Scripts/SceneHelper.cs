using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    private static readonly List<string> SceneStrings =  new List<string>() { "Lore Scene", "Main Menu", "Game" };
    
    public static void NextScene()
    {
        var activeSceneIndex = GetSceneStringIndex(SceneManager.GetActiveScene().name);

        // Reset to main menu when no more scenes to play
        if (activeSceneIndex >= SceneStrings.Count - 1)
        {
            SceneManager.LoadScene(SceneStrings[1]);
        }
        // Otherwise, go to the next scene
        else
        {
            SceneManager.LoadScene(SceneStrings[activeSceneIndex + 1]);
        }
    }

    private static int GetSceneStringIndex(string activeScene)
    {
        return SceneStrings.FindIndex(s => string.Equals(s, activeScene));
    }
}
