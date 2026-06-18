using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectManager : MonoBehaviour
{
    public static CollectManager instance;

    [Header("Configurações do Minigame")]
    [SerializeField] private int scoreToWin = 5;
    [SerializeField] private int startingHealth = 3;

    [Header("Status Atual")]
    [SerializeField] private int score;
    [SerializeField] private int health;

    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = startingHealth;
        score = 0;
    }

    public void AddScore(int amount)
    {
        if (gameEnded) return;

        score += amount;
        Debug.Log("Pegou item! Score: " + score);

        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    public void TakeDamage(int amount)
    {
        if (gameEnded) return;

        health -= amount;
        Debug.Log("Pegou lixo! Vidas restantes: " + health);

        if (health <= 0)
        {
            GameOver();
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        Debug.Log("Você pegou o cartão de acesso!");

        // O que for acontecer quando o jogador vencer, como abrir uma porta ou dar acesso a algo

        Invoke("CloseMinigame", 5f);
    }

    private void GameOver()
    {
        gameEnded = true;
        Debug.Log("Acabaram as vidas! Tente novamente.");
        Invoke("CloseMinigame", 5f);
    }

    private void CloseMinigame()
    {
        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = true;
        }

        SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
    }
}