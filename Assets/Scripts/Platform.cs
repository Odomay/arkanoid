using UnityEngine;

public class Platform : MonoBehaviour
{
    public float MoveSpeed;

    private Transform _transform;
    private bool _move = true;
    private FixedJoystick _joystick;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _joystick = FindFirstObjectByType<FixedJoystick>();

        GameManager.OnBlocksCountEnded += StopPlatform;        
        GameManager.OnAttemptsCountEnded += StopPlatform;

        PauseManager.OnGamePaused += PausePlatform;
        PauseManager.OnGameResumed += ResumePlatform;
    }

    private void Update()
    {
        if (_move)
        MouseInput();
    }

    private void MouseInput()
    {
        // Vector3 mousePosition = Input.mousePosition;
        // Vector3 mousePositionOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);
        // _transform.position = new Vector3(Mathf.Clamp (mousePositionOnScreen.x, -24.6f, 24.6f), _transform.position.y, _transform.position.z);
        float horizontal = _joystick.Horizontal;
        transform.position += Vector3.right * horizontal * MoveSpeed * Time.deltaTime;
    }

    private void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
        }
    }

    private void ResumePlatform()
    {
        _move = true;
    }

    private void StopPlatform()
    {
        _move = false;
        GameManager.OnBlocksCountEnded -= StopPlatform;
        GameManager.OnAttemptsCountEnded -= StopPlatform;
    }

    private void PausePlatform()
    {
        _move = false;
    }

    private void OnDestroy()
    {
        PauseManager.OnGamePaused -= PausePlatform;
    }

    private void ResumeGame()
    {
        _move = true;
    }
}
