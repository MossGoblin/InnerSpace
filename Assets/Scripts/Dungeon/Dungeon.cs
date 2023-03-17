using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<string, Chunk> chunkList;
    [SerializeField]
    private string activeChunkAddress;
    [SerializeField]
    public GameManager gameManager;
    public ConfigManager cfgManager;
    public LogManager logger;
    public ClockManager clocker;

    public Chunk chunkPrefab;


    void Start()
    {
        // get managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        

        chunkList = new Dictionary<string, Chunk>();

        activeChunkAddress = Serializer.AddressToString(new int[] {0, 0, 0});

        // Test auto-load
        string dungeonData = cfgManager.LoadDungeon();
        SetData(dungeonData);

        // Create a chunk and adopt it
        // TEMP adopt thepre-made chunk
        // GameObject chunkGO = GameObject.Find("Chunk");
        // Chunk chunk = chunkGO.GetComponent<Chunk>();
        // chunkList.Add(activeChunkAddress, chunk);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown("enter"))
        {
            // logger.LogInfo("Saving Dungeon Data");
            // cfgManager.SaveDungeon(this);
            // logger.LogInfo("Dungeon Saved");
            // string addr = Serializer.AddressToString(new int[] {0, 0, 0});

            // HERE regex tests
            string dungeonData = cfgManager.LoadDungeon();
            SetData(dungeonData);
        }
        */
    }

    public string GetData()
    {
        // TODO align with new syntax
        // {chunkaddr:0,0,0}{tileaddr:-5,5,0}{biomeID:0,tileTypeID:0,tileID:0}
        // TODO ADD CURRENT CHUNK AND BASE CHUNK
        string result = "";
        foreach (string address in chunkList.Keys)
        {
            result += $"{{chunkaddr:{address}}}";
            result += chunkList[address].GetData();
        }

        return result;
    }

    public void SetData(string dungeonData)
    {
        // TODO
        /*
        0. Reset chunkList
        1. Parse dungeonData
        2. Create chunks and fill in chunkList
        3. Activate current chunk
        */

        chunkList = new Dictionary<string, Chunk>(); // 0
        string[] dungeonSplit = SplitDungeonString(dungeonData); // 1
        string dungeonString = dungeonSplit[1]; // 1
        Dictionary<string, string> chunkStrings = SplitDungeonData(dungeonString); // 1
        foreach (string address in chunkStrings.Keys)
        {
            int[] chunkAddr = Serializer.AddressToArray(address);
            Chunk newChunk = Instantiate(chunkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newChunk.SetData(chunkStrings[address]);
            newChunk.transform.SetParent(this.transform);
            chunkList.Add(address, newChunk);
            if (address != activeChunkAddress)
            {
                newChunk.Deactivate();
            }
        }
    }


    public Dictionary<string, string> SplitDungeonData(string dungeonData)
    {
        // HERE
        string tiles_pattern = "{chunkaddr:(-?\\d+,-?\\d+,-?\\d+)}(.*)";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(dungeonData);
        Dictionary<string, string> split = new Dictionary<string, string>();

        foreach (Match match in matches)
        {
            split.Add(match.Groups[1].Value, match.Groups[2].Value);
        }

        return split;
    }

    public string[] SplitDungeonString(string dungeonString)
    {
        string[] split = dungeonString.Split("/");

        return split;
    }

    public void SwitchChunk(string addr)
    {
        // TODO
        // deactivate current chunk
        // set addr as the address of the current chunk
        // activate current chunk
    }
}
