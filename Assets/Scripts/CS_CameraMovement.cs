using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CS_CameraMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    Transform _transform;
    [SerializeField] AnimationCurve speedCameraByZoom;
    [SerializeField] float accelarationMultiplicateur = 2.5f;
    [SerializeField] float zoomSpeed = 0.01f;
    bool isMoveAccelerate = false;
    Vector2 currentMousePos;
    Vector2 direction;
    bool moveWithKey = false;
    float minHeightCamera = 10;
    float maxHeightCamera = 100;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        currentMousePos = Input.mousePosition;

        if (!moveWithKey)
        {
            //■■■■■■■■■ X ■■■■■■■■■■
            if (currentMousePos.x > Screen.width)
            {
                direction.x = 1;
            }
            else if (currentMousePos.x < 0)
            {
                direction.x = -1;
            }
            else
            {
                direction.x = 0;
            }

            //■■■■■■■■■ Y ■■■■■■■■■■
            if (currentMousePos.y > Screen.height)
            {
                direction.y = 1;
            }
            else if (currentMousePos.y < 0)
            {
                direction.y = -1;
            }
            else
            {
                direction.y = 0;
            }
        }

        Move(direction);
    }

    public void ZQSD_Input(InputAction.CallbackContext ct)
    {
        if (ct.performed == true)
        {
            moveWithKey = true;
            Vector2 inputAxis = ct.ReadValue<Vector2>();
            direction = inputAxis;
        }
        else if (ct.canceled)
        {
            moveWithKey = false;
        }
    }

    public void Scroll_Input(InputAction.CallbackContext ct)
    {
        float inputScroll = ct.ReadValue<Vector2>().y;
        if (inputScroll == 0)
        {
            return;
        }
        else
        {
            _transform.position = new Vector3(_transform.position.x, Mathf.Clamp(_transform.position.y - inputScroll * zoomSpeed, minHeightCamera, maxHeightCamera), _transform.position.z);
        }
    }

    public void Accelerate_Input(InputAction.CallbackContext ct)
    {
        if (ct.started)
        {
            isMoveAccelerate = true;
        }
        else if (ct.canceled)
        {
            isMoveAccelerate = false;
        }
    }

    private void Move(Vector2 direction)
    {
        Debug.DrawRay(new Vector3(32, 0, 25), (direction * 2), Color.red);
        direction = direction.normalized * Time.deltaTime * speedCameraByZoom.Evaluate(map(_transform.position.y, minHeightCamera, maxHeightCamera, 0, 1)) * (isMoveAccelerate ? accelarationMultiplicateur : 1);
        _rigidbody.velocity = (new Vector3(direction.x, 0, direction.y));
    }

    private float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
