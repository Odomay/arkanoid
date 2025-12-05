using DG.Tweening;
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
    public Button[] ExitMenuButtons;
    public static event Action OnNexLevelButtonPressed;

    private void Awake()
    {
        ExitGame(ExitMenuButtons);
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
        NextLevelButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            NextLevelButton.transform.localScale = Vector3.one;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    private void RestartGame(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => Restart(i));
        }

        void Restart(int index)
        {
            for (index = 0; index < buttons.Length; index++)
            {
                buttons[index].transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    buttons[index].transform.localScale = Vector3.one;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                });
            }
            
        }
    }

    private void ExitGame(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => Exit(i));
        }

        void Exit(int index)
        {
            for (index = 0; index < buttons.Length; index++)
            {
                buttons[index].transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    buttons[index].transform.localScale = Vector3.one;
                    SceneManager.LoadScene(3);
                });
            }          

        }
    }
}
