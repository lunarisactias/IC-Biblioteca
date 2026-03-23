using UnityEngine;

public class TrayController : MonoBehaviour
{
    [Header("Configurações de Força")]
    [SerializeField] private float balanceForce = 1500f;
    private float input;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.AddTorque(-input * balanceForce * Time.fixedDeltaTime);
    }
}