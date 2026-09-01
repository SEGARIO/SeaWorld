using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class LevelSelection : MonoBehaviour
{
    public SO_Planets _currentPlanet;
    public SO_Planets[] _planets;
    public int value = 0;
    public Renderer _rend;
    public Transform _planetTransform;
    public AutoRotate _rotationScript;
    public GameObject[] _moons;
    public TextMeshProUGUI _availableText;
    public TextMeshProUGUI _planetName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (value == _planets.Length)
        {
            value = 0;
        }

        if (value == -1)
        {
            value = _planets.Length -1;
        }

        _currentPlanet = _planets[value];
        _rend.material.color = _currentPlanet._color;
        float _diameter = 4 + _currentPlanet._size / 5;
        _planetTransform.localScale = new Vector3(_diameter, _diameter, _diameter);
        float _rotationSpeed = _currentPlanet._nightDayCycle;
        _rotationScript.currentSpeed = _rotationSpeed;
        _rotationScript.targetSpeed = _rotationSpeed;
        _rotationScript.deceleration = _rotationSpeed;
        for (int i = 0; i < _moons.Length; i++)
        {
            if (i <= _currentPlanet._moons - 1)
            {
                _moons[i].SetActive(true);
            }
            else
            {
                _moons[i].SetActive(false);
            }
        }

        if(_currentPlanet._levelOfApparition == SO_Planets.LevelOfApparition.Locked)
        {
            _availableText.text = "Locked";
            _availableText.color = Color.red;
            _planetName.text = "";
        }
        if (_currentPlanet._levelOfApparition == SO_Planets.LevelOfApparition.Available)
        {
            _availableText.text = " ";
            _planetName.text = _currentPlanet._name;
        }
        if (_currentPlanet._levelOfApparition == SO_Planets.LevelOfApparition.ComingSoon)
        {
            _availableText.text = "COMING SOON";
            _availableText.color = Color.blue;
            _planetName.text = "";
        }
        if (_currentPlanet._levelOfApparition == SO_Planets.LevelOfApparition.IDK)
        {
            _availableText.text = "?";
            _availableText.color = Color.white;
            _rend.material.color = Color.black;
            _planetName.text = "";
        }
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
