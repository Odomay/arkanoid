using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteHandler : MonoBehaviour
{
    public void CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= ProgresService.UnlockedLevel) ProgresService.UnlockedLevel = currentLevel + 1;
    }
}
