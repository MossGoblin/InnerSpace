using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ConfigData
{
    public int chunkSize;
    public int rndSeed;
}

public class ConfigManager : MonoBehaviour
{
    public GameManager gameManager;
    public LogManager logger;
    public ClockManager clocker;

    public ConfigData config { get; set; }

    public Dictionary<string, Tile> dungeonData { get; set; }

    public string cfgFilename;
    public string dungeonFilename;

    public static ConfigManager cfgInstance { get; private set; }

    void Awake()
    {
        if (cfgInstance != null && cfgInstance != this)
        {
            Destroy(this);
        }
        else
        {
            cfgInstance = this;
        }

        cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";
        dungeonFilename = Application.dataPath + "/Settings/" + "dungeon.json";
    }

    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        logger = gameManager.logger;
        clocker = gameManager.clocker;
    }
    public void SaveConfig()
    {
        //Convert the ConfigData object to a JSON string.
        string json = JsonUtility.ToJson(config);

        //Write the JSON string to a file on disk.
        File.WriteAllText(cfgFilename, json);
    }

    public void LoadConfig()
    {
        //Get the JSON string from the file on disk.
        string savedJson = File.ReadAllText(cfgFilename);

        //Convert the JSON string back to a ConfigData object.
        config = JsonUtility.FromJson<ConfigData>(savedJson);
    }

    public void SaveDungeon(Dungeon dungeon)
    {
        string dungeonString = dungeon.GetData();
        logger.LogDebug("Dungeon Data Serialized:", true);
        logger.LogDebug(dungeonString, true);

        if (System.IO.File.Exists(dungeonFilename) == true)
        {
            System.IO.File.Delete(dungeonFilename);
            logger.LogDebug("Old Dungeon Data Deleted", true);
        }

        System.IO.File.WriteAllText(dungeonFilename, dungeonString);
        logger.LogDebug("Dungeon Data Saved", true);
    }

    public string LoadDungeon()
    {
        string loadedData = File.ReadAllText(dungeonFilename);
        Debug.Log(loadedData); // DBG
        return loadedData;
    }
}
