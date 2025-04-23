using UnityEngine;
using UnityEngine.Events;

public class MeshIntersectionFinder : MonoBehaviour
{
    public static Vector3 IntersectionPoint;
    public MeshFilter targetMeshFilter; // Assign in Inspector
    public Transform trackedObject; // The GameObject whose rotation we're tracking

    private Quaternion initialRotation; // Stores rotation at press start

    // Unity Event to notify when an intersection is found
    public static UnityEvent<Vector3> OnIntersectionPointUpdated = new UnityEvent<Vector3>();

    void Update()
    {
        // When mouse button is pressed, store the current rotation
        if (Input.GetMouseButtonDown(0))
        {
            if (trackedObject != null)
            {
                initialRotation = trackedObject.rotation; // Capture rotation at press start
            }
        }

        // When button is released, check rotation before finding intersection
        if (Input.GetMouseButtonUp(0))
        {
            if (trackedObject != null && trackedObject.rotation == initialRotation)
            {
                FindMeshIntersection(Input.mousePosition); // Only check if rotation is unchanged
            }
        }
    }

    void FindMeshIntersection(Vector2 pointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        Mesh mesh = targetMeshFilter.mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        bool intersectionFound = false;
        float closestDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        // Loop through all triangles in the mesh
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = targetMeshFilter.transform.TransformPoint(vertices[triangles[i]]);
            Vector3 v1 = targetMeshFilter.transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 v2 = targetMeshFilter.transform.TransformPoint(vertices[triangles[i + 2]]);

            // Check for ray-triangle intersection
            if (RayIntersectsTriangle(ray, v0, v1, v2, out Vector3 intersection))
            {
                float distance = Vector3.Distance(ray.origin, intersection);
                if (distance < closestDistance) // Find closest hit
                {
                    closestDistance = distance;
                    closestPoint = intersection;
                    intersectionFound = true;
                }
            }
        }

        if (intersectionFound)
        {
            IntersectionPoint = closestPoint;
            OnIntersectionPointUpdated.Invoke(IntersectionPoint); // Trigger event

            Debug.Log("Intersection recorded at: " + IntersectionPoint);
        }
    }

    bool RayIntersectsTriangle(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2, out Vector3 intersection)
    {
        intersection = Vector3.zero;

        Vector3 edge1 = v1 - v0;
        Vector3 edge2 = v2 - v0;
        Vector3 h = Vector3.Cross(ray.direction, edge2);
        float a = Vector3.Dot(edge1, h);

        if (a > -Mathf.Epsilon && a < Mathf.Epsilon) return false;

        float f = 1 / a;
        Vector3 s = ray.origin - v0;
        float u = f * Vector3.Dot(s, h);

        if (u < 0.0f || u > 1.0f) return false;

        Vector3 q = Vector3.Cross(s, edge1);
        float v = f * Vector3.Dot(ray.direction, q);

        if (v < 0.0f || (u + v) > 1.0f) return false;

        float t = f * Vector3.Dot(edge2, q);

        if (t > Mathf.Epsilon)
        {
            intersection = ray.origin + ray.direction * t;
            return true;
        }

        return false;
    }
}