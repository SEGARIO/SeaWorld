using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
