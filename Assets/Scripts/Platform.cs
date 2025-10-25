using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float MoveSpeed;

    private Transform _transform;
    private bool _move = true;

    private void Awake()
    {
        //Debug.Log($"Skorost' igroka = {MoveSpeed} km/h, prikooooool"); // интерполяция строк (процесс получения промежуточных значений)
        //Debug.Log("Skorost' igroka = " + MoveSpeed + "km/h, prikooooool"); // конкатенация строк (сложение строк)
        GameManager.OnBlocksCountEnded += StopPlatform;
        _transform = GetComponent<Transform>();       
    }

    private void Update()
    {
        if (_move)
        MouseInput();
        
    }

    private void MouseInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);
        _transform.position = new Vector3(Mathf.Clamp (mousePositionOnScreen.x, -24.6f, 24.6f), _transform.position.y, _transform.position.z);
        
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

    private void StopPlatform()
    {
        _move = false;
        GameManager.OnBlocksCountEnded -= StopPlatform;
    }
}
