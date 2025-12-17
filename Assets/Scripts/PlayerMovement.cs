using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (DialogueManager.instance.isDialogueActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0)
        {
            movement.y = 0;
        }
        else if (movement.x < 0)
        {
            movement.y = 0;
        }

        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
