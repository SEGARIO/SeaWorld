using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
    }
}