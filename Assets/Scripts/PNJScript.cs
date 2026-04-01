using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PNJScript : MonoBehaviour
{
    public bool _canRotate;
    public Transform player;
    public float range = 3f;
    public DialogueSystem _dialogueSystem;
    public GameObject _pressA;
    public GameObject _dialogueCanvas;
    bool _canStart = true;
    Animator _animator;
    Quaternion initialRotation;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if(_canRotate)
        {
            initialRotation = transform.rotation;

        }
    }
    void Update()
    {
        if (_dialogueSystem.IsDialogueFinished())
        {
            Debug.Log("Finished");
            if (_animator != null)

            {
                _animator.SetBool("IsTalking", false);
            }
            
            //player.GetComponent<PlayerController>().enabled = true;
            _canStart = true;
            _dialogueCanvas.SetActive(false);
        }
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                if(_canStart)
                {
                    _dialogueSystem.StartDialogue();
                    _canStart = false;
                }
                
                Interact();
               
            }
            else
            {
                
            }
            _pressA.SetActive(true);
            _dialogueSystem.enabled = true;
        }
        else
        {
            _pressA.SetActive(false);
            _dialogueSystem.enabled = false;
        }
    }

    void Interact()
    {
        if(_canRotate)
        {
            transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(player.position - transform.position).eulerAngles.y, 0f);

        }
        if (_animator != null)

        {
            _animator.SetBool("IsTalking", true);
        }
       
        _dialogueSystem.enabled = true;
        _dialogueCanvas.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Interaction déclenchée !");

        // 👉 Mets ton code ici
    }


}