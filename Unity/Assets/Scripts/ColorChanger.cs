using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;

    private Image imageComponent;

    void Awake()
    {
        // Get the Image component on the same GameObject
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("ColorChanger: No Image component found on this GameObject!");
        }
    }

    /// <summary>
    /// Changes the Image color to the selected color.
    /// </summary>
    public void SetSelectedColor()
    {
        if (imageComponent != null)
            imageComponent.color = selectedColor;
    }

    /// <summary>
    /// Resets the Image color to the default color.
    /// </summary>
    public void SetDefaultColor()
    {
        if (imageComponent != null)
            imageComponent.color = defaultColor;
    }
}