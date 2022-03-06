using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    #endregion

    #region Private Fields
    private Vector3 _velocity;
    private Vector3 _moveDirection;
    private bool _isGrounded = false;
    private bool _isColliding;
    private int _layerMask;
    #endregion

    #region Public Methods

    void Awake()
    {
        _layerMask = 1;
    }
    void FixedUpdate()
    {
        float fixedDelta = Time.fixedDeltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float forward = Input.GetAxisRaw("Vertical");
        //just check x and z values to handle collisions, y is used for groundcheck
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, _layerMask, QueryTriggerInteraction.Ignore);

        if (_isGrounded)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _moveSpeed;
            _moveDirection = (horizontal * transform.right + forward * transform.forward).normalized;
            Vector3 inputVelocity = _moveDirection * speed;
            _velocity = new Vector3(inputVelocity.x, 0, inputVelocity.z);

            if (Input.GetKey(KeyCode.Space) && _isGrounded)
                HandleJump();
        }
        else
        {
            float newY = _velocity.y + Physics.gravity.y * fixedDelta;
            _velocity.y = newY;
        }


        



        Vector3 collisionVelocity = new Vector3(_velocity.x, 0, _velocity.z);
        _isColliding = Physics.Raycast(transform.position, collisionVelocity.normalized, collisionVelocity.magnitude * fixedDelta + 0.5f,
            _layerMask, QueryTriggerInteraction.Ignore);

        if (!_isColliding)
            transform.position += _velocity * fixedDelta;
        else
        {
            _velocity.x = 0;
            _velocity.z = 0;
            transform.position += _velocity * fixedDelta;
        }
    }
    #endregion
    private void HandleJump()
    {
        if (_isGrounded)
            _velocity.y = _jumpSpeed;
    }
}
