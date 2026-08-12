using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NPCDialogue))]
public class AutoDialogueStarter : MonoBehaviour
{
    [Tooltip("Tempo de espera antes do diálogo começar para não ser abrupto")]
    [SerializeField] private float startDelay = 0.5f;

    private void Start()
    {
        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = false;
        }

        StartCoroutine(StartDialogueDelayed());
    }

    private IEnumerator StartDialogueDelayed()
    {
        yield return new WaitForSeconds(startDelay);

        NPCDialogue introDialogue = GetComponent<NPCDialogue>();
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.StartDialogue(introDialogue);
        }
    }
}