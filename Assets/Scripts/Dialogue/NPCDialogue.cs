using UnityEngine;
using UnityEngine.Events;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private string npcName;
    [SerializeField] private Sprite npcPortrait;

    [Header("Dialogue Lines")]
    [SerializeField] private string[] dialogueLines;

    [Header("Configurações do Gatilho")]
    [Tooltip("Se marcado, o diálogo inicia sozinho ao entrar no colisor.")]
    [SerializeField] private bool startOnEnter = false;
    [Tooltip("Garante que o diálogo automático aconteça apenas uma vez.")]
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Events")]
    public UnityEvent onDialogueEnd;

    private bool canTalk = false;
    private bool hasTriggered = false;

    private void Update()
    {
        if (!startOnEnter && canTalk && Input.GetKeyDown(KeyCode.E))
        {
            TryStartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;

            if (startOnEnter)
            {
                if (triggerOnlyOnce && hasTriggered) return;

                TryStartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
        }
    }

    private void TryStartDialogue()
    {
        if (DialogueManager.instance != null && !DialogueManager.instance.isDialogueActive)
        {
            hasTriggered = true;
            DialogueManager.instance.StartDialogue(this);
        }
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