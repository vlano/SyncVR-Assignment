using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Multiline]
    [SerializeField] private string _leftCalibrationText;
    [Multiline]
    [SerializeField] private string _rightCalibrationText;
    [Multiline]
    [SerializeField] private string _forwardCalibrationText;
    [Multiline]
    [SerializeField] private string _finishedCalibrationText;
    
    [SerializeField] private GameObject _pleaseCalibrateText;
    [SerializeField] private GameObject _howToPlayMenu;
    [SerializeField] private GameObject _rootMenuItems;
    [SerializeField] private GameObject _calibrationMenu;
    [SerializeField] private TMP_Text _calibrationText;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _calibrationButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _calibrationOKButton;
    [SerializeField] private Button _howToPlayBackButton;
    [SerializeField] private Button _howToPlayButton;
    

    public Action OnStartButtonPressed;
    public Action OnExitButtonPressed;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartButtonPressed);
        _exitButton.onClick.AddListener(ExittButtonPressed);
        _calibrationButton.onClick.AddListener(CalibrationButtonPressed);
        _howToPlayButton.onClick.AddListener(OpenHowToPlayMenu);
        _howToPlayBackButton.onClick.AddListener(OpenMainMenu);
        _calibrationOKButton.onClick.AddListener(OpenMainMenu);

        CalibrationManager.Instance.OnCalibrationStep += CalibrationProgressUpdate;
        
        OpenMainMenu();
    }

    private void OpenHowToPlayMenu()
    {
        _rootMenuItems.SetActive(false);
        _howToPlayMenu.SetActive(true);
    }

    private void CalibrationProgressUpdate(CalibrationManager.CalibrationSteps step)
    {
        switch (step)
        {
            case CalibrationManager.CalibrationSteps.Left:
                SetCalibrationInstruction(_leftCalibrationText);
                break; 
            case CalibrationManager.CalibrationSteps.Right:
                SetCalibrationInstruction(_rightCalibrationText);
                break;
            case CalibrationManager.CalibrationSteps.Forward:
                SetCalibrationInstruction(_forwardCalibrationText);
                break;
            case CalibrationManager.CalibrationSteps.Finished:
                SetCalibrationInstruction(_finishedCalibrationText);
                _calibrationOKButton.gameObject.SetActive(true);
                break;
        }
    }

    

    private void OpenMainMenu()
    {

        _startGameButton.gameObject.SetActive(CalibrationData.MaxForwardPosition != Vector3.zero);
        _pleaseCalibrateText.SetActive(CalibrationData.MaxForwardPosition == Vector3.zero);
        
        _calibrationOKButton.gameObject.SetActive(false);
        _calibrationMenu.SetActive(false);
        _howToPlayMenu.SetActive(false);
        _rootMenuItems.SetActive(true);
    }

    private void CalibrationButtonPressed()
    {
        _rootMenuItems.SetActive(false);
        _calibrationMenu.SetActive(true);
        CalibrationManager.Instance.StartCalibration();
    }

    private void ExittButtonPressed()
    {
        OnExitButtonPressed?.Invoke();
    }

    private void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
    }

    public void SetCalibrationInstruction(string text)
    {
        _calibrationText.text = text;
    }

    public void ShowMainMenu(bool isShown)
    {
        _rootMenuItems.gameObject.SetActive(isShown);
    }
}
