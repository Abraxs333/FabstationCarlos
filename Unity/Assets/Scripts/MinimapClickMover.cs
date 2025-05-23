using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapClickMover : MonoBehaviour
{
    [Header("Camera References")]
    public Camera mainCamera;         // Main scene camera
    public Camera minimapCamera;      // The minimap camera (with an offset, top-down view)

    [Header("UI Reference")]
    public RawImage minimapUI;        // The minimap render texture displayed in a UI RawImage

    [Header("Movement Settings")]
    public float moveSpeed = 3f;      // Speed for moving the main camera
    public float camrotationSpeed = .25f;// Speed for rotating the main camera
    public LayerMask raycastLayerMask; // (Optional) Only hit objects on these layers

    // Internal state
    private Ray currentRay;
    private RaycastHit hitInfo;
    private bool raycastHitFound = false;
    private bool isClickingMinimap = false;

    void Update()
    {
        // When the user initially clicks, check if the pointer is over the minimap UI.
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverMinimap())
            {
                isClickingMinimap = true;
                CastMinimapRay(); // initial cast
            }
        }

        // While holding, continuously update the ray and draw the debug ray.
        if (Input.GetMouseButton(0) && isClickingMinimap)
        {
            CastMinimapRay();
            Debug.DrawRay(currentRay.origin, currentRay.direction * 100f, Color.green);
        }

        // On release, if the minimap was clicked and a hit was found, move the main camera.
        if (Input.GetMouseButtonUp(0) && isClickingMinimap)
        {
            isClickingMinimap = false;
            if (raycastHitFound)
            {
                StopAllCoroutines();
                StartCoroutine(MoveMainCameraTo(hitInfo.point));
            }
        }
    }

    // Returns true if the mouse is over the minimap UI element.
    bool IsPointerOverMinimap()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(minimapUI.rectTransform, Input.mousePosition);
    }

    // This function builds a ray using the minimap camera settings.
    void CastMinimapRay()
    {
        // Get the RectTransform for the minimap UI.
        RectTransform rt = minimapUI.rectTransform;

        // Convert the mouse's screen position to local coordinates in the minimap UI.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPoint);

        // Convert that local point (with the pivot assumed at the center) to normalized [0,1] coordinates.
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x + rt.rect.width * 0.5f) / rt.rect.width,
            (localPoint.y + rt.rect.height * 0.5f) / rt.rect.height
        );

        // IMPORTANT:
        // Check (and adjust) the vertical coordinate if your UI�s coordinate system requires it.
        // For example, uncomment the following line if your minimap appears flipped vertically:
        // normalizedPoint.y = 1f - normalizedPoint.y;

        // Using these normalized coordinates, we determine where on the minimap camera�s near plane the click landed.
        Vector3 origin = minimapCamera.ViewportToWorldPoint(new Vector3(normalizedPoint.x, normalizedPoint.y, minimapCamera.nearClipPlane));

        // We want the ray to travel in the direction the minimap camera is �looking.�
        Vector3 direction = minimapCamera.transform.forward;

        // Build our ray.
        currentRay = new Ray(origin, direction);

        // Raycast into the world using the built ray (limit the distance if needed; here 1000 units).
        if (Physics.Raycast(currentRay, out hitInfo, 1000f, raycastLayerMask))
        {
            raycastHitFound = true;
            LogTools.Print(this, LogTools.LogType.Minimap, "Minimap Raycast hit: " + hitInfo.collider.gameObject.name);
        }
        else
        {
            raycastHitFound = false;
        }
        

    }

    public Transform ObjecttoFocus;

    // Smoothly moves the main camera to the target position.
    IEnumerator MoveMainCameraTo(Vector3 targetPosition)
    {
        // Step 1: Pre-Lerp Camera Rotation to Face Target Smoothly
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(ObjecttoFocus.position - mainCamera.transform.position);

        
        float elapsedRotationTime = 0f;

        while (elapsedRotationTime < camrotationSpeed)
        {
            elapsedRotationTime += Time.deltaTime;
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedRotationTime / camrotationSpeed);
            yield return null;
        }


        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // Smoothly move the camera toward the target position
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            mainCamera.transform.LookAt(ObjecttoFocus);
            yield return null;
        }

        // Ensure final position is exactly at the target
        StartCoroutine(SmoothFinalAdjustment(targetPosition));
    }

    IEnumerator SmoothFinalAdjustment(Vector3 targetPosition)
    {
        
        
        float elapsedTime = 0f;
        

        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        // Calculate final rotation offset (increase X angle by 10 degrees)
        Quaternion finalRotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x + 15f,
                                                     mainCamera.transform.eulerAngles.y,
                                                     mainCamera.transform.eulerAngles.z);

        while (elapsedTime < camrotationSpeed)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate position
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / camrotationSpeed);

            // Smoothly interpolate rotation
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, elapsedTime / camrotationSpeed);

            yield return null;
        }

        // Ensure final snap at exact position and rotation
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = finalRotation;
    }
}


