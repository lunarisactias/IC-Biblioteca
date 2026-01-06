using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Inventory Info")]
    [SerializeField] public GameObject inventory;

    [SerializeField] public int collectablesUnlocked = 0;
    [SerializeField] public int totalCollectables { get; private set; } = 6;


    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenCloseInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
