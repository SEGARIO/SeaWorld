using TMPro;
using UnityEngine;

public class WaterRiser : MonoBehaviour
{
    public float _newWaterLevel;
    public GameObject _waterLevel;
    public int _life;
    [SerializeField] private float currentSpeed = 500f;
    [SerializeField] private float targetSpeed = 50f;
    [SerializeField] private float deceleration = 200f;
    bool _activated;
    public GameObject _white;
    Renderer _rend;
    public Material _mat;
    bool _canRiseWater = true;
    public ParticleSystem _system;
    bool _canPLaySystem = true;
    private void Start()
    {
        _rend = _white.GetComponent<Renderer>();
    }
    void Update()
    {
        if(_activated)
        {
            currentSpeed = Mathf.MoveTowards(
           currentSpeed,
           targetSpeed,
           deceleration * Time.deltaTime
       );

            transform.Rotate(Vector3.up * currentSpeed * Time.deltaTime);

            if (_canRiseWater)
            {
                
                _waterLevel.transform.position = Vector3.Lerp(_waterLevel.transform.position, new Vector3(_waterLevel.transform.position.x, _newWaterLevel, _waterLevel.transform.position.z), Time.deltaTime * 2);
                Invoke("StopRise", 2);
            }
        }
        else
        {
           
            transform.Rotate(Vector3.up * targetSpeed * Time.deltaTime);
        }

        if(_life <= 0) {

            
            Activate();

            if(_canPLaySystem)
            {
                _system.Play();
                GameFeel.Instance.PlayJuice(2, 0.5f);
                _canPLaySystem =false;
            }
            
        }
        _white.transform.localScale = new Vector3(_white.transform.localScale.x, _white.transform.localScale.y, 100-10*_life);
        _white.transform.localPosition = new Vector3(_white.transform.localPosition.x, -0.01f * _life, _white.transform.localPosition.z);

    }



private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && _life > 0)
        {
            _life -= 1;
        }
    }

    void Activate()
    {
        _activated = true;
        
        _rend.material = _mat;
    }

    void StopRise()
    {
        _canRiseWater = false;
    }
}
