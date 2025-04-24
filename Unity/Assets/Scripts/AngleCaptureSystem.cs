using UnityEngine;

public class AngleCaptureSystem : MonoBehaviour
{
    public enum CaptureState { WaitingForPointA, WaitingForPivot, WaitingForPointB, Complete }
    public CaptureState currentState = CaptureState.WaitingForPointA;

    public Vector3 pointA;    
    public Vector3 pivotPoint;    
    public Vector3 pointB;
    

    [SerializeField] AngleLabelManager angleLabelManager;
    [SerializeField] PointMarkerManager pointMarkerManager;





    void OnEnable()
    {
        // Subscribe to the IntersectionPointSubscriber event
        IntersectionPointSubscriber.OnIntersectionDetected.AddListener(HandleIntersection);
        LogTools.Print(this, LogTools.LogType.Angle, "Please Select Point A");
    }

    void OnDisable()
    {
        // Unsubscribe when this system is disabled
        IntersectionPointSubscriber.OnIntersectionDetected.RemoveListener(HandleIntersection);
    }

    void HandleIntersection(Vector3 detectedPoint)
    {
        switch (currentState)
        {
            case CaptureState.WaitingForPointA:
                pointA = detectedPoint;
                LogTools.Print(this, LogTools.LogType.Angle, "Point A recorded: " + pointA);
                pointMarkerManager.PlaceMarker(pointA, "Point A"); // Place Marker                
                currentState = CaptureState.WaitingForPivot; // Move to next state
                LogTools.Print(this, LogTools.LogType.Angle, "Please Select Pivot Point");
                break;

            case CaptureState.WaitingForPivot:
                pivotPoint = detectedPoint;
                LogTools.Print(this, LogTools.LogType.Angle, "Pivot Point recorded: " + pivotPoint);
                pointMarkerManager.PlaceMarker(pivotPoint, "Pivot Point"); 
               
                currentState = CaptureState.WaitingForPointB; // Move to next state
                LogTools.Print(this, LogTools.LogType.Angle, "Please Select Point B");
                break;

            case CaptureState.WaitingForPointB:
                pointB = detectedPoint;
                LogTools.Print(this, LogTools.LogType.Angle, "Point B recorded: " + pointB);
                pointMarkerManager.PlaceMarker(pointB, "Point B");                
                ComputeAngle(); // All points are captured, calculate the angle
                currentState = CaptureState.Complete; // Transition to final state
                pointMarkerManager.UpdateLines();
                break;
        }
    }

    void ComputeAngle()
    {
        float angle = AngleTools.CalculateAngle(pointA, pointB, pivotPoint);
        LogTools.Print(this, LogTools.LogType.Angle, $"Computed Angle: {angle}° \nPoint A: {pointA} \nPivot: {pivotPoint} \nPoint B: {pointB}");

        // Call the label manager to update the TextMeshPro label
        angleLabelManager.UpdateAngleLabel(pivotPoint, angle);

    }

    public void ResetSystem()
    {
        LogTools.Print(this, LogTools.LogType.Angle, "Resetting system...");

        // Reset state to first step
        currentState = CaptureState.WaitingForPointA;

        // Clear stored points
        pointA = Vector3.zero;
        pivotPoint = Vector3.zero;
        pointB = Vector3.zero;

        // Clear all markers and lines
        pointMarkerManager.ClearAllMarkers();
    }

}