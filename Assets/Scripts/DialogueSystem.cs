using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogues;

    public float typingSpeed = 0.05f;

    private int currentIndex = 0;
    private bool isTyping = false;
    private bool dialogueFinished = false;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Gamepad.current != null &&
      UnityEngine.InputSystem.Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogues[currentIndex];
                isTyping = false;
            }
            else
            {
                NextDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        currentIndex = 0;
        dialogueFinished = false;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogues[currentIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            StartCoroutine(TypeText());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueFinished = true;
        Debug.Log("Dialogue terminé !");
    }

    // 🔥 Pour vérifier depuis un autre script
    public bool IsDialogueFinished()
    {
        return dialogueFinished;
    }
}