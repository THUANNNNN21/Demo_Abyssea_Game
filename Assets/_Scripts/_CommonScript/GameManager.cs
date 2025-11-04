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
    void Start()
    {
        this.currentTime = this.matchTime;
        this.UIGameOver.SetActive(false);
        this.timerText.UpdateTimerUI(this.currentTime);
        this.scoreText.UpdateScoreUI(this.score);
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

    void EndGame()
    {
        this.isGameOver = true;
        this.finalScoreText.UpdateFinalScoreUI(this.score);
        this.UIGameOver.SetActive(true);
        Time.timeScale = 0; // Dừng game
    }

    // Gọi trong nút UI
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
