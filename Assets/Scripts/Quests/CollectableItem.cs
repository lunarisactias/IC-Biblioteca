using UnityEngine;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Image inventorySprite;
    public void OnCollect()
    {
        GameManager.instance.collectablesUnlocked++;
        if (GameManager.instance.collectablesUnlocked >= GameManager.instance.totalCollectables)
        {
            Debug.Log("All collectables unlocked!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.collectablesUnlocked++;

            if (inventorySprite != null && itemSprite != null)
            {
                inventorySprite.sprite = itemSprite;
                inventorySprite.enabled = true;
            }
              
            Destroy(gameObject);
        }
    }
}
