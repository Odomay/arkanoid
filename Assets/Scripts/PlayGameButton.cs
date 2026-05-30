using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        int levelToLoad = ProgresService.UnlockedLevel;
        SceneManager.LoadScene(levelToLoad);
    }
}
