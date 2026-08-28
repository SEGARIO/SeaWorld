using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JoystickInt : MonoBehaviour
{
    public SO_Planets _currentPlanet;
    public SO_Planets[] _planets;
    public int value = 0;

    public Image _planetImage;
    public Text _name;
    public Text _description;
    public Text _position;
    public Text _size;
    public Text temp;
    public Text nighDay;
    public Text population;
    public Text _government;
    public Text _moons;
    public Text _activities;
    public Text _money;

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
        _name.text = _currentPlanet._name;
        _description.text = _currentPlanet._description;
        _position.text = $"Position: {_currentPlanet._position}/15";
        _size.text = $"Taille: {_currentPlanet._size}000km de diamètre";
        temp.text = $"Température moyenne: {_currentPlanet._temperatureMoyenne}°C";
        nighDay.text = $"Cycle jour-nuit: {_currentPlanet._nightDayCycle}h";
        population.text = $"Population: {_currentPlanet._population} d'habitants";
        
        _government.text = $"Gouvernement: {_currentPlanet._gouvernement}";
        _moons.text = $"Nombre de lunes: {_currentPlanet._moons}";
        _activities.text = $"Activités: {_currentPlanet._activities}";
        _money.text = $"Monnaie: {_currentPlanet._money}";
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