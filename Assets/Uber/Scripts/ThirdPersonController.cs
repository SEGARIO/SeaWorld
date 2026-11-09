
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float acceleration = 12f;
    public float rotationSpeed = 12f;

    [Header("Camera")]
    public float cameraDistance = 5f;
    public float cameraHeight = 1.6f;
    public float mouseSensitivity = 0.15f;
    public float gamepadSensitivity = 120f;
    public float cameraSmoothness = 12f;
    public float minPitch = -30f;
    public float maxPitch = 65f;
    public bool invertY = false;

    [Header("Gravity")]
    public float gravity = -20f;

    private CharacterController controller;

    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    private float yaw;
    private float pitch = 15f;

    private float smoothYaw;
    private float smoothPitch;

    private float yawVelocity;
    private float pitchVelocity;

    private bool cursorLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        yaw = transform.eulerAngles.y;
        smoothYaw = yaw;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCursor();
        HandleCameraInput();
        HandleMovement();
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    // CURSOR

    void HandleCursor()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            cursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            !cursorLocked)
        {
            cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // CAMERA INPUT

    void HandleCameraInput()
    {
        Vector2 lookInput = Vector2.zero;

        if (cursorLocked && Mouse.current != null)
        {
            lookInput += Mouse.current.delta.ReadValue() * mouseSensitivity;
        }

        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.rightStick.ReadValue();

            lookInput.x += stick.x * gamepadSensitivity * Time.deltaTime;
            lookInput.y += stick.y * gamepadSensitivity * Time.deltaTime;
        }

        yaw += lookInput.x;

        float yInput = invertY ? lookInput.y : -lookInput.y;

        pitch += yInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    // MOVEMENT

    void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.zKey.isPressed)
                input.y += 1;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.qKey.isPressed)
                input.x -= 1;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1;
        }

        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();

            if (stick.magnitude > input.magnitude)
                input = stick;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        // Direction relative to camera

        Vector3 camForward = playerCamera.forward;
        Vector3 camRight = playerCamera.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection =
            camForward * input.y +
            camRight * input.x;

        moveDirection.Normalize();

        // Sprint

        bool sprinting = false;

        if (Keyboard.current != null)
            sprinting = Keyboard.current.leftShiftKey.isPressed;

        if (Gamepad.current != null)
            sprinting |= Gamepad.current.leftStickButton.isPressed;

        float targetSpeed = sprinting ? sprintSpeed : moveSpeed;

        Vector3 targetVelocity = moveDirection * targetSpeed;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            acceleration * Time.deltaTime
        );

        // Rotate player

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Gravity

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalVelocity = horizontalVelocity;
        finalVelocity.y = verticalVelocity;

        controller.Move(finalVelocity * Time.deltaTime);
    }

    // CAMERA POSITION

    void UpdateCamera()
    {
        if (playerCamera == null) return;

        smoothYaw = Mathf.SmoothDampAngle(
            smoothYaw,
            yaw,
            ref yawVelocity,
            1f / cameraSmoothness
        );

        smoothPitch = Mathf.SmoothDampAngle(
            smoothPitch,
            pitch,
            ref pitchVelocity,
            1f / cameraSmoothness
        );

        Quaternion cameraRotation =
            Quaternion.Euler(smoothPitch, smoothYaw, 0);

        Vector3 pivot = transform.position +
                        Vector3.up * cameraHeight;

        Vector3 desiredPosition =
            pivot - cameraRotation * Vector3.forward * cameraDistance;

        // Camera collision

        Vector3 direction = desiredPosition - pivot;
        float distance = direction.magnitude;

        if (Physics.SphereCast(
            pivot,
            0.2f,
            direction.normalized,
            out RaycastHit hit,
            distance,
            ~0,
            QueryTriggerInteraction.Ignore))
        {
            desiredPosition = pivot +
                direction.normalized * Mathf.Max(0.1f, hit.distance - 0.15f);
        }

        playerCamera.position = desiredPosition;
        playerCamera.rotation = cameraRotation;
    }
}
