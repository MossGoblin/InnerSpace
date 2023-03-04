using System;
using System.IO;
using UnityEngine;

[Serializable]
public class ConfigData
{
    public int chunkSize;
}

public class ConfigManager
{
    public ConfigData config { get; set; }
    public Logger logger { get; set; }
    public string cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";

    public void SaveConfig()
    {
        //Convert the ConfigData object to a JSON string.
        string json = JsonUtility.ToJson(config);

        //Write the JSON string to a file on disk.
        File.WriteAllText(cfgFilename, json);
        logger.LogInfo($"Config saved", true);

    }

    public void LoadConfig()
    {
        //Get the JSON string from the file on disk.
        string savedJson = File.ReadAllText(cfgFilename);

        //Convert the JSON string back to a ConfigData object.
        config = JsonUtility.FromJson<ConfigData>(savedJson);
        logger.LogInfo($"Config loaded", true);
    }
}