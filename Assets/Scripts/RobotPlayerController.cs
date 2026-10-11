
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotPlayerController : MonoBehaviour
{
    public Animator _anim;
    public ParticleSystem _part;
    public CharacterController _controller;
    public Transform _camera;

    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    private float verticalVelocity;
    private Vector2 _moveInput;

    void Start()
    {
        if (_controller == null)
            _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _anim.SetInteger("Random", Random.Range(0, 3));
        // Directions relatives à la caméra
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;

        // On ignore l'inclinaison verticale de la caméra
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * _moveInput.y
                         + right * _moveInput.x;

        if (_controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        bool isMoving = movement.sqrMagnitude > 0.0001f;

        if (isMoving)
        {
            movement.Normalize();

            // Déplacement avec collisions
           

            // Rotation vers la direction du déplacement
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        _controller.Move(
    (movement * speed + Vector3.up * verticalVelocity) * Time.deltaTime
);
        // Animation
        _anim.SetBool("IsWalking", isMoving);

        if (_part != null)
        {
            var main = _part.main;
            main.startLifetime = isMoving ? 1f : 0f;
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PushableBox box = hit.collider.GetComponentInParent<PushableBox>();

        if (box == null)
            return;

        Vector3 direction = hit.moveDirection;
        direction.y = 0f;

        box.Push(direction);
    }
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}
