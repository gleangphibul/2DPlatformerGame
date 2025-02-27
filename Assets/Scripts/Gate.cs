using UnityEngine;

public class Gate : MonoBehaviour
{
    private bool playerIsNear = false; // Track if the player is close

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            playerIsNear = true;
            Debug.Log("Player is near the gate.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            Debug.Log("Player left the gate.");
        }
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.Space)) // Open only if near and space is pressed
        {
            OpenGate();
        }
    }

    public void OpenGate()
    {
        Debug.Log("Gate opened!");
        gameObject.SetActive(false); // Disable the gate
    }
}
