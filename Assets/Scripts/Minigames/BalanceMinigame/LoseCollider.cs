using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coffee"))
        {
            Destroy(collision.gameObject);
            CoffeeMinigameManager.instance.CoffeeFell();
        }
    }
}