using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text timeText;
    public Text surfaceSpeedText;
    public Text carSpeedText;
    public Text currentTimeText;
    public Text fastTimeText;
    public GameObject panel;
    public GameObject targetPanel;
    public Button restartButton;
    public GameObject resultPanel;   // ���� �г�
    public GameObject nextPanel;     // ���� ���� �г�
    public Button overButton;        // Over ��ư


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // ��ư �̺�Ʈ ����
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => GameManager.Instance.GameRestart());

        overButton.onClick.RemoveAllListeners();
        overButton.onClick.AddListener(OpenNextPanel);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� Ȱ��ȭ ���¸� �������Ѽ� SetActive
            bool isActive = targetPanel.activeSelf;
            targetPanel.SetActive(!isActive);
        }
    }

    void OpenNextPanel()
    {
        resultPanel.SetActive(false);  // ���� �г� �ݱ�
        nextPanel.SetActive(true);     // �� �г� ����
    }

    public void UpdateTimeText(string time)
    {
        timeText.text = time;
    }

    public void UpdateSurfaceText(string speed)
    {
        surfaceSpeedText.text = speed;
    }

    public void UpdateCarSpeedText(string speed)
    {
        carSpeedText.text = speed;
    }

    public void UpdateCurrentTimeText(string time)
    {
        currentTimeText.text = time;
    }

    public void UpdateFastTimeText(string time)
    {
        fastTimeText.text = time;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    public void GameRestart()
    {
        GameManager.Instance.GameRestart();
    }
}
