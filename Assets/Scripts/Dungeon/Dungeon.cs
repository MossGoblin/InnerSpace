using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<int[], Chunk> chunkList;
    [SerializeField]
    private int[] activeChunkAddress;
    [SerializeField]
    private int[] activeChunk;
    private GameManager gameManager;
    private ConfigManager cfgManager;
    private LogManager logger;
    private ClockManager clocker;

    void Start()
    {
        // get managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        

        chunkList = new Dictionary<int[], Chunk>();

        // Inspector View Test
        activeChunkAddress = new int[] {0, 0, 0};

        // Create a chunk and adopt it
        // TEMP adopt thepre-made chunk
        GameObject chunkGO = GameObject.Find("Chunk");
        Chunk chunk = chunkGO.GetComponent<Chunk>();
        chunkList.Add(new int[] {0, 0, 0}, chunk);
    }

    void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            logger.LogInfo("Saving Dungeon Data");
            cfgManager.SaveDungeon(this);
            logger.LogInfo("Dungeon Saved");
        }
    }

    public string GetData()
    {
        string result = "";
        result += "{";
        foreach (int[] address in chunkList.Keys)
        {
            result += $"\"{Serializer.SerializeAddress(address)}\" : ";
            result += chunkList[address].GetData();
        }
        result = result.Remove(result.Length - 1); // HERE remove last comma
        result += "}";

        return result;
    }
}
