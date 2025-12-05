using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private string npcName;
    [SerializeField] private Sprite npcPortrait;

    [Header("Dialogue Lines")]
    [SerializeField] private string[] dialogueLines;

    [Header("NPC Booleans")]
    [SerializeField] private bool canTalk = false;

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.instance != null && !DialogueManager.instance.isDialogueActive)
            {
                DialogueManager.instance.StartDialogue(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canTalk = true;
        Debug.Log("Press E to talk to " + npcName);
        Debug.Log("Can talk: " + canTalk);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canTalk = false;
    }

    public string[] GetDialogueLines()
    {
        return dialogueLines;
    }
    public Sprite GetNPCPortrait()
    {
        return npcPortrait;
    }
    public string GetNPCName()
    {
        return npcName;
    }

}
