using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    private int currentHearts;

    public GameObject gameOverImage;  // Game Over refference

    void Start()
    {
        currentHearts = hearts.Length; // Player HP from hearts lenght
        UpdateHeartsUI();
        gameOverImage.SetActive(false);  // Hide game over
    }

    public void LoseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            hearts[currentHearts].enabled = false;  // Hide heart
            if (currentHearts <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverImage.SetActive(true);  // show Game Over
        Time.timeScale = 0f;
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));  // waiting time on death
    }

    IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHearts;
        }
    }
}
