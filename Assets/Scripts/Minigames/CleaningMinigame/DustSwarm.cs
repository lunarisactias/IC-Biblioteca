using UnityEngine;

public class DustSwarm : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float dropDistance = 0.5f;

    [Header("Limites da Tela")]
    [SerializeField] private float leftLimit = -7f;
    [SerializeField] private float rightLimit = 7f;

    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        bool edgeReached = false;
        int activeDustCount = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy && child.CompareTag("Enemy"))
            {
                activeDustCount++; 

                if (child.position.x >= rightLimit && direction > 0)
                {
                    edgeReached = true;
                }
                else if (child.position.x <= leftLimit && direction < 0)
                {
                    edgeReached = true;
                }
            }
        }

        if (edgeReached)
        {
            direction *= -1;
            transform.Translate(Vector2.down * dropDistance);
        }

        if (activeDustCount == 0)
        {
            ShooterManager.instance.WinGame();
            Destroy(this);
        }
    }
}