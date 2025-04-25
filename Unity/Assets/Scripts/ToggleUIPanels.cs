using UnityEngine;
using UnityEngine.UI;

public class ToggleUIPanels : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panelA;
    [SerializeField] private GameObject panelB;


    public  bool isPanelAToggled = true;

    private void Start()
    {
        TogglePanels();
    }

    public void TogglePanels()
    {
        if (isPanelAToggled)
        {
            // Hide Panel A, show Panel B
            panelA.SetActive(false);
            panelB.SetActive(true);

    
        }
        else
        {
            // Hide Panel B, show Panel A
            panelB.SetActive(false);
            panelA.SetActive(true);

        }

        // Toggle the state
        isPanelAToggled = !isPanelAToggled;
    }
}