using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public static event Action OnBallTouchedDestroyZone;

    public float JumpForce;
    public Transform Platform;

    private Rigidbody2D _rigidbody;
    private Vector3 _reflectedDirection;
    private Vector3 _ballPosition;
    private Vector3 _platformPosition;
    private bool _ballOnPlatform;
    private int _downWallLayerIndex = 6;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ballPosition = transform.position;
        _platformPosition = Platform.position;

        GameManager.OnBlocksCountEnded += StopBall;
    }

    private void Start()
    {
        _ballOnPlatform = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hit = collision.contacts[0].normal;
        _rigidbody.velocity = Vector3.Reflect(_reflectedDirection, hit).normalized * JumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _downWallLayerIndex)
        {
            ReturnPosition();
        }
    }

    private void FixedUpdate()
    {
        _reflectedDirection = _rigidbody.velocity;
    }

    private void Update()
    {
        if (_ballOnPlatform)
        {
            transform.position = new Vector3(Platform.position.x, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _ballOnPlatform )
        {
            //_rigidbody.velocity = (Vector2.up + new Vector2(Random.Range(-1f, 1f), 0)) * JumpForce;
            _rigidbody.velocity = Vector2.up * JumpForce ;
            _ballOnPlatform = false;
        }
    }

    private void ReturnPosition()
    {
        if (GameManager.AttemptsCount > 1)
        {
            OnBallTouchedDestroyZone?.Invoke();
            transform.position = _ballPosition;
            Platform.position = _platformPosition;
            _rigidbody.velocity = Vector2.zero;
            _ballOnPlatform = true;
        }
        else
        {
            OnBallTouchedDestroyZone?.Invoke();
            Destroy(gameObject);
        }
    }

    private void StopBall()
    {
        if (_rigidbody == null) return;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
        _ballOnPlatform = false;
        GameManager.OnBlocksCountEnded -= StopBall;
    }

}
