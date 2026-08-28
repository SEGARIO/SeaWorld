using UnityEngine;
using UnityEngine.InputSystem;

public class ReactivateMouse : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDisctivate;

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            objectToActivate.SetActive(true);
            objectToDisctivate.SetActive(false);
        }
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            objectToActivate.SetActive(true);
            objectToDisctivate.SetActive(false);
        }
    }
}
