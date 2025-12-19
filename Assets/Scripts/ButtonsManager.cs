using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public static event Action OnButtonPresed;
    public Button NextLevelButton;
    public Button[] RestartButtons;
    public Button[] ExitMenuButtons;
    public static event Action OnNexLevelButtonPressed;

    private void Awake()
    {
        ExitMenu(ExitMenuButtons);
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
            int index = i;

            buttons[index].onClick.AddListener(() =>
            {
                buttons[index].transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    buttons[index].transform.localScale = Vector3.one;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                });
            });
        }
    }

    private void ExitMenu(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            buttons[index].onClick.AddListener(() =>
            {
                buttons[index].transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    buttons[index].transform.localScale = Vector3.one;
                    OnButtonPresed?.Invoke();
                });
            });

        }
    }
}
