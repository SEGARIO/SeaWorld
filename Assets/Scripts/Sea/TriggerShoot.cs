using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerShoot : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float force = 10f;
    public GameObject _viserTarget;
    public float cooldown = 1f;

    private float lastShootTime;

    void Update()
    {
        transform.LookAt(_viserTarget.transform.position);
        if (Gamepad.current != null && Gamepad.current.rightTrigger.isPressed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Vérifie le cooldown
        if (Time.time < lastShootTime + cooldown) return;

        lastShootTime = Time.time;

        // Instancie + force
        var obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(spawnPoint.forward * force, ForceMode.Impulse);
        }
    }
}