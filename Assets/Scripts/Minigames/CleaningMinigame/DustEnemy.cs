using UnityEngine;

public class DustEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DeathZone"))
        {
            ShooterManager.instance.GameOver();
        }
    }
}