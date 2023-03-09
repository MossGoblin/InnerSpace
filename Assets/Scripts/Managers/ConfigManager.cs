using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Json;

[Serializable]
public class ConfigData
{
    public int chunkSize;
}

public class ConfigManager : MonoBehaviour
{
    public ConfigData config { get; set; }

    public Dictionary<int[], Tile> dungeonData { get; set; }

    public string cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";

    public string dungeonFilename = Application.dataPath + "/Settings/" + "dungeon.json";

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

        // DontDestroyOnLoad(this.gameObject);
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

    // TESTING WITH CHUNK INSTEAD OF DUNGEON
    public void SaveDungeon()
    {

        Dictionary<string, string> serializableDungeonData = new Dictionary<string, string>();
        foreach (int[] tileKey in dungeonData.Keys)
        {
            string addr = string.Join(",", tileKey);
            Dictionary<string, int> tileData = new Dictionary<string, int>();
            tileData.Add("addr_p", dungeonData[tileKey].addr_p);
            tileData.Add("addr_q", dungeonData[tileKey].addr_q);
            tileData.Add("addr_r", dungeonData[tileKey].addr_r);
            tileData.Add("biome_id", dungeonData[tileKey].biomeID);
            tileData.Add("tile_type_id", dungeonData[tileKey].tileTypeID);
            tileData.Add("tile_id", dungeonData[tileKey].tileID);
            serializableDungeonData.Add(addr, JsonUtility.ToJson(tileData));
        }
        //Convert the dungeonData object to a JSON string
        string json = JsonUtility.ToJson(serializableDungeonData, prettyPrint: true);


        //Write the JSON string to a file on disk.
        File.WriteAllText(dungeonFilename, json);
    }

    public void LoadDungeon()
    {
        //Get the JSON string from the file on disk.
        string savedJson = File.ReadAllText(dungeonFilename);

        //Convert the JSON string back to a ConfigData object.
        dungeonData = JsonUtility.FromJson<Dictionary<int[], Tile>>(savedJson);
    }
}