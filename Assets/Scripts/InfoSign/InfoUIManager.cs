using UnityEngine;
using TMPro;

public class InfoUIManager : MonoBehaviour
{
    public static InfoUIManager instance;

    [Header("Elementos de UI")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;

    public bool isInfoOpen { get; private set; } = false;
    private bool canClose = false; 

    private void Awake()
    {
        instance = this;
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInfoOpen && canClose && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseInfo();
        }
    }

    public void OpenInfo(InfoSignData data)
    {
        isInfoOpen = true;
        titleText.text = data.signTitle;
        contentText.text = data.signText;
        infoPanel.SetActive(true);

        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = false;
        }

        Invoke("EnableClose", 0.1f);
    }

    private void EnableClose()
    {
        canClose = true;
    }

    public void CloseInfo()
    {
        isInfoOpen = false;
        canClose = false; 
        infoPanel.SetActive(false);

        if (Player.instance != null)
        {
            Player.instance.GetComponent<PlayerMovement>().canMove = true;
        }
    }
}