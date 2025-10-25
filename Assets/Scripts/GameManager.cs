using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static event Action OnAttemptsCountChanged;
    public static event Action OnBlocksCountChanged;
    public static event Action OnBlocksCountEnded;

    public List<GameObject> BlocksCount => _blocksCount;

    public static int AttemptsCount = 5;

    private List<GameObject> _blocksCount = new List<GameObject>();
    private Coroutine CheckBlocks;

    private void Awake()
    {
        AttemptsCount = 5;
        CheckBlocks = StartCoroutine(CheckBlocksCount());
    }

    private IEnumerator CheckBlocksCount()
    {
        while (_blocksCount.Count > 0)
        {
            Debug.Log(_blocksCount.Count);
            for (int i = 0; i < _blocksCount.Count; i++)
            {
                if (_blocksCount[i] == null)
                {
                    _blocksCount.RemoveAt(i);
                    OnBlocksCountChanged?.Invoke();
                }

            }
            yield return null;
        }

        OnBlocksCountEnded?.Invoke();
        //CheckBlocks = null;
        //StopCoroutine(CheckBlocks);
    }

    private void OnEnable()
    {
        Ball.OnBallTouchedDestroyZone += RemoveAttempt;
    }

    private void OnDisable()
    {
        Ball.OnBallTouchedDestroyZone -= RemoveAttempt;
    }

    private void RemoveAttempt()
    {
        AttemptsCount--;
        OnAttemptsCountChanged?.Invoke();        
    }

    public void SetBlocksCount(List<GameObject> blocksCount)
    {
        _blocksCount = blocksCount;
    }
}
