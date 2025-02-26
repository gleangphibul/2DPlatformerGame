using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public Text coinText; // UI Text for coin count
    public Slider healthBar; // UI Slider for health bar

    [Header("Player Stats")]
    public int maxHealth = 5; // Max health for the player
    private int currentHealth;
    private int coinCount = 0;

    private void Start()
    {
        currentHealth = maxHealth; // Set initial health
        UpdateUI(); // Update the UI on start
    }

    private void Update()
    {
        // Example to test coin and health updates, you can remove or replace this
        if (Input.GetKeyDown(KeyCode.C)) // Add a coin
        {
            AddCoin();
        }

        if (Input.GetKeyDown(KeyCode.H)) // Take damage
        {
            TakeDamage(1);
        }
    }

    // Method to add a coin
    public void AddCoin()
    {
        coinCount++;
        UpdateUI();
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Ensure health doesn't go below 0
        if (currentHealth < 0) currentHealth = 0;

        // Update the health bar and UI
        UpdateUI();
    }

    // Method to update the UI elements
    private void UpdateUI()
    {
        // Update coin text
        coinText.text = "Coins: " + coinCount;

        // Update health bar slider value
        healthBar.value = currentHealth;

        // Update the health text (optional, if you want it still as a label)
        // healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }
}
