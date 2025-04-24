using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera targetCamera; // Assign the main camera in the Inspector

    void Update()
    {
        if (targetCamera == null)
            targetCamera = Camera.main; // Auto-assign main camera if not set

        // Make the object face the camera
        transform.LookAt(targetCamera.transform);

        // Optional: Rotate 180 degrees to ensure correct orientation
        transform.Rotate(0, 180f, 0);
    }
}