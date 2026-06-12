using UnityEngine;

public class RopLook : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    void Update()
    {
        Vector3 direction = pointB.position - pointA.position;
        float distance = direction.magnitude;

        // Milieu entre les deux points
        transform.position = (pointA.position + pointB.position) / 2f;

        // Oriente le cube
        transform.rotation = Quaternion.LookRotation(direction);

        // Étire le cube sur Z
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            distance
        );
    }
}