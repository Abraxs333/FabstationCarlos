using UnityEngine;

public class GlobalVariables
{
    private static GlobalVariables instance;
    public static GlobalVariables Instance
    {
        get
        {
            if (instance == null)
                instance = new GlobalVariables();
            return instance;
        }
    }

    // Stored values for Camera
    public Vector3 CameraInitialPosition { get; private set; }
    public Quaternion CameraInitialRotation { get; private set; }
    public Vector3 CameraInitialScale { get; private set; }

    // Stored values for Target Object
    public Vector3 TargetObjectInitialPosition { get; private set; }
    public Quaternion TargetObjectInitialRotation { get; private set; }
    public Vector3 TargetObjectInitialScale { get; private set; }

    // Store initial values for the Camera
    public void StoreCameraInitialValues(Transform cameraTransform)
    {
        CameraInitialPosition = cameraTransform.position;
        CameraInitialRotation = cameraTransform.rotation;
        CameraInitialScale = cameraTransform.localScale;
    }

    // Store initial values for the Target Object
    public void StoreTargetObjectInitialValues(Transform targetTransform)
    {
        TargetObjectInitialPosition = targetTransform.position;
        TargetObjectInitialRotation = targetTransform.rotation;
        TargetObjectInitialScale = targetTransform.localScale;
    }
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

