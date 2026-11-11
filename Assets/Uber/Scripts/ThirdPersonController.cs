
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MotorcycleController : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;
    public Transform bikeVisual;

    [Header("Movement")]
    public float moveSpeed = 12f;
    public float sprintSpeed = 20f;
    public float acceleration = 5f;
    public float brakeAcceleration = 12f;
    public float coastDeceleration = 2f;
    public float steeringSpeed = 90f;
    public float lowSpeedSteering = 0.3f;

    [Header("Motorcycle Lean")]
    public float maxLeanAngle = 30f;
    public float leanSmoothness = 5f;

    [Header("Camera")]
    public float cameraDistance = 6f;
    public float cameraHeight = 2f;
    public float mouseSensitivity = 0.15f;
    public float gamepadSensitivity = 120f;
    public float cameraSmoothness = 12f;
    public float minPitch = -30f;
    public float maxPitch = 65f;
    public bool invertY = false;

    [Header("Gravity")]
    public float gravity = -20f;

    private CharacterController controller;

    private float currentSpeed;
    private float verticalVelocity;

    private float steeringInput;
    private float throttleInput;

    private float yaw;
    private float pitch = 15f;

    private float smoothYaw;
    private float smoothPitch;

    private float yawVelocity;
    private float pitchVelocity;

    private float currentLean;
    private Quaternion visualStartRotation;

    private bool cursorLocked = true;
    public ParticleSystem particleSystem;
    void Start()
    {
        controller = GetComponent<CharacterController>();

        yaw = transform.eulerAngles.y;
        smoothYaw = yaw;

        if (bikeVisual != null)
            visualStartRotation = bikeVisual.localRotation;

        
    }

    void Update()
    {
        HandleCursor();
        HandleCameraInput();
        HandleMovement();
        var emission = particleSystem.emission;
        emission.rateOverTime = controller.velocity.magnitude * 10f;
    }

    void LateUpdate()
    {
        UpdateCamera();
        UpdateLean();
    }

    // CURSOR

    void HandleCursor()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
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

        // Keyboard

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

        // Gamepad

        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();

            if (stick.magnitude > input.magnitude)
                input = stick;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        throttleInput = input.y;
        steeringInput = input.x;

        // Sprint

        bool sprinting = false;

        if (Keyboard.current != null)
            sprinting = Keyboard.current.leftShiftKey.isPressed;

        if (Gamepad.current != null)
            sprinting |= Gamepad.current.leftStickButton.isPressed;

        float maxSpeed = sprinting ? sprintSpeed : moveSpeed;

        // ACCELERATION / BRAKING

        float targetSpeed = throttleInput * maxSpeed;

        if (Mathf.Abs(throttleInput) > 0.01f)
        {
            float accel = Mathf.Sign(targetSpeed) != Mathf.Sign(currentSpeed)
                && Mathf.Abs(currentSpeed) > 0.1f
                ? brakeAcceleration
                : acceleration;

            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                targetSpeed,
                accel * Time.deltaTime
            );
        }
        else
        {
            // Natural slowdown when releasing the throttle

            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                coastDeceleration * Time.deltaTime
            );
        }

        // STEERING

        float speedFactor = Mathf.Lerp(
            lowSpeedSteering,
            1f,
            Mathf.Clamp01(Mathf.Abs(currentSpeed) / moveSpeed)
        );

        float reverseFactor = currentSpeed < -0.1f ? -1f : 1f;

        float turnAmount = steeringInput *
                           steeringSpeed *
                           speedFactor *
                           reverseFactor *
                           Time.deltaTime;

        transform.Rotate(0f, turnAmount, 0f);

        // FORWARD MOVEMENT

        Vector3 moveDirection = transform.forward;

        Vector3 horizontalVelocity = moveDirection * currentSpeed;

        // GRAVITY

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalVelocity = horizontalVelocity;
        finalVelocity.y = verticalVelocity;

        controller.Move(finalVelocity * Time.deltaTime);
    }

    // MOTORCYCLE LEAN

    void UpdateLean()
    {
        if (bikeVisual == null) return;

        float speedFactor = Mathf.Clamp01(
            Mathf.Abs(currentSpeed) / moveSpeed
        );

        float targetLean = -steeringInput * maxLeanAngle * speedFactor;

        currentLean = Mathf.Lerp(
            currentLean,
            targetLean,
            leanSmoothness * Time.deltaTime
        );

        Quaternion leanRotation = Quaternion.Euler(
            0f,
            0f,
            currentLean
        );

        bikeVisual.localRotation = Quaternion.Slerp(
            bikeVisual.localRotation,
            visualStartRotation * leanRotation,
            leanSmoothness * Time.deltaTime
        );
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
