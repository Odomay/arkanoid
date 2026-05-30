using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public static event Action OnButtonPressed;
    public static event Action OnNexLevelButtonPressed;
    public Button NextLevelButton;
    public Button[] RestartButtons;
    public Button[] ExitMenuButtons;

    private SceneHandlerType _type;
    private LevelCompleteHandler _levelCompleteHandler;
    private enum SceneHandlerType
    {
        RestartLevel,
        LoadMenu,
        NextLevel
    }

    private void Awake()
    {
        ExitMenu(ExitMenuButtons);
        RestartGame(RestartButtons);
        NextLevelButton.onClick.AddListener(NextGameLevel);

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            NextLevelButton.gameObject.SetActive(false);
        }
        _levelCompleteHandler = FindFirstObjectByType<LevelCompleteHandler>();

    }

    private void NextGameLevel()
    {
        OnNexLevelButtonPressed?.Invoke();
        _levelCompleteHandler.CompleteLevel();
        NextLevelButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            NextLevelButton.transform.localScale = Vector3.one;
            StartCoroutine(Delay(SceneHandlerType.NextLevel));
        });
    }

    private IEnumerator Delay(SceneHandlerType type)
    {
        yield return new WaitForSeconds(0.4f);
        OnButtonPressed?.Invoke();
        yield return new WaitForSeconds(LoadingMenuPanel.AnimationTime);
        switch (type)
        {
            case SceneHandlerType.LoadMenu: SceneManager.LoadScene(0); break;
            case SceneHandlerType.NextLevel: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); break;
            case SceneHandlerType.RestartLevel: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); break;
        }
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

                    StartCoroutine(Delay(SceneHandlerType.RestartLevel));
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
                    StartCoroutine(Delay(SceneHandlerType.LoadMenu));
                });
            });

        }
    }
}
