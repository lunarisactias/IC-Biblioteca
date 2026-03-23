using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoffeeMinigameManager : MonoBehaviour
{
    public static CoffeeMinigameManager instance;

    [Header("Configurações")]
    [SerializeField] private float timeToWin = 15f;
    [SerializeField] private TextMeshProUGUI timerText;

    private int coffeeCount;
    private bool gameEnded = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coffeeCount = GameObject.FindGameObjectsWithTag("Coffee").Length;
    }

    private void Update()
    {
        if (gameEnded) return;

        timeToWin -= Time.deltaTime;
        timerText.text = "Tempo: " + Mathf.CeilToInt(timeToWin).ToString() + "s";

        if (timeToWin <= 0)
        {
            WinGame();
        }
    }

    public void CoffeeFell()
    {
        coffeeCount--;

        if (coffeeCount <= 0 && !gameEnded)
        {
            GameOver();
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        timerText.text = "Sobreviveu!";
        Debug.Log("Vitória!");
        Invoke("CloseMinigame", 2f);
    }

    private void GameOver()
    {
        gameEnded = true;
        timerText.text = "Derrubou todos!";
        Debug.Log("Game Over!");
        Invoke("CloseMinigame", 2f);
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