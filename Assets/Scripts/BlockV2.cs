using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockV2 : MonoBehaviour
{
    [SerializeField] private Sprite[] _blockSprites;
    [SerializeField] private Color[] _blockColor; 

    private int _lives = 2;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeBlockSprite()
    {
        if(_lives == 0)
        {
            StartCoroutine(BlockDestroy());
        }
        _spriteRenderer.sprite = _blockSprites[_lives];
        _spriteRenderer.color = _blockColor[_lives];
        _lives -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ChangeBlockSprite();
        }
    }

    private IEnumerator BlockDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
