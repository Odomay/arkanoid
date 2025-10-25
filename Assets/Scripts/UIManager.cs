using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager GameManager;
    public TMP_Text AttemptsCountText;
    public TMP_Text BlocksCountText;
    public GameObject WinPanel;

    private void Start()
    {
        AttemptsCountText.text = $"attempts: {GameManager.AttemptsCount:D3}";
        BlocksCountText.text = $"blocks: {GameManager.BlocksCount.Count:D3}";

        GameManager.OnAttemptsCountChanged += UpdateAttemptsCountText;
        GameManager.OnBlocksCountChanged += UpdateBlocksCountText;
        GameManager.OnBlocksCountEnded += ActivateWinPanel;
    }

    private void OnDisable()
    {
        GameManager.OnAttemptsCountChanged -= UpdateAttemptsCountText;
        GameManager.OnBlocksCountChanged -= UpdateBlocksCountText;
        GameManager.OnBlocksCountEnded -= ActivateWinPanel;
    }

    private void UpdateAttemptsCountText()
    {
        AttemptsCountText.text = $"attempts: {GameManager.AttemptsCount:D3}";
    }

    private void UpdateBlocksCountText()
    {
        BlocksCountText.text = $"blocks: {GameManager.BlocksCount.Count:D3}";
    }

    private void ActivateWinPanel()
    {
        WinPanel.SetActive(true);
    }
}
