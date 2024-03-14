using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField] private float gravityForce = -9.8f;

    void FixedUpdate()
    {
        Rigidbody[] rigidBodies = FindObjectsOfType<Rigidbody>();
        foreach (var objects in rigidBodies)
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
