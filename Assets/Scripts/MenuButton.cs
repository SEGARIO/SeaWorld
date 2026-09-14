using TMPro;
using UnityEngine;
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
                _source.pitch = Random.Range(0.8f, 1.2f);
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
}