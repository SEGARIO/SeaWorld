using UnityEngine;
using UnityEngine.InputSystem;

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
    
    void Start()
    {
        _playerSwitcher = FindObjectOfType<PlayerSwitcher>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log(timerChangePlayer);
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.magnitude > 1f)
            move.Normalize();

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(move * speed * Time.deltaTime);
            _smokeParticles.startLifetime = 1;
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
    }

    // Fonction appelée par l'Input System
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}