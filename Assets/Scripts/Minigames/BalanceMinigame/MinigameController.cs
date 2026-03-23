using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public void FinishMinigame()
    {
        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = true;
        }

        SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
    }
}