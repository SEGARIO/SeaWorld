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
    public bool _isCurrentPlayer;
    private float lastShootTime;
    public Transform _reference;

    void OnEnable()
    {
        Debug.Log("TriggerShoot prefab au Start = " + prefab);
      //  prefab = GameObject.Find("Shoot");
    }

    void Update()
    {
        if(_isCurrentPlayer)
        {
            transform.LookAt(_viserTarget.transform.position);
            if (Gamepad.current != null && Gamepad.current.rightTrigger.ReadValue() > 0.1f)
            {
                Shoot(cooldown);
                Debug.Log("PRESSES");
            }
            if (Gamepad.current != null && Gamepad.current.leftTrigger.ReadValue() > 0.1f)
            {
                Shoot(cooldown);
                Debug.Log("PRESSES");
            }

            if (Gamepad.current != null && Gamepad.current.leftShoulder.isPressed)
            {
                Shoot(cooldown);
                Debug.Log("PRESSES");
            }

            if (Gamepad.current != null && Gamepad.current.rightShoulder.isPressed)
            {
                Shoot(cooldown);
                Debug.Log("PRESSES");
            }
        }
        this.transform.position = _reference.position;
    }

    public void Shoot(float _cooldown)
    {
        if (prefab == null)
        {
            Debug.LogError("MON PREFAB EST NULL !");
            return;
        }
        Debug.Log("Shoot appelé, prefab = " + prefab);
        // Vérifie le cooldown
        if (Time.time < lastShootTime + _cooldown) return;

        lastShootTime = Time.time;

        // Instancie + force
        var obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(spawnPoint.forward * force, ForceMode.Impulse);
        }
    }
}