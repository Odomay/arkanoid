using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanAnimation : MonoBehaviour
{
    public RectTransform Pan;
    public RectTransform Egg;

    private float _panAngle = 15;
    private float _jumpHeight = 500;
    private float _duration = 0.7f;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(Pan.DORotate(new Vector3(0, 0, _panAngle), _duration));

        seq.Join(Egg.DOAnchorPosY(Egg.anchoredPosition.y + _jumpHeight, _duration));

        seq.Join(Egg.DORotate(new Vector3(0, 0, 360), _duration, RotateMode.FastBeyond360));

        seq.Append(Pan.DORotate(new Vector3(0, 0, -_panAngle), _duration));

        seq.Join(Egg.DOAnchorPosY(Egg.anchoredPosition.y - 150, _duration));

        seq.Join(Egg.DORotate(new Vector3(0, 0, 360), _duration, RotateMode.FastBeyond360));

        seq.SetLoops(-1);
    }
}
