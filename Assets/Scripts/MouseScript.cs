using UnityEngine;
using UnityEngine.InputSystem;

public class MouseScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 500f;

    [Header("Y Limits")]
    public float minY = -300f;
    public float maxY = 300f;

    [Header("X Limits")]
    public float minX = -500f;
    public float maxX = 500f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Gamepad.current == null)
            return;

        // Joystick gauche
        Vector2 joystick = Gamepad.current.leftStick.ReadValue();

        // Déplacement sur le Canvas
        Vector2 movement = new Vector2(
            joystick.x,
            joystick.y
        ) * speed * Time.deltaTime;

        rectTransform.anchoredPosition += movement;

        // Limites
        Vector2 position = rectTransform.anchoredPosition;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        rectTransform.anchoredPosition = position;
    }
}