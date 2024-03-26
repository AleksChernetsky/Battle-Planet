using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float _distanceToCheck;

    private float _checkDelay;
    private float gravityForce = -9.8f;
    public Collider[] _colliders;
    public List<Rigidbody> _rigidbodies;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _distanceToCheck);
    }
    private void Update()
    {
        CheckObjects();
    }
    private void FixedUpdate()
    {
        PerformGravity();
    }
    private void CheckObjects()
    {
        _checkDelay += Time.deltaTime;
        if (_checkDelay >= 0.1f)
        {
            _rigidbodies.Clear();
            _colliders = Physics.OverlapSphere(transform.position, _distanceToCheck, layermask);
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i].gameObject.TryGetComponent(out Rigidbody rigidbody))
                {
                    _rigidbodies.Add(rigidbody);
                }
            }
            _checkDelay = 0;
        }
    }
    private void PerformGravity()
    {
        foreach (var objects in _rigidbodies)
        {
            objects.useGravity = false;
            objects.constraints = RigidbodyConstraints.FreezeRotation;

            Vector3 direction = (objects.position - transform.position).normalized;
            Vector3 currentRotation = objects.transform.up;

            objects.rotation = Quaternion.FromToRotation(currentRotation, direction) * objects.rotation;
            objects.AddForce(direction * gravityForce);
        }
    }
}