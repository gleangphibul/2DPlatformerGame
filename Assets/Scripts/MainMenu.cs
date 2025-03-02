using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instruction;
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Instruction() {
        if (instruction.activeSelf) {
            instruction.SetActive(false);
        } else {
            instruction.SetActive(true);
        }
    }
}
