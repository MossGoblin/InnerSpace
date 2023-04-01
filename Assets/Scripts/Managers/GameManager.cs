using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LogManager logger;
    public ClockManager clocker;
    public ConfigManager cfgManager;
    public AssetManager assetManager;

    private Scene activeScene;
    public static GameManager gmInstance { get; private set; }

    void Awake()
    {
        if (gmInstance != null && gmInstance != this)
        {
            Destroy(this);
        }
        else
        {
            gmInstance = this;
        }

        logger = GetComponentInChildren<LogManager>();
        clocker = GetComponentInChildren<ClockManager>();
        cfgManager = GetComponentInChildren<ConfigManager>();

        logger.LogInfo($"GameManager: ON", true);
        logger.LogInfo($"Logger: ON", true);
        logger.LogInfo($"Config Manager: ON", true);
        logger.LogInfo($"Clocker: ON", true);

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        ConfigData cfg = new ConfigData();
        logger.LogInfo($"Config init: {cfgManager.cfgFilename}", true);
        cfgManager.LoadConfig();
        logger.LogInfo($"Config loaded", true);
        logger.LogInfo(cfgManager.config.ToString());
        logger.LogInfo($"Chunk size: {cfg.chunkSize}");
    }

    void Update()
    {

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("2_Dungeon");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
