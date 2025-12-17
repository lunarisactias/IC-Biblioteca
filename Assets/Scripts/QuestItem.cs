using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private string questName;
    [SerializeField] private bool opensNewArea = false;
    [SerializeField] private GameObject newAreaDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            QuestManager.instance.currentItemCount++;
            Destroy(gameObject);

            if (QuestManager.instance.currentItemCount == QuestManager.instance.quantityQuestItems[QuestManager.instance.questsCompleted] && 
            questName == QuestManager.instance.currentQuest)
            {
                QuestManager.instance.CompleteCurrentQuest();

                if (opensNewArea && newAreaDoor != null)
                {
                    newAreaDoor.SetActive(false);
                }
            }

        }
    }
}
