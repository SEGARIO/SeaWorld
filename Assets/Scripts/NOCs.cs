using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class NOCs : MonoBehaviour
{

    public string _npcName;
    public Color _npcNameColor;
    public bool _isInRange;
    public Transform _player;
    public bool _isTalking;
    public GameObject _dialoguePanel;
    public GameObject _pressA;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _nameText;
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

    public Transform _target;
    public bool _canTurnWhenTalking;
    public bool _haveMultipleIdles;
    public Color _cosmoColor;
    public Color _secondaryColor;
    public string _secondaryName;

    public bool _canGiveSomething;
    public SO_Items _itemToGive;
    public TMP_Text _itemText;
    public GameObject _buyPanel;
    public BuyPanel _buyScript;

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
        
        if (_anim != null && _haveMultipleIdles)
        {
            _anim.SetInteger("Random", UnityEngine.Random.Range(0, 4));
        }
        
        if (_isInRange)
        {
            _text.text = _dialogues[_index];
            _text.color = _textColors[_index];
            _audioSource.clip = _audios[_index];
            
            if (Gamepad.current.buttonSouth.isPressed && _canPress)
            {
                _isTalking = true ;
                _nameText.text = _npcName;
                _nameText.color = _npcNameColor;
                if (_anim != null)
                {
                    _anim.SetBool("IsTalking", true);
                }
                
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
            Debug.Log("TEXT : " + _text.color);
            Debug.Log("SECONDARY : " + _secondaryColor);

            Debug.Log($"R: {_text.color.r} / {_secondaryColor.r}");
            Debug.Log($"G: {_text.color.g} / {_secondaryColor.g}");
            Debug.Log($"B: {_text.color.b} / {_secondaryColor.b}");
            Debug.Log($"A: {_text.color.a} / {_secondaryColor.a}");
            if (Vector4.Distance(_text.color, _secondaryColor) < 0.01f)
            {
                Debug.Log("SameColor");
                _nameText.text = _secondaryName;
                _nameText.color = _secondaryColor;
            }
            Debug.Log("I S talking");
            if (_text.color == _cosmoColor)
            {
                _nameText.text = "Cosmo";
                _nameText.color = _cosmoColor;
            }
            if (_text.color == Color.white)
            {
                _nameText.text = _npcName;
                _nameText.color = _npcNameColor;
            }
            if (_text.color == Color.blue)
            {
                _nameText.text = "Enfant";
                _nameText.color = Color.blue;
            }
            if (_text.color == Color.red)
            {
                _nameText.text = "Homme aux toilettes";
                _nameText.color = Color.red;
            }
            
            _pressA.SetActive(false);

            if(_canTurnWhenTalking)
            {
                transform.LookAt(new Vector3(_target.transform.position.x, this.transform.position.y, _target.transform.position.z));

            }
            _player.GetComponent<PlayerController>()._objectToTurnWhenTalking.transform.LookAt(new Vector3(this.gameObject.transform.position.x, _player.transform.position.y, this.gameObject.transform.position.z));
            _player.GetComponent<PlayerController>()._animator.SetBool("IsTalking", true);
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
            _player.GetComponent<PlayerController>()._animator.SetBool("IsTalking", false);
            if (_anim != null)
            {
                _player.GetComponent<PlayerController>()._animator.SetBool("IsTalking", false);
                _anim.SetBool("IsTalking", false);
            }
            
            _dialoguePanel.SetActive(false);
            if(_canActivateSomething)
            {
                for (int i = 0; i < _thingToActivate.Length; i++)
                {
                    _thingToActivate[i].SetActive(true);
                }
                
            }
            if(_canGiveSomething)
            {
                _buyPanel.SetActive(true);
                _buyScript._item = _itemToGive;
                _itemText.text = "Acheter " + _itemToGive._name + " pour " + _itemToGive._price + "?";
            }

            if(_canActivateAnimation && _anim != null)
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
            _player.GetComponent<PlayerController>()._animator.SetBool("IsTalking", false);
            _dialoguePanel.SetActive(false);
            if (_canActivateSomething)
            {
                for (int i = 0; i < _thingToActivate.Length; i++)
                {
                    _thingToActivate[i].SetActive(true);
                }

            }

            if (_canActivateAnimation && _anim != null)
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
