using UnityEngine;


public class Manager : MonoBehaviour
{
    public Logger logger;

    void Start()
    {

        logger = GetComponentInParent<Logger>();


        ConfigData cfg = new ConfigData();
        cfg.chunkSize = 4;
        ConfigManager cfgMgr = new ConfigManager();
        cfgMgr.logger = logger;
        logger.LogInfo($"Config init: {cfgMgr.cfgFilename}", true);
        cfgMgr.config = cfg;
        cfgMgr.SaveConfig();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            Debug.Log("Conductor ENTER");
        }
        
    }
}
