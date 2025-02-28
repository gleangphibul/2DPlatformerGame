using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI elements

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject winText; // Assign in Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal!");

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                LoadNextLevel();
            }
            else
            {
                ShowWinMessage();
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void ShowWinMessage()
    {
        if (winText != null)
        {
            winText.SetActive(true); // Show "You Won!" text
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogError("Win text object is not set in the Inspector!");
        }
    }
}
