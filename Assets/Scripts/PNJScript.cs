using UnityEngine;
using UnityEngine.InputSystem;

public class PNJScript : MonoBehaviour
{
    public Transform player;
    public float range = 3f;
    public DialogueSystem _dialogueSystem;
    public GameObject _pressA;
    public GameObject _dialogueCanvas;

    void Update()
    {
        if (_dialogueSystem.IsDialogueFinished())
        {
            player.GetComponent<PlayerController>().enabled = true;
            _pressA.SetActive(true);
            _dialogueCanvas.SetActive(false);
        }
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                Interact();
                _pressA.SetActive(false);
            }
            else
            {
                
            }
        }
    }

    void Interact()
    {
        _dialogueSystem.enabled = true;
        _dialogueCanvas.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Interaction déclenchée !");

        // 👉 Mets ton code ici
    }


}