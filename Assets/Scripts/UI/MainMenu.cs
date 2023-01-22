using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
