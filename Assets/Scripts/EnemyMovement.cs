using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private float _chaseDistance = 5;
    [SerializeField] private float _checkObstacleDistance = 1;

    private Rigidbody _rigidbody;
    private bool obstacleFront, obstacleFrontLeft, obstacleFrontRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        CheckObstacle();

        Vector3 direction = (_target.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, _target.position) > _chaseDistance)
        {
            if (obstacleFrontLeft || obstacleFrontRight)
            {
                _rigidbody.MovePosition(transform.position + transform.TransformDirection(Vector3.forward) * (_speed * 2) * Time.fixedDeltaTime);
            }
            else
            {
                _rigidbody.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);
                Rotation();
            }
        }
    }
    private void Rotation()
    {
        Vector3 targetRotation = transform.InverseTransformPoint(_target.transform.position);
        float angle = Mathf.Atan2(targetRotation.x, targetRotation.z) * Mathf.Rad2Deg;
        Quaternion localRotation = Quaternion.Euler(new Vector3(0, angle, 0) * _rotateSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
    }
    private void CheckObstacle()
    {
        obstacleFront = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), _checkObstacleDistance / 2);
        obstacleFrontLeft = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 1)), _checkObstacleDistance);
        obstacleFrontRight = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 1)), _checkObstacleDistance);

        if (obstacleFrontLeft)
        {
            Quaternion localRotation = Quaternion.Euler(Vector3.up * _rotateSpeed);
            _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
        }
        else if (obstacleFrontRight)
        {
            Quaternion localRotation = Quaternion.Euler(Vector3.down * _rotateSpeed);
            _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
        }
        else if (obstacleFront)
        {
            Quaternion localRotation = Quaternion.Euler(new Vector3(0, Random.Range(-45f, 45f), 0));
            _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
        }
    }
}
