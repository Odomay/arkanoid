using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainUISpawner : MonoBehaviour
{
    public GameObject[] UIPrefbs;
    public RectTransform Parent;

    public float SpawnDelay;
    public float MinSpeed;
    public float MaxSpeed;

    private float _canvasWidth;
    private float _canvasHeight;

    private void Start()
    {
        _canvasHeight = Parent.rect.height;
        _canvasWidth = Parent.rect.width;
        InvokeRepeating(nameof(Spawn), 0f, SpawnDelay);
    }

    private void Spawn()
    {
        GameObject obj = Instantiate(UIPrefbs[Random.Range(0, UIPrefbs.Length)], Parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        float randomX = Random.Range(-_canvasWidth / 2, _canvasWidth / 2);
        float spawnY = _canvasHeight / 2 + 100;
        rect.anchoredPosition = new Vector3(randomX, spawnY);
        rect.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        float speed = Random.Range(MinSpeed, MaxSpeed);
        obj.GetComponent<UIFallingSprite>().Speed = speed;
    }
}
