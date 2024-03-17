using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private float _chaseDistance = 5;
    [SerializeField] private float _checkObstacleDistance = 2;

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
                Rotation(_target.position);
                _rigidbody.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);
            }
        }
    }
    private void Rotation(Vector3 target)
    {
        Vector3 targetRotation = transform.InverseTransformPoint(target);
        float angle = Mathf.Atan2(targetRotation.x, targetRotation.z) * Mathf.Rad2Deg;
        Quaternion localRotation = Quaternion.Euler(new Vector3(0, angle, 0) * _rotateSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
    }
    private void CheckObstacle()
    {
        obstacleFront = CheckDirection(new Vector3(0, 0, 1));
        obstacleFrontLeft = CheckDirection(new Vector3(-1, 0, 1));
        obstacleFrontRight = CheckDirection(new Vector3(1, 0, 1));

        Quaternion localRotation = Quaternion.identity;

        if (obstacleFront)
        {
            localRotation = Quaternion.Euler(new Vector3(0, Random.Range(-45f, 45f), 0));
        }
        else if (obstacleFrontLeft)
        {
            localRotation = Quaternion.Euler(Vector3.up * _rotateSpeed);
        }
        else if (obstacleFrontRight)
        {
            localRotation = Quaternion.Euler(Vector3.down * _rotateSpeed);
        }

        _rigidbody.MoveRotation(_rigidbody.rotation * localRotation);
    }

    private bool CheckDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(direction), _checkObstacleDistance);
    }
}
