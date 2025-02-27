using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Set the next scene name in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure player enters the portal
        {
            Debug.Log("Player entered the portal! Loading next level...");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Load the specified scene
        }
        else
        {
            Debug.LogError("Next scene name is not set in the Inspector!");
        }
    }
}
