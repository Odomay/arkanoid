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

    private RectTransform _panelTransform;
    private CanvasGroup _panelAlpha;

    private void Awake()
    { 
        PauseGameButton.onClick.AddListener(PauseGame);
        ResumeGameButton.onClick.AddListener(ResumeGame);

        _panelTransform = PausePanel.GetComponent<RectTransform>();
        _panelAlpha = PausePanel.GetComponent<CanvasGroup>();
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
        PausePanel.SetActive(true);
        Vector3 startPosition = new Vector3(858, 435);
        Vector3 destination = Vector3.zero;
        _panelAlpha.alpha = 0;
        PausePanel.transform.localScale = Vector3.zero;
        _panelTransform.anchoredPosition = startPosition;

        PauseGameButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        PauseGameButton.transform.localScale = Vector3.one);

        Sequence seq = DOTween.Sequence();
        seq.Join(_panelAlpha.DOFade(1, 2f)).Join(PausePanel.transform.DOScale(1, 2f)).Join(_panelTransform.DOAnchorPos(destination, 2f));

        PauseGameButton.interactable = false;
        OnGamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        ResumeGameButton.transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ResumeGameButton.transform.localScale = Vector3.one;

            Vector3 startPosition = new Vector3(858, 435);

            Sequence seq = DOTween.Sequence();
            seq.Join(_panelAlpha.DOFade(0, 2f)).Join(PausePanel.transform.DOScale(0, 2f)).
            Join(_panelTransform.DOAnchorPos(startPosition, 2f)).OnComplete(() =>
            {
                OnGameResumed?.Invoke();
                PausePanel.SetActive(false);
                PauseGameButton.interactable = true;
            });
        });
    }
}
