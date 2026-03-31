using UnityEngine;
using UnityEngine.InputSystem;

public class Viser : MonoBehaviour
{
    public Transform center;        // point autour duquel tourner
    public float maxDistance = 5f;  // distance max
    public float height = 1.5f;     // hauteur optionnelle
    public float smoothSpeed = 10f; // fluidité

    private Vector2 lookInput;
    private Vector3 currentOffset;

    void Update()
    {
        if (center == null) return;

        // Direction du joystick (X = droite/gauche, Y = avant/arrière)
        Vector3 direction = new Vector3(lookInput.x, 0f, lookInput.y);

        // Distance dépend de la force du joystick
        float magnitude = Mathf.Clamp01(direction.magnitude);

        Vector3 targetOffset = direction.normalized * magnitude * maxDistance;

        // Lerp pour un mouvement fluide
        currentOffset = Vector3.Lerp(currentOffset, targetOffset, smoothSpeed * Time.deltaTime);

        // Position finale
        Vector3 targetPosition = center.position + currentOffset + Vector3.up * height;

        transform.position = targetPosition;

        // Optionnel : regarder le centre
        transform.LookAt(center);
    }

    // Input System
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}