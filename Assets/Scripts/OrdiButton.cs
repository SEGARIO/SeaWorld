using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OrdiButton : MonoBehaviour
{
    [Header("Objet à détecter")]
    public RectTransform objectToDetect;

    [Header("Objet à activer")]
    public GameObject objectToActivate;
    public GameObject objectToActivate2;
  
    public GameObject[] objectToDisctivate;

    private Button button;
    private Color normalColor;
    private Color highlightedColor;

    private bool objectIsOnButton = false;

    void Start()
    {
        button = GetComponent<Button>();

        ColorBlock colors = button.colors;

        normalColor = colors.normalColor;
        highlightedColor = colors.highlightedColor;
    }

    void Update()
    {
        if (objectToDetect == null)
            return;

        // Vérifie si les deux RectTransform se chevauchent
        objectIsOnButton = RectTransformUtility.RectangleContainsScreenPoint(
            GetComponent<RectTransform>(),
            objectToDetect.position,
            null
        );

        // Change la couleur
        ColorBlock colors = button.colors;

        if (objectIsOnButton)
            colors.normalColor = highlightedColor;
        else
            colors.normalColor = normalColor;

        button.colors = colors;

        // Appui sur A / Cross
        if (objectIsOnButton && Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
               
                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);

                    for (int i = 0; i < objectToDisctivate.Length; i++)
                    {

                        objectToDisctivate[i].SetActive(false);
                    }
                    if(objectToActivate2 != null)
                    {
                        Invoke("Reactivate", 2);
                    }
                  
                   
                }
              
            }
        }
    }

    void Reactivate()
    {
        if (objectToActivate2 != null)
        {
            objectToActivate2.SetActive(true);
        }
    }
}