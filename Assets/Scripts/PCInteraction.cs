using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PcInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField, TextArea] private string _message;
    [SerializeField] private float _letterDelay = 0.05f;

    private Coroutine _typingCoroutine;
    public GameObject _textPanel;
    public GameObject _pressA;
    bool isInRange;
    public bool _canActivateSomething;
    public GameObject _objectToActivate;
    public PlayerController _controller;
    public Animator _anim;

    public void StartTyping()
    {
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        _text.text = "";

        foreach (char letter in _message)
        {
            _text.text += letter;
            yield return new WaitForSeconds(_letterDelay);
        }
    }

    [SerializeField] private Transform _player;
    [SerializeField] private float _range = 2f;

    private bool _playerDetected;

    private void Update()
    {
        if (_player == null)
            return;



        if (isInRange && !_playerDetected)
        {
            _playerDetected = true;
            PlayerIn();
        }
        else if (!isInRange && _playerDetected)
        {
            _playerDetected = false;
        }

        if (isInRange)
        {

            if (Gamepad.current.buttonSouth.isPressed)
            {
                _pressA.SetActive(false);
                _textPanel.SetActive(true);
                _anim.enabled = true;
                StartTyping();
                _controller.enabled = false;
                Invoke("Activate", 1);
            }
            else
            {

            }
        }
        else
        {


        }
    }

    private void PlayerIn()
    {

    }

    void Activate()
    {
        if (_canActivateSomething)
        {
            _anim.enabled = true;
            _textPanel.SetActive(false);
            _controller.enabled = true;
            isInRange = false;
            _objectToActivate.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == _player)
        {
            _pressA.SetActive(true);
            Debug.Log("Is in range");
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == _player)
        {
            _textPanel.SetActive(false);
            _pressA.SetActive(false);
            isInRange = false;
        }
    }
}
