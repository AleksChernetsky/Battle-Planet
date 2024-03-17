using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private JoysticksHandler _joystickHandler;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotateSpeed = 2;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MoveCharacter(_joystickHandler.MoveDirection);
        RotateCharacter(_joystickHandler.RotateDirection);
    }
    private void MoveCharacter(Vector2 direction)
    {
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.y).normalized;
        _rigidbody.MovePosition(_rigidbody.position + (transform.TransformDirection(targetDirection) * _speed * Time.fixedDeltaTime));
    }
    private void RotateCharacter(Vector2 direction)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, direction.x) * _rotateSpeed);
        _rigidbody.MoveRotation(_rigidbody.rotation * rotation);
    }
}
