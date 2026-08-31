using UnityEngine;
using UnityEngine.InputSystem;

public class Star : MonoBehaviour
{
    [Header("Interaction")]
    public Transform player;
    public float interactionRange = 3f;

    [Header("Object to Activate")]
    public GameObject objectToActivate;

    [Header("Animation")]
    public Animator animator;
    public string animationTrigger = "Activate";

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Active ou désactive l'objet selon la distance
        if (distance <= interactionRange)
        {
            objectToActivate.SetActive(true);

            // Appui sur le bouton South (A / Croix)
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                animator.SetTrigger(animationTrigger);
                Invoke("Destroyer", 1);
            }
        }
        else
        {
            objectToActivate.SetActive(false);
        }
    }

    void Destroyer()
    {
        Destroy(objectToActivate);
    }
}