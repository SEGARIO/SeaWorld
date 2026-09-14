using System;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private float moveZ = -10f;
    [SerializeField] private float lerpSpeed = 10f;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    private bool isHighlighted;
    [SerializeField] private GameObject monBouton;
    [SerializeField] private TMP_Text _buttonText;
    float lightAngle = 0f;
    public AudioClip _clip;
    AudioSource _source;
    bool _canPlaySound;
    int indexQuit;

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(
      IntPtr hWnd,
      string text,
      string caption,
      uint type
  );

    private void Awake()
    {
        _source = FindObjectOfType<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.localPosition;
        _canPlaySound = true;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(monBouton);
    }

    private void Update()
    {
        lightAngle = (lightAngle + Time.deltaTime *3) % 6.28f;
        _buttonText.fontMaterial.SetFloat("_LightAngle", lightAngle);
        isHighlighted = EventSystem.current.currentSelectedGameObject == gameObject;

        if (isHighlighted)
        {
            objectToActivate.SetActive(true);

            if(_canPlaySound)
            {
                _source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                _source.PlayOneShot(_clip);
                _canPlaySound = false;
            }
          
            Vector3 targetPosition = startPosition + new Vector3(0f, 0f, moveZ);

            rectTransform.localPosition = Vector3.Lerp(
                rectTransform.localPosition,
                targetPosition,
                Time.deltaTime * lerpSpeed
            );
        }
        else
        {
            _canPlaySound = true;
            objectToActivate.SetActive(false);

            rectTransform.localPosition = Vector3.Lerp(
                rectTransform.localPosition,
                startPosition,
                Time.deltaTime * lerpSpeed
            );
        }
    }

    public void Quit()
    {
        if(indexQuit == 0)
        {
            MessageBox(
           IntPtr.Zero,
           "Où tu crois aller comme ça?",
           "Erreur",
           0x10
       );
           
        }
        if (indexQuit == 1)
        {
            MessageBox(
           IntPtr.Zero,
           "Retourne jouer au jeu.",
           "Erreur",
           0x10
       );
            
        }
        if (indexQuit == 2)
        {
            MessageBox(
           IntPtr.Zero,
           "Qu'est ce que je t'ai dit?",
           "Erreur",
           0x10
       );
            
        }
        if (indexQuit == 3)
        {
            MessageBox(
           IntPtr.Zero,
           "Allez hop, on joue ensemble!",
           "Erreur",
           0x10
       );
          
        }
        if (indexQuit == 4)
        {
            MessageBox(
           IntPtr.Zero,
           "...",
           "Erreur",
           0x10
       );
           
        }
        if (indexQuit == 5)
        {
            MessageBox(
           IntPtr.Zero,
           "Bon très bien, j'ai compris...",
           "Erreur",
           0x10
       );
           
        }
        if (indexQuit == 6)
        {
            Application.Quit();
        }
        indexQuit += 1;
    }
}