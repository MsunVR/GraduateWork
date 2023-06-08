using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        audioSource1.UnPause(); // Возобновляем звук
        audioSource2.UnPause();
        audioSource3.UnPause();
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
        audioSource1.Pause(); // Останавливаем звук
        audioSource2.Pause();
        audioSource3.Pause();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезапускаем сцену
    }
    public void ExitGame()
    {
        Debug.Log("���� ���������");
        Application.Quit();
    }
}
