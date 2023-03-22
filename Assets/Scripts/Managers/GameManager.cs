using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LogManager logger;
    public ClockManager clocker;
    public ConfigManager cfgManager;
    public AssetManager assetManager;
    public UIManager uiManager;

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

        // // LOAD NEXT SCENE
        // if (activeScene.name == "0_StartUp")
        // {
        //     SceneManager.LoadScene("2_Dungeon");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("enter"))
        // {
        //     cfgManager.SaveConfig();
        //     Debug.Log("Conductor ENTER");
        //     Debug.Log("CONFIG SAVED");
        // }
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
