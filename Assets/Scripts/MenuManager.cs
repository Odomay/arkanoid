using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public Button PlayButton;
    public Button OpenLevelsPanelButton;
    public Button ExitButton;
    public Button CloseLevelsPanelButton;
    public Button ClearProgressButton;
    public CanvasGroup LevelsPanel;

    private void Awake()
    {
        OpenLevelsPanelButton.onClick.AddListener(OpenLevelsPanel);
        CloseLevelsPanelButton.onClick.AddListener(CloseLevelsPanel);
        ExitButton.onClick.AddListener(ExitGame);
        ClearProgressButton.onClick.AddListener(() => ProgresService.ClearProgress());
    }

    private void OpenLevelsPanel()
    {
        LevelsPanel.gameObject.SetActive(true);
        LevelsPanel.alpha = 0;
        OpenLevelsPanelButton.gameObject.transform.DOScale(0.8f, 0.3f).OnComplete(() =>
        {
            OpenLevelsPanelButton.gameObject.transform.DOScale(1f, 0.3f).OnComplete(() =>
            {
                LevelsPanel.DOFade(1f, 1f).OnComplete(() => LevelsPanel.gameObject.transform.DOKill());
            });
        });
    }

    private void CloseLevelsPanel()
    {
        CloseLevelsPanelButton.transform.DOScale(0.8f, 0.3f).OnComplete(() =>
        {
            CloseLevelsPanelButton.transform.DOScale(1f, 0.3f).OnComplete(() =>
            {
                LevelsPanel.DOFade(0f, 1f).OnComplete(() =>
                {
                    LevelsPanel.gameObject.SetActive(false);
                    LevelsPanel.gameObject.transform.DOKill();
                });
            });
        });
    }

    private void ExitGame()
    {
        ExitButton.transform.DOScale(0.8f, 0.3f).OnComplete(() =>
        {
            ExitButton.transform.DOScale(1f, 0.3f).OnComplete(() =>
            {
                Application.Quit();
            });
        });
    }
}
