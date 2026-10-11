
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    public float pushSpeed = 3f;

    private Rigidbody rb;
    private Vector3 pushDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Push(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
            pushDirection = direction.normalized;
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = pushDirection.x * pushSpeed;
        velocity.z = pushDirection.z * pushSpeed;
        rb.linearVelocity = velocity;

        pushDirection = Vector3.zero;
    }
}
