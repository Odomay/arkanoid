using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices.WindowsRuntime;

public class LoadingMenuPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _topPart;
    [SerializeField] private RectTransform _bottomPart;
    [SerializeField] private RectTransform _leftPart;
    [SerializeField] private RectTransform _rightPart;
    [SerializeField] private float _animationTime;

    private AnimationType _type;

    private enum AnimationType
    {
        Width,
        Height
    }

    private void SetType()
    {
        AnimationType[] indexes = (AnimationType[])System.Enum.GetValues(typeof(AnimationType));

        int randomIndex = Random.Range(0, indexes.Length);

        switch (randomIndex)
        {
            case 0:
                PlayAnimationWidth();
                break;

            case 1: PlayAnimationHeight();
                break;
        }
    }

    private void Awake()
    {
        ButtonsManager.OnButtonPresed += SetType;
    }

    private void PlayAnimationHeight()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_topPart.DOAnchorPosY(270, _animationTime)).Join(_bottomPart.DOAnchorPosY(-270, _animationTime)).
            OnComplete(() =>
            {
                SceneManager.LoadScene(0);
                ButtonsManager.OnButtonPresed -= SetType;
            });
    }

    private void PlayAnimationWidth()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_leftPart.DOAnchorPosX(-480, _animationTime)).Join(_rightPart.DOAnchorPosX(480, _animationTime)).
            OnComplete(() =>
            {
                SceneManager.LoadScene(0);
                ButtonsManager.OnButtonPresed -= SetType;
            });
    }
}
