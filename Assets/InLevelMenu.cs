using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InLevelMenu : MonoBehaviour
{
    [SerializeField] GameObject inLevelMenuPanel;

    string sceneName;

    private void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }

    public void onRestartClicked()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void onExitClicked()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void onCancelClicked()
    {
        inLevelMenuPanel.SetActive(false);
    }
}
