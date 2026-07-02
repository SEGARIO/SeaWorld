using UnityEngine;
using UnityEngine.InputSystem;

public class Powerup : MonoBehaviour
{
    float _timer;
    public float _timeToActivate;
    public bool _isActivated;
    public ParticleSystem _light;
    public ParticleSystem _aura;
    bool _canPlay;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _range = 2f;

    private bool _playerInside;
    float timerChangePlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _timeToActivate && _canPlay)
        {
            _isActivated = true;
            _light.Play();
            _aura.Play();
            _canPlay = false;
        }

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= _range)
        {
            if (!_playerInside)
            {
                _playerInside = true;
                PlayerIn();
            }
        }
        else
        {
            _playerInside = false;
        }
        if (Gamepad.current.buttonSouth.isPressed)
        {
            timerChangePlayer -= Time.deltaTime;
        }
        else
        {
            timerChangePlayer = 1.5f;
        }

        if (_playerInside && _isActivated)
        {
          


          if(timerChangePlayer <= 0)
          {

                ActivatePowerUp();
                timerChangePlayer = 1.5f;

          }
        }
    }

    private void PlayerIn()
    {
        Debug.Log("Le joueur est entré dans la zone !");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    void ActivatePowerUp()
    {
        _canPlay = true;
        _timer = 0;
        _isActivated = false;
        _light.Stop();
        _aura.Stop();
        FindObjectOfType<PlayerPowerUps>()._isInPowerUpMode = true;
        Debug.Log("Power uppppppp!!!!");
    }
}
