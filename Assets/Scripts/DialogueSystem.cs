using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

[System.Serializable]
public class DialogueSet
{
    public int waterLevel;

    [TextArea(2, 5)]
    public string[] dialogues;
}

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Sets")]
    public DialogueSet[] dialogueSets;

    [Header("Current State")]
    public int _waterLevel;

    public float typingSpeed = 0.05f;

    private string[] currentDialogues;
    private int currentIndex = 0;
    private bool isTyping = false;
    private bool dialogueFinished = false;

    void Start()
    {
        SelectDialogueSet();
        StartDialogue();
    }
    public void SetWaterLevel(int newLevel)
    {
        _waterLevel = newLevel;
        SelectDialogueSet();
    }
    void Update()
    {
        
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentDialogues[currentIndex];
                isTyping = false;
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void SelectDialogueSet()
    {
        foreach (var set in dialogueSets)
        {
            if (set.waterLevel == _waterLevel)
            {
                currentDialogues = set.dialogues;
                return;
            }
        }

        // fallback si aucun trouvé
        if (dialogueSets.Length > 0)
            currentDialogues = dialogueSets[0].dialogues;
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

        foreach (char letter in currentDialogues[currentIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextDialogue()
    {
        currentIndex++;

        if (currentIndex < currentDialogues.Length)
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

    public bool IsDialogueFinished()
    {
        return dialogueFinished;
    }
}