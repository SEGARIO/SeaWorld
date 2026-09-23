using UnityEngine;
using UnityEngine.SceneManagement;

public class Levierthan : MonoBehaviour
{
    public SO_Enemy _scriptable;
    public int _life;
    public GameObject _playerTransform;
    public float _timer;
    int _randomAttack;
    public Transform _attackOrigin;


    [Header("Intro")]
    public float _introDuration;

    [Header("Acid Attack")]
    public GameObject _prefacAcid;
    public float _projectionForce;
    public float _minTimerAcidAttack;
    public float _maxTimerAcidAttack;
    public float _timeBetweenAcidAttacks;
    public float _timerAcid;

    [Header("Stalactit Attack")]
    public GameObject _prefabStalactit;

    public float _minTimerStalactitAttack;
    public float _maxTimerStalactitAttack;
    public float _timeBetweenStalactits;
    public float _timerStalactit;

    [Header("Vulnerable")]
    public float _minVulnerableTime;
    public float _maxVulnerableTime;

    [Header("Underground")]
    public float _undergroundTime;

    public float _timerUnderground;
    


    [Header("Between Attacks")]
    public float _minTimeBeforeAttack;
    public float _maxTimeBeforeAttack;

    public bool _canNewAttack;
    public Transform _originPosition;
    public Animator _animator;
    public Animator _camAnimator;
    public GameObject _deathScene;
    public GameObject _dialogueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _life = _scriptable._life;
        _timer = _introDuration;
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        _timerAcid -= Time.deltaTime;
        _timerStalactit -= Time.deltaTime;
        _timerUnderground -= Time.deltaTime;

        if(_timer <= 0)
        {
            _canNewAttack = true;
            NewAttack();
        }

        if(_life <= 0)
        {
            Death();
        }

        transform.LookAt(new Vector3(_playerTransform.transform.position.x,this.transform.position.y, _playerTransform.transform.position.z));
    }

    void NewAttack()
    {
        _randomAttack = Random.Range(0, 4);
        if(_canNewAttack)
        {
            if (_randomAttack == 0)
            {
                Acid();
            }
            if (_randomAttack == 1)
            {
                Stalactit();
            }
            if (_randomAttack == 2)
            {
                Vulnerable();
            }
            if (_randomAttack == 3)
            {
                Underground();
            }
            _canNewAttack = false;
        }

      
    }
    void Acid()
    {
        _timerAcid = Random.Range(_minTimerAcidAttack, _maxTimerAcidAttack);
        _timer = _timerAcid;
        Invoke("ShootAcid", _timeBetweenAcidAttacks);
    }

    void ShootAcid()
    {
        if(_timerAcid > 0)
        {
            // Instancie + force
            var obj = Instantiate(_prefacAcid, _attackOrigin.position, _attackOrigin.rotation);

            if (obj.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(_attackOrigin.forward * _projectionForce, ForceMode.Impulse);
            }
            Invoke("ShootAcid", _timeBetweenAcidAttacks);
        }
        else
        {
            return;
        }
       
    }

    void Stalactit()
    {
        _timerStalactit = Random.Range(_minTimerStalactitAttack, _maxTimerStalactitAttack);
        _timer = _timerStalactit;
        Invoke("SpawnStalactit", _timeBetweenStalactits);
    }

    void SpawnStalactit()
    {
        if (_timerStalactit > 0)
        {
            Instantiate(_prefabStalactit, new Vector3(Random.Range(_originPosition.transform.position.x - 15, _originPosition.transform.position.x + 15), 0, Random.Range(_originPosition.transform.position.z -15, _originPosition.transform.position.z + 15)), Quaternion.identity);
            Invoke("SpawnStalactit", _timeBetweenStalactits);
        }
        else
        {
            return;
        }
    }

    void Vulnerable()
    {
        _timer = Random.Range(_minVulnerableTime, _maxVulnerableTime);
        _animator.SetTrigger("Vulnerable");
        Invoke("EndVulnerable", _timer - 2);
    }

    void EndVulnerable()
    {
        _animator.SetTrigger("EndVulnerable");
    }

    void Underground()
    {
        _timer = _undergroundTime;
        _timerUnderground = _undergroundTime;
        _animator.SetTrigger("Underground");
        Invoke("MoveUnderground", 2);
    }

    void MoveUnderground()
    {
        this.transform.position = new Vector3(Random.Range(_originPosition.transform.position.x - 13, _originPosition.transform.position.x + 13), 0, Random.Range(_originPosition.transform.position.z - 13, _originPosition.transform.position.z + 13));
    }

    void Death()
    {
       
        _deathScene.SetActive(true);
        _dialogueText.SetActive(true);
        Destroy(this.gameObject);
    }

  
}
