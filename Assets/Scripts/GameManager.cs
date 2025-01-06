using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject pauseMenu;

    public Action OnRestartGame;
    public Action OnStartGame;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        Time.timeScale = 0f;
    }
    private void Start()
    {
        gameOverMenu.SetActive(false);
        startMenu.SetActive(true);
    }
    
    public void OnStartButtonClicked()
    {
        Time.timeScale = 1f;
        OnStartGame?.Invoke();
        startMenu.SetActive(false);
    }
    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        OnRestartGame?.Invoke();
        gameOverMenu.SetActive(false);
    }

    public void OnPauseButtonClicked()
    {
        if(gameOverMenu.activeSelf == false && startMenu.activeSelf == false)
        {
            Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
            pauseMenu.SetActive(true);
        }
    }

    public void OnResumeGameButtonClicked()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void OnQuitGameButtonClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
