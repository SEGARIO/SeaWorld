using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPowerUps : MonoBehaviour
{
    public bool _isInPowerUpMode;
    public float _timeOfPowerUp;
    float _timer;
    public ParticleSystem _system;
    public ParticleSystem _lightSystem;
    bool _canPlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = _timeOfPowerUp;
        _canPlay = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInPowerUpMode)
        {
            if(_canPlay)
            {
                _system.Play();
                _lightSystem.Play();
                _canPlay = false;
            }
            if (Gamepad.current != null && Gamepad.current.leftTrigger.isPressed)
            {
                Debug.Log("New shoot");
            }
            _timer -= Time.deltaTime;
        }

        if(_timer <= 0)
        {
            _system.Stop();
            _isInPowerUpMode = false;
            _canPlay = true;
            _timer = _timeOfPowerUp;
        }
    }
}
