using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private BoxCollider2D portalCollider;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
