using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Button NextLevelButton;
    public Button[] RestartButtons;
    public Button[] ExitButtons;
    public static event Action OnNexLevelButtonPressed;

    private void Awake()
    {
        ExitGame(ExitButtons);
        RestartGame(RestartButtons);
        NextLevelButton.onClick.AddListener(NextGameLevel);

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            NextLevelButton.gameObject.SetActive(false);
        }
    }

    private void NextGameLevel()
    {
        OnNexLevelButtonPressed?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartGame(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(Restart);
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ExitGame(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i ++)
        {
            buttons[i].onClick.AddListener(Exit);
        }

        void Exit()
        {
            Application.Quit();
        }
    }
}
