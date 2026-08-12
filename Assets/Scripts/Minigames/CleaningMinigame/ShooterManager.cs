using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterManager : MonoBehaviour
{
    public static ShooterManager instance;
    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("Vitória! Todo o equipamento foi limpo. Peça da catraca obtida!");
        Invoke("CloseMinigame", 2f);
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("A poeira atingiu o sistema central! Game Over.");
        Invoke("CloseMinigame", 2f);
    }

    private void CloseMinigame()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
        }
        else
        {
            Debug.LogWarning("Teste isolado: cena não será fechada.");
        }
        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = true;
        }
        if (MinigameCallback.pendingDialogue != null)
        {
            DialogueManager.instance.StartDialogue(MinigameCallback.pendingDialogue);

            MinigameCallback.pendingDialogue = null;
        }
    }
}