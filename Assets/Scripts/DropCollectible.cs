using UnityEngine;

public class DropCollectible : MonoBehaviour
{
    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 15f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        AddRandomImpulse();
    }

    public void AddRandomImpulse()
    {
        Vector3 randomDirection = Random.onUnitSphere; // Direction aléatoire
        float randomForce = Random.Range(_minForce, _maxForce);

        _rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);
    }

    // Test avec la touche E
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddRandomImpulse();
        }
    }
}
