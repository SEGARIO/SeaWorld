using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public  float currentSpeed = 500f;
    public  float targetSpeed = 50f;
    public float deceleration = 200f;

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