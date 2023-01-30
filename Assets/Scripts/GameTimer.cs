using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float _remainingTime = 30.0f;
    public Action OnTimerEnded;

    private bool _isStarted;
    private void Update()
    {
        if(!_isStarted)
            return;

        _remainingTime -= Time.deltaTime;
 
        if (_remainingTime <= 0)
        {
            TimerEnd();
        }
 
    }
    
    private void TimerEnd()
    {
        _isStarted = false;
        OnTimerEnded?.Invoke();
    }
    public void SetTime(float timeInSeconds)
    {
        _remainingTime = timeInSeconds;
    }

    public void StartTimer()
    {
        _isStarted = true;
    }

    public float GetRemainingTime()
    {
        return _remainingTime;
    }

}
