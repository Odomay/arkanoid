using System.Collections;
using UnityEngine;

public class BlockV4 : MonoBehaviour
{
    public int Speed;

    private Vector3 _targetScale = Vector3.one * 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BlockReducing();
        }
    }

    public void BlockReducing()
    {
        StartCoroutine(Reduce());
    }

    private IEnumerator Reduce()
    {
        while (transform.localScale != _targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
