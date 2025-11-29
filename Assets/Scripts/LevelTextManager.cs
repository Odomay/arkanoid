using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTextManager : MonoBehaviour
{
    public TMP_Text LevelIndexText;

    private int _levelIndex = 1;

    private void Awake()
    {
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        switch (_levelIndex)
        {
            case 1: LevelIndexText.text = $"Level {_levelIndex}"; break;
            case 2: LevelIndexText.text = $"Level {_levelIndex}"; break;
            case 3: LevelIndexText.text = $"Level {_levelIndex}"; break;
            case 4: LevelIndexText.text = $"Level {_levelIndex}"; break;
        }
    }
}
