using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Color[] Colors = { Color.blue, Color.red, Color.yellow,Color.green,Color.magenta };
    
    public static int _scoreValue = 10;

    [SerializeField] private GameObject _spawnPointContainer;
    [SerializeField] private GameTimer _timer;
    [SerializeField] private GameObject _spwanPointPrefab;


    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    private void Start()
    {
        PointManager.OnScoreUpdated += UpdateScore;
        PointManager.SetScore(0);
        InGameUIManager.Instance.OnBackToMainMenuPressed += BackToMenuScene;
        InGameUIManager.Instance.OnPauseMenuOpened += PauseGame;
        InGameUIManager.Instance.OnPauseMenuCanceled += ResumeGame;
        _timer.OnTimerEnded += GameEnd;

        LoadGame();
    }

    private void OnDestroy()
    {
        PointManager.OnScoreUpdated -= UpdateScore;
        InGameUIManager.Instance.OnBackToMainMenuPressed -= BackToMenuScene;
        _timer.OnTimerEnded -= GameEnd;
    }

    private void GameEnd()
    {
        PauseGame();
        InGameUIManager.Instance.ShowPopUp($"Congratulations!\n You scored {PointManager.Score} points!");
    }
    private async void LoadGame()
    {
        await StartGame();
    }
    
    private async Task StartGame()
    {
        ResumeGame();
        UserInputManager.Instance.EnableHandRays(false);

        Instantiate(_spwanPointPrefab,CalibrationData.MaxLeftPosition,Quaternion.identity,_spawnPointContainer.transform);
        Instantiate(_spwanPointPrefab,CalibrationData.MaxRightPosition,Quaternion.identity,_spawnPointContainer.transform);
        Instantiate(_spwanPointPrefab,CalibrationData.MaxForwardPosition,Quaternion.identity,_spawnPointContainer.transform);
        
        _timer.SetTime(30f);
        _timer.StartTimer();
    }

    private void UpdateScore(int newScore)
    {
        InGameUIManager.Instance.UpdateScoreDisplay(newScore);
    }

    private void Update()
    { 
        InGameUIManager.Instance.UpdateTimerDisplay(_timer.GetRemainingTime());
    }
    
    private void PauseGame ()
    {
        UserInputManager.Instance.EnableHandRays(true);
        Time.timeScale = 0;
        _spawnPointContainer.SetActive(false);
    }
    private void ResumeGame ()
    {
        UserInputManager.Instance.EnableHandRays(false);
        Time.timeScale = 1;
        _spawnPointContainer.SetActive(true);
    }
    private void BackToMenuScene()
    {
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
    }
}
