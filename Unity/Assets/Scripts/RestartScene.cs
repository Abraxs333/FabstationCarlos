using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartCurrentScene()
    {
        // Get the active scene name and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}