using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.OpenCloseInventory();
            gameObject.GetComponent<PlayerMovement>().canMove = !GameManager.instance.inventory.activeSelf;
        }
    }
}
