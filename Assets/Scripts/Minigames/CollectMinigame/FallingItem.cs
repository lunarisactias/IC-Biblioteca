using UnityEngine;

public class FallingItem : MonoBehaviour
{
    [Header("Configurações do Item")]
    [SerializeField] private bool isGoodItem;
    [SerializeField] private int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isGoodItem)
            {
                CollectManager.instance.AddScore(value);
            }
            else
            {
                CollectManager.instance.TakeDamage(value);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}