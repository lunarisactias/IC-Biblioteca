using UnityEngine;

public class InfoSign : MonoBehaviour
{
    [Header("Dados da Informação")]
    [SerializeField] private InfoSignData signData;

    [Header("Visual")]
    [Tooltip("Um pequeno ícone (ex: um balão com 'E') que aparece em cima da placa")]
    [SerializeField] private GameObject interactionPrompt;

    private bool canInteract = false;

    private void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("1. O jogador apertou E perto da placa!");

            if (DialogueManager.instance != null && DialogueManager.instance.isDialogueActive)
            {
                Debug.Log("2. ALERTA: O jogo achou que há um diálogo ativo e cancelou a placa.");
                return;
            }

            if (InfoUIManager.instance != null && InfoUIManager.instance.isInfoOpen)
            {
                Debug.Log("3. ALERTA: O jogo achou que a placa já está aberta e cancelou.");
                return;
            }

            Debug.Log("4. Tudo certo! Mandou o InfoUIManager abrir.");
            InfoUIManager.instance.OpenInfo(signData);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            if (interactionPrompt != null) interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
        }
    }
}