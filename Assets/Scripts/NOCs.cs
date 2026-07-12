using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


public class NOCs : MonoBehaviour
{

    public string _npcName;
    public bool _isInRange;
    public Transform _player;
    public bool _isTalking;
    public GameObject _dialoguePanel;
    public GameObject _pressA;
    public TextMeshProUGUI _text;
    public bool _canActivateSomething;
    public bool _canActivateAnimation;
    public GameObject[] _thingToActivate;
    public bool _isGivingMission;
    public GameObject _exclamationMark;

    public string[] _dialogues;
    public Color[] _textColors;
    public AudioClip[] _audios;
    public int _index;
    public bool _canPress = true;
    public AudioSource _audioSource;
    bool _isPlayingSound;
    public Animator _anim;

    public bool _hasAnotherDialogue;
    public string[] _otherDialogues;
    public Color[] _otherTextColors;
    bool _hasFinishedTalkingDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_audioSource = FindObjectOfType<AudioSource>();
        for (int i = 0; i < _thingToActivate.Length; i++)
        {
            _thingToActivate[i].SetActive(false);
        }

        if(_isGivingMission)
        {
            _exclamationMark.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInRange)
        {
            _text.text = _dialogues[_index];
            _text.color = _textColors[_index];
            _audioSource.clip = _audios[_index];
            
            if (Gamepad.current.buttonSouth.isPressed && _canPress)
            {
                _isTalking = true ;
                if(_exclamationMark != null)
                {
                    _exclamationMark.GetComponent<Animator>().SetTrigger("Play");
                    Invoke("Destroyer", 0.5f);
                }
              
                _audioSource.PlayOneShot(_audios[_index]);
                NextDialogue();
                _canPress = false;
            }
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {

                _isPlayingSound = true;
               
            }
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            {

                _canPress = true;
                _isPlayingSound = false;
            }

            if(_isPlayingSound)
            {
                PlayVoice();
            }
        }

        if(_isTalking)
        {
            _pressA.SetActive(false);
            _player.GetComponent<PlayerController>().enabled = false;
            _dialoguePanel.SetActive(true);
        }
        
    }


    void Destroyer()
    {
        Destroy(_exclamationMark);
    }

    void PlayVoice()
    {

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

        if(!_hasFinishedTalkingDialogue && _index >= _dialogues.Length)
        {
            _isTalking = false;
            _dialoguePanel.SetActive(false);
            if(_canActivateSomething)
            {
                for (int i = 0; i < _thingToActivate.Length; i++)
                {
                    _thingToActivate[i].SetActive(true);
                }
                
            }

            if(_canActivateAnimation)
            {
                _anim.SetTrigger("Go");
            }
            _player.GetComponent<PlayerController>().enabled = true;
            if(_hasAnotherDialogue)
            {
                _dialogues = _otherDialogues;
                _textColors = _otherTextColors;
            }
        }
        if (_hasFinishedTalkingDialogue && _index >= _otherDialogues.Length)
        {
            _isTalking = false;
            _dialoguePanel.SetActive(false);
            if (_canActivateSomething)
            {
                for (int i = 0; i < _thingToActivate.Length; i++)
                {
                    _thingToActivate[i].SetActive(true);
                }

            }

            if (_canActivateAnimation)
            {
                _anim.SetTrigger("Go");
            }
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
