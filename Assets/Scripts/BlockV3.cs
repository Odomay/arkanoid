using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockV3 : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _radius = 5;
    private int _health = 2;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HandleCollision();
        }
    }

    public void HandleCollision()
    {
        _health--;
        if (_health >= 0)
        {
            Vector3 movePosition = _startPosition + (Vector3)Random.insideUnitCircle * _radius;
            transform.position = movePosition;
            if (_health == 0)
            {
                StartCoroutine(BlockDestroy());
            }
        }
    }
    private IEnumerator BlockDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

