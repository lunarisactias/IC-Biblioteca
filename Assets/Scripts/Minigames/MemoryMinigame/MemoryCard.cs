using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{
    [HideInInspector] public int cardID; // Para saber quem é o par de quem

    [Header("Referências Visuais")]
    [SerializeField] private Image cardImage; 
    [SerializeField] private Sprite cardBack; 
    private Sprite cardFront;

    private bool isRevealed = false;

    public void Setup(int id, Sprite front)
    {
        cardID = id;
        cardFront = front;
        cardImage.sprite = cardBack; 
    }

    public void OnCardClicked()
    {
        if (isRevealed || MemoryGameManager.instance.isChecking) return;

        Reveal();
        MemoryGameManager.instance.CardRevealed(this);
    }

    public void Reveal()
    {
        isRevealed = true;
        cardImage.sprite = cardFront;
    }

    public void Hide()
    {
        isRevealed = false;
        cardImage.sprite = cardBack;
    }
}