using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 500f;
    [SerializeField] private float targetSpeed = 50f;
    [SerializeField] private float deceleration = 200f;

    void Update()
    {
        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            deceleration * Time.deltaTime
        );

        transform.Rotate(Vector3.up * currentSpeed * Time.deltaTime);
    }
}