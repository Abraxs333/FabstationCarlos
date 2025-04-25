using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    Transform TargetObjectInitialTransform;
    Transform CameraInitialTransform;
}

public enum GameState
{
    MeasureAngle,
    PivotRotation,
    Minimap,
    PartSelector,
    Tools,
    ViewChange,
    NoState
}

