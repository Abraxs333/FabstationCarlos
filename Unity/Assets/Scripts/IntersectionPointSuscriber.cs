using UnityEngine;

public class IntersectionPointSubscriber : MonoBehaviour
{
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
        Debug.Log("Reacting to intersection at: " + newPoint);
        // Add any logic here to handle the event
    }
}