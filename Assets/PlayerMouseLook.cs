using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private float _rotationSpeed = 100f;
    #endregion

    #region Private Fields
    private Transform _cameraTransform;
    private Transform _playerTransform;
    private bool _isLocked;
    #endregion

    #region Public Methods
    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _playerTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
        _isLocked = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isLocked = !_isLocked;
            if (_isLocked == false)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (_isLocked)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector2 mouseInput = new Vector2(x, y);
            if (mouseInput == Vector2.zero)
                return;
            _playerTransform.rotation *= Quaternion.AngleAxis(mouseInput.x * _rotationSpeed * Time.deltaTime, Vector3.up);
            Vector3 localEuler = _cameraTransform.localRotation.eulerAngles;

            float xRot = localEuler.x;
            if (xRot > 180)
                xRot -= 360;

            if ((xRot < 85f && mouseInput.y < 0) || (xRot > -85f && mouseInput.y > 0))
            {
                _cameraTransform.localRotation *= Quaternion.AngleAxis(-mouseInput.y * _rotationSpeed * Time.deltaTime, Vector3.right);
            }
        }

    }
    #endregion
}
