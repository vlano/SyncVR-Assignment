using System;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandInputHandler : MonoBehaviour
{
    public Action OnTriggerPressed;
    public Action OnPrimaryButtonPressed;

    [SerializeField] private InputActionProperty _pinchAction;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private InputActionProperty _buttonAction;

    private Animator _handAnimator;
    private static readonly int TRIGGER = Animator.StringToHash("Trigger");
    private static readonly int GRIP = Animator.StringToHash("Grip");

    private void Awake()
    {
        if (_handAnimator == null)
            _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        var trigger = _pinchAction.action.ReadValue<float>();
        var grip = _gripAction.action.ReadValue<float>();
        _handAnimator.SetFloat(TRIGGER,trigger);
        _handAnimator.SetFloat(GRIP,grip);

        if (_gripAction.action.WasPressedThisFrame())
        {
            OnTriggerPressed?.Invoke();
        }
        if (_buttonAction.action.WasPressedThisFrame())
        {
            Debug.Log("PRESSED");
            OnPrimaryButtonPressed?.Invoke();
        }

    }
}
