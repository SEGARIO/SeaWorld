using UnityEngine;
using UnityEngine.InputSystem;

public class Viser : MonoBehaviour
{
    public Transform center;
    public float maxDistance = 5f;
    public float height = 1.5f;
    public float smoothSpeed = 10f;

    [Header("Scale")]
    public float maxScale = 1f; // taille max quand joystick à fond

    private Vector2 lookInput;
    private Vector3 currentOffset;

    void Update()
    {
        if (center == null) return;

        Vector3 direction = new Vector3(lookInput.x, 0f, lookInput.y);
        float magnitude = Mathf.Clamp01(direction.magnitude);

        Vector3 targetOffset = Vector3.zero;

        if (direction != Vector3.zero)
        {
            targetOffset = direction.normalized * magnitude * maxDistance;
        }

        currentOffset = Vector3.Lerp(currentOffset, targetOffset, smoothSpeed * Time.deltaTime);

        Vector3 targetPosition = center.position + currentOffset + Vector3.up * height;
        transform.position = targetPosition;

        transform.LookAt(center);

        // 🔥 SCALE basé sur la distance
        float distance = currentOffset.magnitude;
        float t = distance / maxDistance; // normalisé entre 0 et 1

        float scaleValue = t * maxScale;

        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}