using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        PauseGameButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => PauseGameButton.transform.localScale = Vector3.one);
        PausePanel.SetActive(true);
        PauseGameButton.interactable = false;
        OnGamePaused?.Invoke();
        
    }

    private void ResumeGame()
    {
        ResumeGameButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ResumeGameButton.transform.localScale = Vector3.one;
            OnGameResumed?.Invoke();
            PausePanel.SetActive(false);
            PauseGameButton.interactable = true;
        });
    }
}
