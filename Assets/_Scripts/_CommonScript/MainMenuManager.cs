using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MyMonoBehaviour
{
    private static MainMenuManager instance;
    public static MainMenuManager Instance => instance;
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
    void Start()
    {
        // Play main menu background music
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.BackgroundMainMenu, 0.5f);
        }
        howToPlayPanel.SetActive(false);
    }
    [SerializeField] private GameObject howToPlayPanel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHowToPlayPanel();
    }
    private void LoadHowToPlayPanel()
    {
        if (howToPlayPanel != null) return;
        this.howToPlayPanel = GameObject.Find("HowToPlayPanel");
        Debug.LogWarning($"Load HowToPlayPanel in {gameObject.name}.");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.LogWarning("Quit Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void ShowHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }
}
