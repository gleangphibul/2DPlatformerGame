using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI elements

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject winText; // Assign in Inspector

    private AudioSource portalAudioSource;
    private SpriteRenderer spriteRenderer;

    [Header("Animation")]
    public Sprite[] sprites;
    public float animationSpeed = 0.1f;
    private int currentFrame;
    private float timer;

    private void Start()
    {
        portalAudioSource = GetComponent<AudioSource>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        winText.SetActive(false);
    }

    private void Update()
    {
        AnimateSprite(); // FIXED: Call AnimateSprite() in Update() to loop animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalAudioSource.Play();
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

    private void AnimateSprite()
    {
        if (sprites.Length == 0) return; // Prevent errors if no sprites are assigned

        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % sprites.Length; // Loops through sprites
            spriteRenderer.sprite = sprites[currentFrame];
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
            if (winText.activeSelf)
        {
            Debug.Log("WinText is now active.");
        }
        else
        {
            Debug.Log("Failed to activate WinText.");
        }
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogError("Win text object is not set in the Inspector!");
        }
    }
}
