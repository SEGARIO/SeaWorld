using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    public ParticleSystem _smokeParticles;
    PlayerSwitcher _playerSwitcher;
    float timerChangePlayer;
    public Animator _animator;
    int _randomIdle;
    
    void Start()
    {
        _playerSwitcher = FindObjectOfType<PlayerSwitcher>();
        controller = GetComponent<CharacterController>();
        if (SceneManager.GetActiveScene().name != "Subway")
        {
            _animator.SetBool("Subway", false);
            Debug.Log("Subway");
        }
    }

    void Update()
    {
        _randomIdle = Random.Range(0, 3);
        _animator.SetInteger("RandomIdle", _randomIdle);
        //Debug.Log(timerChangePlayer);
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.magnitude > 1f)
            move.Normalize();

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(move * speed * Time.deltaTime);

            if(move.magnitude > 0.7f)
            {
                _smokeParticles.startLifetime = move.magnitude;
            }

            if(move.magnitude < 0.8f)
            {
                _animator.speed = move.magnitude*3;
            }
            else
            {
                _animator.speed = 1;
            }
          
        }
        else
        {
            _smokeParticles.startLifetime = 0;
        }

        // Gravité
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /* if (Gamepad.current.buttonSouth.isPressed)
         {
             timerChangePlayer -= Time.deltaTime;
         }
         else
         {
             timerChangePlayer = 1.5f;
         }


         if(timerChangePlayer <= 0)
         {
             //_playerSwitcher.ChangePlayer();
             //Debug.Log(timerChangePlayer);
             timerChangePlayer = 1.5f;

         }*/

        if (move.magnitude == 0)
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", false);
        }
            if (move.magnitude >= 0.9f)
            {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsWalking", false);
        }
               
        if (move.magnitude > 0f && move.magnitude < 0.9f)
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", true);
        }
    }

    // Fonction appelée par l'Input System
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}