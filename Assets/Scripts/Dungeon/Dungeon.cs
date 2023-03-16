using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<string, Chunk> chunkList;
    [SerializeField]
    private string activeChunkAddress;
    [SerializeField]
    private int[] activeChunk;
    private GameManager gameManager;
    private ConfigManager cfgManager;
    private LogManager logger;
    private ClockManager clocker;


    // XX TEST CHUNK JSON
    private string chunkTestJson;



    void Start()
    {
        // get managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        

        chunkList = new Dictionary<string, Chunk>();

        // Inspector View Test
        activeChunkAddress = Serializer.AddressToString(new int[] {0, 0, 0});

        // Create a chunk and adopt it
        // TEMP adopt thepre-made chunk
        GameObject chunkGO = GameObject.Find("Chunk");
        Chunk chunk = chunkGO.GetComponent<Chunk>();
        chunkList.Add(activeChunkAddress, chunk);



        // XXX TEST CHUNK JSON
        chunkTestJson = "-5,4,1\":{\"biomeID\": 1,\"tileTypeID\": 2,\"tileID\": 3},\"-1,0,1\":{\"biomeID\": 4,\"tileTypeID\": 5,\"tileID\": 6}, ";
    }

    void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            logger.LogInfo("Saving Dungeon Data");
            cfgManager.SaveDungeon(this);
            logger.LogInfo("Dungeon Saved");
            string addr = Serializer.AddressToString(new int[] {0, 0, 0});
            // HERE regex tests
            // chunkList[addr].SetData(chunkTestJson);
            cfgManager.LoadDungeon();
        }
    }

    public string GetData()
    {
        string result = "";
        result += "{";
        foreach (string address in chunkList.Keys)
        {
            result += $"\"{address}\" : ";
            result += chunkList[address].GetData();
        }
        result = result.Remove(result.Length - 1);
        result += "}";

        return result;
    }
}
