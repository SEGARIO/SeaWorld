
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotationMove : MonoBehaviour
{
    public float rotationSpeed = 150f;

    void Update()
    {
        if (Gamepad.current == null)
            return;

        float input = Gamepad.current.rightStick.x.ReadValue();

        transform.Rotate(
            0f,
            input * rotationSpeed * Time.deltaTime,
            0f,
            Space.World
        );
    }
}
