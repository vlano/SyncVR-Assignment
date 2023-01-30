using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInstance<T> : MonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance;

    private void Awake()
    {
        // _instance = this;
    }
}
