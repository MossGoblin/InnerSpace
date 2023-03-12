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

    public Dictionary<int[], Tile> dungeonData { get; set; }

    // public string cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";
    // public string dungeonFilename = Application.dataPath + "/Settings/" + "dungeon.json";
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

        // DontDestroyOnLoad(this.gameObject);
        cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";
        dungeonFilename = Application.dataPath + "/Settings/" + "dungeon.json";
        Debug.Log($"App.DataPath: {Application.dataPath}");

    }

    void Start()
    {
        // cfgFilename = Application.dataPath + "/Settings/" + "innser_space_cfg.json";
        // dungeonFilename = Application.dataPath + "/Settings/" + "dungeon.json";
        // Debug.Log($"App.DataPath: {Application.dataPath}");

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



    private static ChunkData compileDungeonData(Dictionary<int[], Tile> dungeonData)
    {
        // HERE
        ChunkData chunkData = new ChunkData();
        string chunkDataString = "";
        foreach (int[] addr in dungeonData.Keys)
        {
            // Tile tile = dungeonData[addr];
            // TileData tileData = new TileData();
            List<string> chunkDataList = new List<string>();
            chunkDataList.Add(String.Join(",", addr));
            chunkDataList.Add(String.Join(",", dungeonData[addr].addrP));
            chunkDataList.Add(String.Join(",", dungeonData[addr].addrQ));
            chunkDataList.Add(String.Join(",", dungeonData[addr].biomeID));
            chunkDataList.Add(String.Join(",", dungeonData[addr].tileTypeID));
            chunkDataList.Add(String.Join(",", dungeonData[addr].tileID));
            chunkDataString += String.Join(";", chunkDataList);
            chunkDataString += "/";
        //     public string biomeID;
        //     public string tileTypeID;
        //     public string tileID;            
        }
        chunkData.tileData = chunkDataString;
        return chunkData;
    }

    public void SaveDungeon()
    {

        ChunkData chunkData = compileDungeonData(dungeonData);

        string jsonFormattedContent = Newtonsoft.Json.JsonConvert.SerializeObject(chunkData);
        Debug.Log("Chunk Data Serialized:");
        Debug.Log(jsonFormattedContent);

        if (System.IO.File.Exists(dungeonFilename) == true) 
        {
            System.IO.File.Delete(dungeonFilename);
            Debug.Log("Old Chunk Data Deleted");

        }
        System.IO.File.WriteAllText(dungeonFilename, jsonFormattedContent);
        Debug.Log("Chunk Data Saved");
    }

    public void LoadDungeon()
    {
        //Get the JSON string from the file on disk.
        string savedJson = File.ReadAllText(dungeonFilename);

        //Convert the JSON string back to a ConfigData object.
        dungeonData = JsonUtility.FromJson<Dictionary<int[], Tile>>(savedJson);
    }
}

public class ChunkData
{
    public string tileData;
}

// public class TileData
// {
//     public string chunkAddress;
//     public string addrP;
//     public string addrQ;
//     public string biomeID;
//     public string tileTypeID;
//     public string tileID;
// }