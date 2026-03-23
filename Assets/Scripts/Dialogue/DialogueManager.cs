using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI Elements")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image npcPortrait;
    [SerializeField] private TextMeshProUGUI npcName;

    [Header("Dialogue Lines")]
    [SerializeField] private string[] dialogueLines = null;
    [SerializeField] private float typingSpeed = 0.02f;
    private int currentLineIndex = 0;

    [Header("Dialogue Booleans")]
    [SerializeField] public bool isDialogueActive { get; private set; } = false;
    [SerializeField] private bool isTyping = false;

    private NPCDialogue currentNPC;

    public static DialogueManager instance;
    private void Awake()
    {
        instance = this;

        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialogueActive)
        {
            if (!isTyping)
            {
                ShowNextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLineIndex];
                isTyping = false;
            }
        }
    }
    public void StartDialogue(NPCDialogue npcDialogue)
    {
        isDialogueActive = true;
        currentNPC = npcDialogue;

        npcPortrait.sprite = npcDialogue.GetNPCPortrait();
        dialogueLines = npcDialogue.GetDialogueLines();
        npcName.text = npcDialogue.GetNPCName();

        currentLineIndex = 0;
        dialogueUI.SetActive(true);
        StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));

        if (dialogueLines.Length == 0) return;
    }
    public void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            Debug.Log(dialogueLines[currentLineIndex]);
            currentLineIndex++;
            StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
        }
        else
        {
            EndDialogue();
        }
    }
    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        Debug.Log("Dialogue ended.");
        currentLineIndex = 0;
        dialogueLines = null;
        isDialogueActive = false;

        if (currentNPC != null)
        {
            currentNPC.onDialogueEnd?.Invoke();
            currentNPC = null;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
