using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class LoadingMenuPanel : MonoBehaviour
{
    public static float AnimationTime;

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
        switch (_type)
        {
            case AnimationType.Width: PlayAnimationWidth(); break;

            case AnimationType.Height: PlayAnimationHeight(); break;
        }
    }

    private void ChooseType()
    {
        AnimationType[] indexes = (AnimationType[])System.Enum.GetValues(typeof(AnimationType));

        _type = indexes[Random.Range(0, indexes.Length)];

        SetType();
    }

    private void Awake()
    {
        ButtonsManager.OnButtonPressed += ChooseType;
        AnimationTime = _animationTime;
    }

    private void PlayAnimationHeight()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_topPart.DOAnchorPosY(270, _animationTime)).Join(_bottomPart.DOAnchorPosY(-270, _animationTime)).
            OnComplete(() =>
            {
                ButtonsManager.OnButtonPressed -= ChooseType;
            });
    }

    private void PlayAnimationWidth()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_leftPart.DOAnchorPosX(-480, _animationTime)).Join(_rightPart.DOAnchorPosX(480, _animationTime)).
            OnComplete(() =>
            {
                ButtonsManager.OnButtonPressed -= ChooseType;
            });
    }
}
