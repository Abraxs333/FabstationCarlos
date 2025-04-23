using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
    public Image reticleImage; 
    public Color defaultColor = Color.red; 
    public Color intersectColor = Color.green; 

    void Start()
    {
        // Hide reticle initially
        reticleImage.gameObject.SetActive(false);
        reticleImage.color = defaultColor;
    }

    void Update()
    {
        // Show reticle and check intersection when pressing
        if (Input.GetMouseButton(0))
        {
            ShowReticle(Input.mousePosition);
            CheckIntersection();
        }
        // Hide reticle on release
        if (Input.GetMouseButtonUp(0))
        {
            HideReticle();
        }
    }

    void ShowReticle(Vector2 pointerPosition)
    {
        reticleImage.rectTransform.position = pointerPosition;
        reticleImage.gameObject.SetActive(true);
    }

    void HideReticle()
    {
        reticleImage.gameObject.SetActive(false);
        reticleImage.color = defaultColor; // Reset color
    }

    void CheckIntersection()
    {
        // Create ray from camera to pointer position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform raycast
        if (Physics.Raycast(ray, out hit))
        {
            reticleImage.color = intersectColor; // Change color if hit occurs
        }
        else
        {
            reticleImage.color = defaultColor; // Reset if no intersection
        }
    }
}