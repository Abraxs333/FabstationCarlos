using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public Transform targetObject; // Assign the object to track in the Inspector
    public Vector3 offset = new Vector3(0, 10, 0); // Fixed relative position
    public Camera minimapCamera; // Assign the minimap camera
    public float paddingMultiplier = 1.2f; // Extra padding around object in minimap

    void LateUpdate()
    {
        if (targetObject != null)
        {
            // Keep minimap camera at a fixed offset relative to the target
            transform.position = targetObject.position + offset;

            // Adjust camera view to fit the object’s bounding box
            AdjustCameraView();
        }
    }

    void AdjustCameraView()
    {
        if (targetObject.TryGetComponent(out Renderer objectRenderer))
        {
            Bounds bounds = objectRenderer.bounds;

            // Calculate size based on bounding box
            float maxExtent = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);

            // Adjust orthographic camera size (for 2D minimap)
            if (minimapCamera.orthographic)
            {
                minimapCamera.orthographicSize = maxExtent * paddingMultiplier;
            }
            // Adjust perspective camera distance (for 3D minimap)
            else
            {
                minimapCamera.transform.position = targetObject.position + new Vector3(0, maxExtent * paddingMultiplier, 0);
                minimapCamera.transform.LookAt(targetObject.position);
            }
        }
    }
}