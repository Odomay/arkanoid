using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private Sprite _unlockedSprite;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.interactable = _levelIndex <= ProgresService.UnlockedLevel;

        if (_button.interactable)
        {
            var imageComponent = GetComponent<Image>();
            imageComponent.sprite = _unlockedSprite;
        }

        _button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(_levelIndex);
    }
}
