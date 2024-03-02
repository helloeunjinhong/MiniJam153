using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetInputMapBasedOnScene(SceneManager.GetActiveScene().name);
    }

    public void GotoScene(string nextSceneName)
    {
        SetInputMapBasedOnScene(nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    private void SetInputMapBasedOnScene(string sceneName)
    {
        if(sceneName == "TitleScene")
            InputManager.Get.ChangeInputMap(ActionCategory.MENU_MAIN);
        else if(sceneName == "MainScene")
            InputManager.Get.ChangeInputMap(ActionCategory.PLAYER);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
