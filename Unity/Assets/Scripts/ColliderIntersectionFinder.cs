using UnityEngine;
using UnityEngine.Events;

public class ColliderIntersectionFinder : MonoBehaviour
{
    // Public static variable storing the last intersection point
    public static Vector3 IntersectionPoint;

    // Unity Event that is invoked whenever the intersection point is set
    public static UnityEvent<Vector3> OnIntersectionPointUpdated = new UnityEvent<Vector3>();

    void Update()
    {
        // Register intersection when mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            FindIntersectionPoint(Input.mousePosition);
        }
    }

    void FindIntersectionPoint(Vector2 pointerPosition)
    {
        // Create a ray from the camera through the pointer position
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        RaycastHit hit;

        // Perform raycast to detect intersection with the model
        if (Physics.Raycast(ray, out hit))
        {
            IntersectionPoint = hit.point;

            // Invoke the event, notifying subscribers of the new intersection point
            OnIntersectionPointUpdated.Invoke(IntersectionPoint);

            Debug.Log("Intersection recorded at: " + IntersectionPoint);
        }
    }
}