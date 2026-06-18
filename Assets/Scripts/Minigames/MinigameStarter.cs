using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameStarter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().canMove = false;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }
    }
}