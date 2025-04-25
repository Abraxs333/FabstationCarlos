using UnityEngine;

public class GlobalVariables
{
    // Singleton instance
    private static GlobalVariables instance;

    // Public property to access the instance
    public static GlobalVariables Instance
    {
        get
        {
            if (instance == null)
                instance = new GlobalVariables(); // Create instance on first access
            return instance;
        }
    }

    // Private constructor prevents external instantiation
    private GlobalVariables() { }

    // Global variables
    public Transform TargetObjectInitialTransform { get; set; }
    public Transform CameraInitialTransform { get; set; }
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

