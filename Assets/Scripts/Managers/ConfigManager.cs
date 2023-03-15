using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


[Serializable]
public class ConfigData
{
    public int chunkSize;
}

public class ConfigManager : MonoBehaviour
{
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
        // Find dungeon
        // GameObject dungeonObject = GameObject.Find("Dungeon");
        // Dungeon dungeon = dungeonObject.GetComponent<Dungeon>();
        
        string dungeonString = dungeon.GetData();
        Debug.Log("Dungeon Data Serialized:");
        Debug.Log(dungeonString);

        if (System.IO.File.Exists(dungeonFilename) == true) 
        {
            System.IO.File.Delete(dungeonFilename);
            Debug.Log("Old Dungeon Data Deleted");

        }

        System.IO.File.WriteAllText(dungeonFilename, dungeonString);
        Debug.Log("Dungeon Data Saved");
    }

    public void LoadDungeon()
    {
        //Get the JSON string from the file on disk.
        string savedJson = File.ReadAllText(dungeonFilename);

        //Convert the JSON string back to a ConfigData object.
        dungeonData = JsonUtility.FromJson<Dictionary<string, Tile>>(savedJson);
    }
}