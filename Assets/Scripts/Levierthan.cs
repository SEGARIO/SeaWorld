using UnityEngine;

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
    

    [Header("Between Attacks")]
    public float _minTimeBeforeAttack;
    public float _maxTimeBeforeAttack;

    public bool _canNewAttack;
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

        if(_timer <= 0)
        {
            _canNewAttack = true;
            NewAttack();
        }

        transform.LookAt(new Vector3(_playerTransform.transform.position.x,this.transform.position.y, _playerTransform.transform.position.z));
    }

    void NewAttack()
    {
        _randomAttack = Random.Range(0, 3);
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
            _canNewAttack = false;
        }

      
    }
    void Acid()
    {
        _timerAcid = Random.Range(_minTimerAcidAttack, _maxTimerAcidAttack);
        _timer = Random.Range(_minTimerAcidAttack, _maxTimerAcidAttack);
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
        _timer = Random.Range(_minTimerStalactitAttack, _maxTimerStalactitAttack);
        Invoke("SpawnStalactit", _timeBetweenStalactits);
    }

    void SpawnStalactit()
    {
        if (_timerStalactit > 0)
        {
            Instantiate(_prefabStalactit, new Vector3(Random.Range(this.transform.position.x - 15, this.transform.position.x + 15), 0, Random.Range(this.transform.position.z -15, this.transform.position.z + 15)), Quaternion.identity);
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
    }
}
