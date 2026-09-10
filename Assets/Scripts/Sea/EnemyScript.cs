using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public GameObject _deathParticle;
    public SO_Enemy _enemy;
    public int _life;
    NavMeshAgent _agent;
    bool _followplayer;
    Animator _anim;

   Transform player;
    public ParticleSystem _smokePart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemy._speed;
        _life = _enemy._life;

        if(_enemy._isAgressive)
        {
           player = FindObjectOfType<PlayerController>().gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy._isAgressive)
        {
            if (_enemy._detectsWithSight)
            {
                CheckForPlayer();
            }
            else
            {
                if (player == null) return;

                float distance = Vector3.Distance(transform.position, player.position);

                //Debug.Log("Distance au player : " + distance);

                if (distance <= _enemy._detectionRange)
                {
                    DetectPlayer();
                }
            }

        }
        _anim.SetInteger("Random", Random.Range(0, 3));
        if(_life <= 0)
        {
            Death();
        }

        if(_followplayer)
        {
            _anim.SetBool("IsWalking", true);
            _agent.SetDestination(FindObjectOfType<PlayerController>().gameObject.transform.position);
            _smokePart.startLifetime = 2;
        }
        else
        {
            _smokePart.startLifetime = 0;
        }
      
       
       
    }
    public void CheckForPlayer()
    {
        Debug.DrawRay(transform.position, transform.forward * _enemy._detectionRange, Color.red, 1f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _enemy._detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                DetectPlayer();
            }
        }
       
    }
    public void DetectPlayer()
    {
        _followplayer = true;
    }

    void Death()
    {
        _enemy._hasBeenMet = true;
        GameFeel.Instance.PlayJuice(1.5f, 0.3f);

        //GameFeel.Instance.Flash(0.1f);
        Instantiate(_deathParticle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);    
    }
}
