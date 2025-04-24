using UnityEngine;
using TMPro;

public class AngleLabelManager : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshPro textLabelPrefab; // Assign a TextMeshPro prefab in the Inspector
    public Camera mainCamera; // Assign the main camera in the Inspector

    [Header("Label Offset")]
    public Vector3 labelOffset = new Vector3(-0.2f, 0.2f, -0.2f); // Default offset (modifiable in Inspector)

    private TextMeshPro angleLabelInstance;

    /// <summary>
    /// Creates or updates a floating angle label at the given position.
    /// </summary>
    public void UpdateAngleLabel(Vector3 pivotPoint, float angle)
    {
        if (angleLabelInstance == null)
        {
            angleLabelInstance = Instantiate(textLabelPrefab, pivotPoint, Quaternion.identity);
        }

        // Update label text
        angleLabelInstance.text = $"{angle:F1}°"; // Format to 1 decimal place

        // Adjust label position with the serialized offset
        angleLabelInstance.transform.position = pivotPoint + labelOffset;

        // Make the label face the main camera correctly
        angleLabelInstance.transform.LookAt(mainCamera.transform);
        angleLabelInstance.transform.Rotate(0, 180f, 0); // Rotate 180 degrees for correct readability
    }
}