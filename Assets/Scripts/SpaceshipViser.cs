using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipViser : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float smoothTime = 0.08f;

    [Header("Limits")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private Vector2 input;
    private Vector3 targetPosition;
    private Vector3 velocity;

    private void Start()
    {
        targetPosition = transform.localPosition;
    }

    [SerializeField] private InputActionReference moveAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {

        input = moveAction.action.ReadValue<Vector2>();

        targetPosition += new Vector3(input.x, input.y, 0f) * moveSpeed * Time.deltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
