using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFallingSprite : MonoBehaviour
{
    public float Speed;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rect.anchoredPosition += Vector2.down * Speed * Time.deltaTime;

        if(_rect.anchoredPosition.y < -Screen.height - 200f)
        {
            Destroy(gameObject);
        }
    }
}
