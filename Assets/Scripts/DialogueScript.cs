using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private List<Dial> entries = new List<Dial>();
    public TextMeshProUGUI _text;
    public int _index = -1;
    public bool _imobilisePlayer;
    public PlayerController _controller;
    public bool _canActivateSomething;
    public GameObject _objectToActivate;

    private void Start()
    {
        NextDialogue();

        if(_imobilisePlayer)
        {
            _controller.enabled = false;
        }
    }
    void NextDialogue()
    {
        _index += 1;
        _text.text = entries[_index]._text;
        _text.color = entries[_index]._textColor;
        Invoke("NextDialogue", entries[_index]._time);
    }

    private void Update()
    {
        if(_index >= entries.Count)
        {
            if(_imobilisePlayer)
            {
                _controller.enabled = true;
            }
            if(_canActivateSomething)
            {
                _objectToActivate.SetActive(true);
            }
            _text.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

[Serializable]
public class Dial
{
    public string _text;
    public Color _textColor;
    public Sprite _characterSprite;
    public AudioClip _voice;
    public int _time;
}