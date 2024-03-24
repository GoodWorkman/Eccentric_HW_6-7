using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _body;
    
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _maxSpeed = 8f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _friction = 0.3f;
    
    [SerializeField] private bool _isGrounded;

    private Vector3 _initialScale= Vector3.one;
    private Vector3 _targetScale = new(1f, 0.5f, 1f);

    private float _scaleChangeSpeed = 6f;
    private float _speedMultiple;

    private void Update()
    {
        TryChangeScale();
        TryJump();
    }

    private void FixedUpdate()
    {
        StopPlayer();
        CalculateSpeedMultiplier();
        ApplyForces();
        //ApplyForces2(); - трясется на земле при движении??
    }

    private void ApplyForces()
    {
        float targetXVelocity = Input.GetAxis("Horizontal") * _moveSpeed;
        float xVelocityChange = targetXVelocity - _rigidbody.velocity.x;
        xVelocityChange = Mathf.Clamp(xVelocityChange, -_maxSpeed, _maxSpeed);
        float xForce = xVelocityChange * _speedMultiple;

        _rigidbody.AddForce(xForce, 0f, 0f, ForceMode.VelocityChange);

        if (_isGrounded)
        {
            // Плавное замедление через изменение линейного демпфирования, плавно без тряски
            _rigidbody.drag = _friction;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }
    
    private void ApplyForces2()
    {
       // !!!!все трясется при движении по земле!!!!
        
        float xForce = Input.GetAxis("Horizontal") * _moveSpeed * _speedMultiple;
        _rigidbody.AddForce(xForce, 0f, 0f, ForceMode.VelocityChange);
        
        if (_isGrounded)
        {
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0f, 0f, ForceMode.VelocityChange);
        }
    }

    private void StopPlayer()
    {
        _rigidbody.AddForce(0f,0f,0f, ForceMode.VelocityChange);

        _speedMultiple = 1f;
    }

    private void CalculateSpeedMultiplier()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > _maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            _speedMultiple = 0f;
        }
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(0f, _jumpForce, 0f, ForceMode.VelocityChange);
        }
    }

    private void TryChangeScale()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !_isGrounded)
        {
            _body.localScale = Vector3.Lerp(_body.localScale, _targetScale, Time.deltaTime * _scaleChangeSpeed);
        }
        else
        {
            _body.localScale = Vector3.Lerp(_body.localScale, _initialScale, Time.deltaTime * _scaleChangeSpeed);

        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        Vector3 normal = other.contacts[0].normal;

        float dot = Vector3.Dot(normal, Vector3.up);

        if (dot > 0.3)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _isGrounded = false;
    }
}
