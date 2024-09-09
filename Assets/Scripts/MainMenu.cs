using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void OpenSettings()
    {
        Debug.Log("Settings");
    }


    public void OpenAchievements()
    {
        Debug.Log("Achievements");
    }


    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
