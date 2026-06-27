using UnityEngine;

public class Checkcpoint : MonoBehaviour
{
    PlayerLife _player;
    public Renderer _rend;
    public Material _mat;
    public static bool _isActivated;
    GameManager _manager;
    public ParticleSystem _system;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(_manager._checkpoint == this)
        {
            _isActivated = true;
        }
        else
        {
            _isActivated = false;
        }

      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Checkpoint");
            _player = other.GetComponent<PlayerLife>();
            _system.Stop();
            _manager._checkpoint = this.transform;
            _rend.material = _mat;
        }
    }
}
