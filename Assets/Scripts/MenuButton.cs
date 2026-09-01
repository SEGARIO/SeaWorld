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
    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.localPosition;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(monBouton);
    }

    private void Update()
    {
        isHighlighted = EventSystem.current.currentSelectedGameObject == gameObject;

        if (isHighlighted)
        {
            objectToActivate.SetActive(true);

            Vector3 targetPosition = startPosition + new Vector3(0f, 0f, moveZ);

            rectTransform.localPosition = Vector3.Lerp(
                rectTransform.localPosition,
                targetPosition,
                Time.deltaTime * lerpSpeed
            );
        }
        else
        {
            objectToActivate.SetActive(false);

            rectTransform.localPosition = Vector3.Lerp(
                rectTransform.localPosition,
                startPosition,
                Time.deltaTime * lerpSpeed
            );
        }
    }
}