using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverMenuUi;
    public PlayerController playerController;

    private bool gameOver;
    private void Update()
    {
        if (playerController.currentHealth <= 0 && !gameOver)
        {
            GameOver();
            gameOver = true;
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverMenuUi.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
