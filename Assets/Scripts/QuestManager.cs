using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private string[] questNames = { "Primeiro Andar", "Segundo Andar", "Terceiro Andar", "Quarto Andar"};
    [SerializeField] public int[] quantityQuestItems = { 1, 1, 1, 1 };

    [SerializeField] private string currentQuestField;

    public string currentQuest { get => currentQuestField; private set => currentQuestField = value; }
    [SerializeField] public int currentItemCount = 0;

    [SerializeField] public int questsCompleted { get; private set; } = 0;
    [SerializeField] private int totalQuests = 4;
    [SerializeField] private GameObject questUI;

    public static QuestManager instance;

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

    private void Start()
    {
        if (questNames.Length > 0)
        {
            currentQuest = questNames[0];
        }
    }

    public void CompleteCurrentQuest()
    {
        questsCompleted++;

        if (questsCompleted < questNames.Length)
        {
            currentQuest = questNames[questsCompleted];
        }

        else
        {
            currentQuest = "All quests completed!";
        }
    }
}
