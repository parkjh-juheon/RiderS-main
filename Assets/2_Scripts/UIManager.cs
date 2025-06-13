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
    public GameObject resultPanel;   // 기존 패널
    public GameObject nextPanel;     // 새로 열릴 패널
    public Button overButton;        // Over 버튼


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
        // 버튼 이벤트 연결
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => GameManager.Instance.GameRestart());

        overButton.onClick.RemoveAllListeners();
        overButton.onClick.AddListener(OpenNextPanel);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 현재 활성화 상태를 반전시켜서 SetActive
            bool isActive = targetPanel.activeSelf;
            targetPanel.SetActive(!isActive);
        }
    }

    void OpenNextPanel()
    {
        resultPanel.SetActive(false);  // 기존 패널 닫기
        nextPanel.SetActive(true);     // 새 패널 열기
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
