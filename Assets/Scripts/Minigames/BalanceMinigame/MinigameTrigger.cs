using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private string minigameSceneName = "BalanceMinigame";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = false;

            SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);

            gameObject.SetActive(false);
        }
    }
}