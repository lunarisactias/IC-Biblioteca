using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int achievementsUnlocked = 0;
    [SerializeField] private int totalAchievements = 10;

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
}
