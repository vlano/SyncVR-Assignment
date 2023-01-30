using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager _instance;
    public static InGameUIManager Instance => _instance;
    
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private TMP_Text _timerDisplay;
    [SerializeField] private GameObject _popUp;
    [SerializeField] private TMP_Text _popUpText;
    [SerializeField] private Button _popUpButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseMainMenuButton;
    [SerializeField] private Button _pauseCancelButton;

    public Action OnBackToMainMenuPressed;
    public Action OnPauseMenuOpened;
    public Action OnPauseMenuCanceled;
    
    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    private void Start()
    {
        _popUpButton.onClick.AddListener(GoBackToMainMenu);
        UserInputManager.Instance.OnPrimaryButtonPressed += OpenPauseMenu;
        _pauseMainMenuButton.onClick.AddListener(GoBackToMainMenu);
        _pauseCancelButton.onClick.AddListener(HidePauseMenu);
    }

    private void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);
        OnPauseMenuCanceled?.Invoke();
    }

    private void OnDestroy()
    {
        UserInputManager.Instance.OnPrimaryButtonPressed -= OpenPauseMenu;
    }

    private void OpenPauseMenu()
    {
        if(_popUp.activeSelf)
            return;
        _pauseMenu.SetActive(true);
        OnPauseMenuOpened?.Invoke();
    }

    private void GoBackToMainMenu()
    {
        OnBackToMainMenuPressed?.Invoke();
    }

    private void ClosePopUp()
    {
        _popUp.SetActive(false);
    }
    public void UpdateScoreDisplay(int newScore)
    {
        _scoreDisplay.text = newScore.ToString();
    }
    

    public void UpdateTimerDisplay(float timeLeft)
    {
        _timerDisplay.text = timeLeft.ToString("N0");
    }

    public void ShowPopUp(string text)
    {
        _popUp.SetActive(true);
        _popUpText.text = text;
    }
}
