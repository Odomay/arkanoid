using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockV1 : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeBlockColor()
    {
        _spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        StartCoroutine(BlockDestroy());
    }

    private IEnumerator BlockDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ChangeBlockColor();
        }
    }
}
