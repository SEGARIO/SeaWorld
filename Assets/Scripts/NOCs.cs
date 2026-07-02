using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using TMPro;

public class NOCs : MonoBehaviour
{
    public bool _isInRange;
    public Transform _player;
    public bool _isTalking;
    public GameObject _dialoguePanel;
    public GameObject _pressA;
    public TextMeshProUGUI _text;

    public string[] _dialogues;
    public int _index;
    public bool _canPress = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInRange)
        {
            _text.text = _dialogues[_index];
            if (Gamepad.current.buttonSouth.isPressed && _canPress)
            {
                _isTalking = true ;
                NextDialogue();
                _canPress = false;
            }
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            {

                _canPress = true;
            }
        }

        if(_isTalking)
        {
            _pressA.SetActive(false);
            _player.GetComponent<PlayerController>().enabled = false;
            _dialoguePanel.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == _player)
        {
            _canPress = true;
            Debug.Log("CanTalk");
            _pressA.SetActive(true);
            _isInRange = true;
            _index = 0;
        }
    }

    void NextDialogue()
    {
        _index += 1;

        if(_index >= _dialogues.Length)
        {
            _isTalking = false;
            _dialoguePanel.SetActive(false);
            
            _player.GetComponent<PlayerController>().enabled = true;
        }
            
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == _player)
        {
            _pressA.SetActive(false);
            _isInRange = false;
        }
    }
}
