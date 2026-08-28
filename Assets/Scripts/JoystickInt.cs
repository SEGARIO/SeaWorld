using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JoystickInt : MonoBehaviour
{
    public SO_Planets _currentPlanet;
    public SO_Planets[] _planets;
    public int value = 0;

    public Image _planetImage;

    void Update()
    {
        if (Gamepad.current == null)
            return;

        float x = Gamepad.current.leftStick.ReadValue().x;

        // Droite
        if (x > 0.5f)
        {
            value += 1;

            // Attendre que le joystick revienne au centre
            enabled = false;
            StartCoroutine(WaitForJoystickRelease());
        }

        // Gauche
        else if (x < -0.5f)
        {
            value -= 1;

            enabled = false;
            StartCoroutine(WaitForJoystickRelease());
        }

        _currentPlanet = _planets[value];
        _planetImage.color = _currentPlanet._color;
    }

    System.Collections.IEnumerator WaitForJoystickRelease()
    {
        yield return new WaitUntil(() =>
            Gamepad.current == null ||
            Mathf.Abs(Gamepad.current.leftStick.ReadValue().x) < 0.2f
        );

        enabled = true;
    }
}