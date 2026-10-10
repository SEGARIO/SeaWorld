using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RobotPlayerController : MonoBehaviour
{
    public NavMeshAgent _agent;
    public GameObject _target;
    public Animator _anim;
    public ParticleSystem _part;
    void Start()
    {
       
      
    }

    void Update()
    {
        _anim.SetInteger("Random", Random.Range(0, 3));
        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.magnitude > 0.3f)
        {
            _anim.SetBool("IsWalking", true);
            _part.startLifetime = _agent.velocity.magnitude /3;
        }
        else
        {
            _anim.SetBool("IsWalking", false);
            _part.startLifetime = 0;
        }
    }

    // Fonction appelée par l'Input System
    public void OnMove(InputValue value)
    {
       
    }
}