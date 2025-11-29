using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public Button PauseGameButton;
    public Button ResumeGameButton;
    public GameObject PausePanel;

    private void Awake()
    {
        PauseGameButton.onClick.AddListener(PauseGame);
        ResumeGameButton.onClick.AddListener(ResumeGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Debug.Log("press");
        PausePanel.SetActive(true);
        PauseGameButton.interactable = false;
        OnGamePaused?.Invoke();
        
    }

    private void ResumeGame()
    {
        OnGameResumed?.Invoke();
        PausePanel.SetActive(false);
        PauseGameButton.interactable = true;
    }
}
