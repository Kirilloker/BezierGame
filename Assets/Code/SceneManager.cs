using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void GoToMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            GoToMainMenuScene();

        if(Input.GetKeyDown(KeyCode.Space))
            GoToGameScene();
    }
}
