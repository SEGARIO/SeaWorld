
using UnityEngine;
using System.Collections;

public class MotorcycleCrash : MonoBehaviour
{
    [Header("Settings")]
    public bool _canCrash = true;
    public float _crashDuration = 4f;
    public float _rotationSpeed = 600f;

    [Header("References")]
    public MonoBehaviour _motorcycleController;
    public CharacterController _characterController;

    private bool _isCrashing = false;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(!_canCrash)
        {
            if (hit.gameObject.CompareTag("Car"))
            {
                Destroy(hit.gameObject);
            }
        }
        if (!_canCrash || _isCrashing)
            return;

        if (hit.gameObject.CompareTag("Car"))
        {
            StartCoroutine(CrashCoroutine());
        }
    }

    private IEnumerator CrashCoroutine()
    {
        _isCrashing = true;

        _motorcycleController.enabled = false;

        float elapsed = 0f;

        while (elapsed < _crashDuration)
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _motorcycleController.enabled = true;

        _isCrashing = false;
    }
}
