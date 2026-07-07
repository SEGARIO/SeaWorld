using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private List<Dial> entries = new List<Dial>();
}

[Serializable]
public class Dial
{
    public string _text;
    public Sprite _characterSprite;
    public AudioClip _voice;
    public int _time;
}