using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DestroyWithDelay : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject _particl;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Instantiate(_particl, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
