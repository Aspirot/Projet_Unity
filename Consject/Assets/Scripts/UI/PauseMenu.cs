using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject fillUI;
    public GameObject normalUI;
    public GameObject player;
    public GameObject GameUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (player.activeSelf)
            Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level");
    }

    public void ModifyLevel()
    {
        if (!normalUI.activeSelf)
        {
            if (fillUI.activeSelf)
            {
                fillUI.SetActive(false);
            }
            if(player.activeSelf)
            {
                player.SetActive(false);
            }
            if (GameUi.activeSelf)
            {
                GameUi.SetActive(false);
            }
            var coin = GameObject.FindGameObjectWithTag("Coin");
            if(coin != null)
            {
               Destroy(coin); 
            }
            normalUI.SetActive(true);
            Resume();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
