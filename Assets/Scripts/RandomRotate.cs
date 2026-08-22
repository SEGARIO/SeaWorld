using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    [SerializeField] private float minSpeed = 20f;
    [SerializeField] private float maxSpeed = 100f;

    private Vector3 rotationSpeed;

    private void Start()
    {
        rotationSpeed = new Vector3(
            Random.Range(minSpeed, maxSpeed),
            Random.Range(minSpeed, maxSpeed),
            Random.Range(minSpeed, maxSpeed)
        );

        // Permet d'avoir des directions aléatoires
        rotationSpeed.x *= Random.value > 0.5f ? 1 : -1;
        rotationSpeed.y *= Random.value > 0.5f ? 1 : -1;
        rotationSpeed.z *= Random.value > 0.5f ? 1 : -1;
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}