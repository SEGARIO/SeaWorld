using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private Button defaultButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }
}