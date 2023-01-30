using System;
using System.Threading.Tasks;
using UnityEngine;

public class CalibrationManager : MonoBehaviour
{
    private static CalibrationManager _instance;
    public static CalibrationManager Instance => _instance;
    public enum CalibrationSteps
    {
        NotStarted,
        Left,
        Right,
        Forward,
        Finished
    }

    public Action<CalibrationSteps> OnCalibrationStep;
    
    private CalibrationSteps _currentCalibrationStep;

    private CalibrationSteps CurrentCalibrationStep
    {
        get => _currentCalibrationStep;
        set
        {
            _currentCalibrationStep = value;
            OnCalibrationStep?.Invoke(_currentCalibrationStep);
        }
    }

    [SerializeField] private GameObject _positionPrefab;
    [SerializeField] private Transform _container;

    private Vector3 _selectedLeftReach = Vector3.zero;
    private Vector3 _selectedRightReach = Vector3.zero;
    private Vector3 _selectedForwardReach = Vector3.zero;

    private GameObject _leftCalibrationGO;
    private GameObject _rightCalibrationGO;
    private GameObject _forwardCalibrationGO;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    private void Start()
    {
        UserInputManager.Instance.OnLeftTriggerPressed += LeftTriggerPressed;
        UserInputManager.Instance.OnRightTriggerPressed += RightTriggerPressed;
    }

    public async void StartCalibration()
    {
        ResetCalibratedValues();
        CurrentCalibrationStep = CalibrationSteps.Left;
        while (_selectedLeftReach == Vector3.zero)
        {
            await Task.Delay(30);
        }
        
        _leftCalibrationGO = Instantiate(_positionPrefab,_container);
        _leftCalibrationGO.transform.position = _selectedLeftReach;

        CurrentCalibrationStep = CalibrationSteps.Right;
        while (_selectedRightReach == Vector3.zero)
        {
            await Task.Delay(30);
        }
        _rightCalibrationGO = Instantiate(_positionPrefab,_container);
        _rightCalibrationGO.transform.position = _selectedRightReach;

        CurrentCalibrationStep = CalibrationSteps.Forward;
        while (_selectedForwardReach == Vector3.zero)
        {
            await Task.Delay(30);
        }
        _forwardCalibrationGO = Instantiate(_positionPrefab,_container);
        _forwardCalibrationGO.transform.position = _selectedForwardReach;

        CalibrationData.SetCalibrationData(_selectedLeftReach,_selectedRightReach,_selectedForwardReach);
        CurrentCalibrationStep = CalibrationSteps.Finished;
        foreach (Transform pole in _container)
        {
            Destroy(pole.gameObject);
        }
    }

    private void ResetCalibratedValues()
    {
        _selectedLeftReach = Vector3.zero;
        _selectedRightReach = Vector3.zero;
        _selectedForwardReach = Vector3.zero;
    }

    private void LeftTriggerPressed(Vector3 worldPos)
    {
        switch (_currentCalibrationStep)
        {
            case CalibrationSteps.Left:
                _selectedLeftReach = worldPos;
                break;
            case CalibrationSteps.Forward:
                _selectedForwardReach = worldPos;
                break;
        }
    }

    private void RightTriggerPressed(Vector3 worldPos)
    {
        switch (_currentCalibrationStep)
        {
            case CalibrationSteps.Right:
                _selectedRightReach = worldPos;
                break;
            case CalibrationSteps.Forward:
                _selectedForwardReach = worldPos;
                break;
        }
    }

    private void FinalizeCalibration()
    {
        CalibrationData.SetCalibrationData(_selectedLeftReach,_selectedRightReach,_selectedForwardReach);
        Destroy(_leftCalibrationGO);
        Destroy(_rightCalibrationGO);
        Destroy(_forwardCalibrationGO);
    }
}
