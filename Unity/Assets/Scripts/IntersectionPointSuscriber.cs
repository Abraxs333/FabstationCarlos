using UnityEngine;
using UnityEngine.Events;

public class IntersectionPointSubscriber : MonoBehaviour
{
    // UnityEvents that can be assigned in the Unity Editor
    public static UnityEvent<Vector3> OnIntersectionDetected = new UnityEvent<Vector3>();


    void OnEnable()
    {
        MeshIntersectionFinder.OnIntersectionPointUpdated.AddListener(HandleIntersectionUpdate);
    }

    void OnDisable()
    {
        MeshIntersectionFinder.OnIntersectionPointUpdated.RemoveListener(HandleIntersectionUpdate);
    }

    void HandleIntersectionUpdate(Vector3 newPoint)
    {
        LogTools.Print(this, LogTools.LogType.Input, "Reacting to intersection at: " + newPoint);

        // Invoke events so they can be set in the Editor
        OnIntersectionDetected.Invoke(newPoint);
   
    }
}