using Unity.VisualScripting;
using UnityEngine;

public class BoosScript : MonoBehaviour
{
    public SO_Enemy _scriptable;
    public int _life;
    public float _introDuration;
    public int _index;
    public Animator _anim;

    public Renderer[] _renderers;

    public Color[] _originalColors;
    public Color _hitColor;
    public float _timeUnderground;

    [Header("Acid Attack")]
    public GameObject _acidProjectile;
    public Transform _acidProjectileOrigin;
    public float _projectionForce;
    public float _timeBeforeStartingShoot;
    public float _maxDurationAcidAttack;
    public float _minDurationAcidAttack;
    float _durationAcidAttack;
    public float _timeBetweenProjectiles;
    public float _limitTimeBetweenProjectiles;
    bool _isDoingAcidAttack;

    [Header("Stalactit Attack")]
    public GameObject _stalactit;
    public float _timeBeforeStalactits;
    public float _minDurationStalactitAttack;
    public float _maxDurationStalactitAttack;
    public float _durationStalactitAttack;
    public float _timeBetweenStalactits;
    public float _limitTimeBetweenStalactits;
    public bool _isDoingStalactits;

    [Header("Vulnerable")]
    public float _minVulnerableDuration;
    public float _maxVulnerableDuration;
    bool _isVulnerable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _life = _scriptable._life;
        Invoke("AcidAttack", _introDuration);
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
        _anim.SetTrigger("PlayIntro");
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDoingAcidAttack)
        {
            _durationAcidAttack -= Time.deltaTime;
        }
        if(_isDoingStalactits)
        {
            _durationStalactitAttack -= Time.deltaTime;
           
        }
      
        if(_durationStalactitAttack <= 0)
        {
            _isDoingStalactits = false;
        }

        if(_life <= 0)
        {
            Death();
        }
    }

    void AcidAttack()
    {
        _anim.SetTrigger("AcidAttack");
        Invoke("ShootAcid", _timeBeforeStartingShoot);
        _isDoingAcidAttack = true;
        Debug.Log("Acid attack");
        _durationAcidAttack = Random.Range(_minDurationAcidAttack, _maxDurationAcidAttack);
    }

    void ShootAcid()
    {
        _anim.SetTrigger("ShootAcid");
        // Vérifie le cooldown

        // Instancie + force
        var obj = Instantiate(_acidProjectile, _acidProjectileOrigin.position, _acidProjectileOrigin.rotation);

        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(_acidProjectileOrigin.forward * _projectionForce, ForceMode.Impulse);
        }

        if(_durationAcidAttack >= 0)
        {
            _isDoingAcidAttack = true;
            Invoke("ShootAcid", _timeBetweenProjectiles);
        }
        else
        {
            NextAttack();
            _isDoingAcidAttack = false;
            Debug.Log("End Acid Attack");
        }
        
    }

    void NextAttack()
    {
        _anim.SetTrigger("Underground");
        _index = Random.Range(0, 3);

        if(_index == 0)
        {
            AcidAttack();
        }
        if (_index == 1)
        {
            StalactitAttack();
        }
        if(_index == 2)
        {
            Vulnerble();
        }
    }
    void StalactitAttack()
    {
        _anim.SetTrigger("StalactitAttack");
        Invoke("SpawnStalactit", _timeBeforeStalactits);
        _isDoingStalactits = true;
        _durationStalactitAttack = Random.Range(_minDurationStalactitAttack, _maxDurationStalactitAttack);
        Debug.Log("Stalactit attack");
    }

    void SpawnStalactit()
    {
        Debug.Log("Spawn Stalactit");
        Instantiate(_stalactit, new Vector3(Random.Range(this.transform.position.x -25, this.transform.position.x + 25), 0, Random.Range(this.transform.position.z - 25, this.transform.position.z + 25)), Quaternion.identity);

        if (_isDoingStalactits)
        {
            Debug.Log("Superieur à 0");
            Invoke("SpawnStalactit", _timeBetweenStalactits);
        }
        else
        {
            NextAttack();
            _isDoingStalactits = false;
            Debug.Log("End Stalactits Attack");
        }
        
    }

    void Vulnerble()
    {
        _anim.SetTrigger("Vulnerable");
        _isVulnerable = true;
        Invoke("EndVulnerable", Random.Range(_minVulnerableDuration, _maxVulnerableDuration));
        Debug.Log("Vulnerable");
    }

    void EndVulnerable()
    {
        _anim.SetTrigger("EndVulnerable");
        _isVulnerable = false;
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
        NextAttack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && _isVulnerable)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _hitColor;

                Invoke("OriginalColors", 0.1f);
            }
            _life -= 1;

            
            Destroy(collision.gameObject);
        }
    }

    void OriginalColors()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
    }

    void Death()
    {
        _anim.SetTrigger("Death");
        Destroy(gameObject);
    }
}
