using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalibrationData
{
    public static Vector3 MaxLeftPosition { get; private set; }
    public static Vector3 MaxRightPosition { get; private set; }
    public static Vector3 MaxForwardPosition { get; private set; }

    public static void SetCalibrationData(Vector3 left, Vector3 right, Vector3 forward)
    {
        MaxLeftPosition = left;
        MaxRightPosition = right;
        MaxForwardPosition = forward;
    }
}
