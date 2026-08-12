using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float xLimit = 8f;

    [Header("Tiro")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + Vector2.right * horizontalInput * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);

        rb.MovePosition(newPosition);
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Faltou colocar o Prefab do Tiro ou o FirePoint no script!");
        }
    }
}