using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private SimpleTouchController _joystick;
    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;
    private float _mobileSpeed;

    //public bool IsMoving => _rigidbody.velocity != Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mobileSpeed = 0.8f * _speed;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //_horizontalSpeed = Input.GetAxisRaw("Horizontal") * _speed;
        //_verticalSpeed = Input.GetAxisRaw("Vertical") * _speed;
        MobileInput();
        if (_joystick.IsPressed == false)
        {
            DesktopMove();
            DesktopRotation();
        }
        _rigidbody.velocity = Vector3.zero;
    }

    private void MobileInput()
    {
        if (_joystick.IsPressed == false)
        {
            _animator.SetBool("IsWalking", false);
            return;
        }

        _animator.SetBool("IsWalking", true);
        _rigidbody.MovePosition(_rigidbody.position + (Vector3.forward * _joystick.GetTouchPosition.y * Time.fixedDeltaTime * _mobileSpeed) +
            (Vector3.right * _joystick.GetTouchPosition.x * Time.fixedDeltaTime * _mobileSpeed));

        MobileRotation();
    }

    private void MobileRotation()
    {
        Vector3 joysticRotation = new Vector3(_joystick.GetTouchPosition.x, 0f, _joystick.GetTouchPosition.y);

        if (joysticRotation != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(joysticRotation);
            _rigidbody.MoveRotation(Quaternion.SlerpUnclamped(_rigidbody.rotation, rotation, 0.5f));
        }
    }

    private void DesktopMove()
    {
        if (Input.GetKey(KeyCode.W) == true)
        {
            _rigidbody.MovePosition(transform.position + (transform.forward * Time.fixedDeltaTime * _speed));
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
    }

    private void DesktopRotation()
    {
        if (Input.GetKey(KeyCode.A) == true)
        {
            float rotationAngle = _rotationSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, -rotationAngle, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * rotation);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            float rotationAngle = _rotationSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * rotation);
        }
    }
}
