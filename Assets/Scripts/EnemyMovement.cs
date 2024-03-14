using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private float _chaseDistance = 5;

    private float _checkObstacleDistance = 2;
    private bool _obstacleInFront;

    protected Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }
    private void Movement()
    {
        CheckObstacle();
        var direction = (_target.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, _target.position) > _chaseDistance)
        {
            _rigidbody.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);
        }
        if (_obstacleInFront)
        {
            _rigidbody.MovePosition(transform.position + transform.TransformDirection(Vector3.right) * _speed * 2 * Time.fixedDeltaTime);
            // костыль да, но на навигацию без navmesh нужно потратить немного времени =)
        }
    }
    private void Rotation()
    {
        Vector3 localTarget = transform.InverseTransformPoint(_target.transform.position);
        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * _rotateSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
    private void CheckObstacle()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), _checkObstacleDistance))
        {
            _obstacleInFront = true;
        }
        else
        {
            _obstacleInFront = false;
        }
    }
}
