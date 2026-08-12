using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    [Header("Configurações do Minigame")]
    [SerializeField] private string minigameSceneName;

    [Header("Retorno")]
    [Tooltip("Arraste o NPCDialogue que deve tocar APÓS vencer o minigame")]
    [SerializeField] private NPCDialogue dialogueAfterMinigame;

    public void StartMinigame()
    {
        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = false;
        }

        MinigameCallback.pendingDialogue = dialogueAfterMinigame;

        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
    }
}

public static class MinigameCallback
{
    public static NPCDialogue pendingDialogue;
}