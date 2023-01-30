using System;
using Unity.VisualScripting;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    private static UserInputManager _instance;
    public static UserInputManager Instance => _instance;
    
    [SerializeField] private HandInputHandler _leftController;
    [SerializeField] private HandInputHandler _rightController;
    [SerializeField] private GameObject _leftHandRay;
    [SerializeField] private GameObject _rightHandRay;

    public Action<Vector3> OnLeftTriggerPressed;
    public Action<Vector3> OnRightTriggerPressed;
    public Action OnPrimaryButtonPressed;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    private void Start()
    {
        _leftController.OnTriggerPressed += LeftTriggerPressed;
        _rightController.OnTriggerPressed += RightTriggerPressed;
        _rightController.OnPrimaryButtonPressed += PrimaryButtonPressed;
        _rightController.OnPrimaryButtonPressed += PrimaryButtonPressed;
    }

    private void OnDestroy()
    {
        _leftController.OnTriggerPressed -= LeftTriggerPressed;
        _rightController.OnTriggerPressed -= RightTriggerPressed;
        _rightController.OnPrimaryButtonPressed -= PrimaryButtonPressed;
        _rightController.OnPrimaryButtonPressed -= PrimaryButtonPressed;
    }

    private void PrimaryButtonPressed()
    {
        OnPrimaryButtonPressed?.Invoke();
    }

    private void LeftTriggerPressed()
    {
        OnLeftTriggerPressed?.Invoke(_leftController.transform.position);
    }
    
    private void RightTriggerPressed()
    {
        OnRightTriggerPressed?.Invoke(_rightController.transform.position);
    }

    public void EnableHandRays(bool isEnabled)
    {
        _leftHandRay.SetActive(isEnabled);
        _rightHandRay.SetActive(isEnabled);
    }
}
