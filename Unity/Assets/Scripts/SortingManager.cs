using UnityEngine;

public class SortingManager : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();

        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = 9999; // High value ensures it renders on top
        }
        else
        {
            Debug.LogWarning("Canvas component not found on this GameObject!");
        }
    }
}