using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.OnStartButtonPressed += LoadGame;
        UIManager.Instance.OnExitButtonPressed += QuitGame;
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnStartButtonPressed -= LoadGame;
        UIManager.Instance.OnExitButtonPressed -= QuitGame;
    }

    private async void LoadGame()
    {
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
    private async Task LoadGameScene()
    {
        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        while(!sceneLoadingOperation.isDone)
        {
            await Task.Delay(30);
        }
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}
