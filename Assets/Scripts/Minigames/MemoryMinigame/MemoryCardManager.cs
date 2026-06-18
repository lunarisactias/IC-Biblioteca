using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGameManager : MonoBehaviour
{
    public static MemoryGameManager instance;

    [Header("Cartas na Cena")]
    [SerializeField] private List<MemoryCard> allCards;

    [Header("Artes das Cartas")]
    [SerializeField] private List<Sprite> cardSprites;

    [Header("Configurações")]
    [SerializeField] private float timeToHide = 1f; 

    private MemoryCard firstCard;
    private MemoryCard secondCard;
    public bool isChecking { get; private set; } = false;
    private int pairsFound = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        List<Sprite> gameSprites = new List<Sprite>();
        gameSprites.AddRange(cardSprites);
        gameSprites.AddRange(cardSprites);

        if (allCards.Count != gameSprites.Count)
        {
            Debug.LogError("O número de cartas na tela não corresponde ao dobro do número de artes!");
            return;
        }

        for (int i = 0; i < gameSprites.Count; i++)
        {
            Sprite temp = gameSprites[i];
            int randomIndex = Random.Range(i, gameSprites.Count);
            gameSprites[i] = gameSprites[randomIndex];
            gameSprites[randomIndex] = temp;
        }

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].Setup(gameSprites[i].GetInstanceID(), gameSprites[i]);
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isChecking = true;

        yield return new WaitForSeconds(timeToHide);

        if (firstCard.cardID == secondCard.cardID)
        {
            pairsFound++;
            Debug.Log("Acertou o par!");

            if (pairsFound == cardSprites.Count)
            {
                WinGame();
            }
        }
        else
        {
            firstCard.Hide();
            secondCard.Hide();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    private void WinGame()
    {
        Debug.Log("Vitória! Jogo da Memória Concluído.");

        // Aqui é o que acontece depois de ganhar, dnv :P

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