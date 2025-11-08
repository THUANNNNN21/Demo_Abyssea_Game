using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MyMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    [Header("UI References")]
    [SerializeField] private TimerText timerText;
    [SerializeField] private ScoreText scoreText;
    [SerializeField] private FinalScoreText finalScoreText;
    [SerializeField] private GameObject UIInventory;
    [SerializeField] private GameObject OpenInventoryBtn;
    [SerializeField] private GameObject UIHotKeyBar;
    GameObject UIGameOver;

    [Header("Game Settings")]
    public float matchTime = 60f; // Thời gian chơi (giây)
    [SerializeField] public float currentTime;

    [Header("Score")]
    public int score = 0;

    private bool isGameOver = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTimerText();
        this.LoadScoreText();
        this.LoadFinalScoreText();
        this.LoadGameOverUI();
        this.LoadInventoryUI();
        this.LoadOpenInventoryButton();
        this.LoadHotKeyBar();
    }
    private void LoadTimerText()
    {
        if (timerText != null) return;
        timerText = GameObject.Find("TimerText").GetComponent<TimerText>();
        Debug.LogWarning("Load TimerText: " + timerText.name, gameObject);
    }
    private void LoadScoreText()
    {
        if (scoreText != null) return;
        scoreText = GameObject.Find("ScoreText").GetComponent<ScoreText>();
        Debug.LogWarning("Load ScoreText: " + scoreText.name, gameObject);
    }
    private void LoadFinalScoreText()
    {
        if (finalScoreText != null) return;
        finalScoreText = GameObject.Find("FinalScoreText").GetComponent<FinalScoreText>();
        Debug.LogWarning("Load FinalScoreText: " + finalScoreText.name, gameObject);
    }
    private void LoadGameOverUI()
    {
        if (UIGameOver != null) return;
        UIGameOver = GameObject.Find("UIGameOver");
        Debug.LogWarning("Load UIGameOver: " + UIGameOver.name, gameObject);
    }
    private void LoadInventoryUI()
    {
        if (UIInventory != null) return;
        UIInventory = GameObject.Find("UIInventoryController");
        Debug.LogWarning("Load UIInventory: " + UIInventory.name, gameObject);
    }
    private void LoadOpenInventoryButton()
    {
        if (OpenInventoryBtn != null) return;
        OpenInventoryBtn = GameObject.Find("OpenInventoryBtn");
        Debug.LogWarning("Load OpenInventoryBtn: " + OpenInventoryBtn.name, gameObject);
    }
    private void LoadHotKeyBar()
    {
        if (UIHotKeyBar != null) return;
        UIHotKeyBar = GameObject.Find("UIHotKey");
        Debug.LogWarning("Load UIHotKeyBar: " + UIHotKeyBar.name, gameObject);
    }
    void Start()
    {
        this.currentTime = this.matchTime;
        this.UIGameOver.SetActive(false);
        this.timerText.UpdateTimerUI(this.currentTime);
        this.scoreText.UpdateScoreUI(this.score);

        // Play gameplay background music
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.BackgroundGamePlay, 0.5f);
        }
    }

    void Update()
    {
        if (this.isGameOver) return;

        this.currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            this.currentTime = 0;
            this.EndGame();
        }
        this.timerText.UpdateTimerUI(this.currentTime);
    }

    public void AddScore(int amount)
    {
        this.score += amount;
        this.scoreText.UpdateScoreUI(this.score);
    }
    public void AddTime(float amount)
    {
        this.currentTime += amount;
        this.timerText.UpdateTimerUI(this.currentTime);
    }
    void EndGame()
    {
        this.isGameOver = true;
        this.finalScoreText.UpdateFinalScoreUI(this.score);
        this.ShowGameOverUI();
        Time.timeScale = 0; // Dừng game
    }
    private void ShowGameOverUI()
    {
        this.UIGameOver.SetActive(true);
        this.UIInventory.SetActive(false);
        this.OpenInventoryBtn.SetActive(false);
        this.UIHotKeyBar.SetActive(false);
    }

    // Gọi trong nút UI
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.BackgroundGamePlay, 0.5f);
        }
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.BackgroundMainMenu, 0.5f);
        }
    }
}
