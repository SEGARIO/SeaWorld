using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RobotPlayerController : MonoBehaviour
{
    public NavMeshAgent _agent;
    public GameObject _target;

    void Start()
    {
       
      
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    // Fonction appelée par l'Input System
    public void OnMove(InputValue value)
    {
       
    }
}