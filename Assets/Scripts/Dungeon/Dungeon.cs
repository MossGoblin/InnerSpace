using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<Address, Chunk> chunkList;
    [SerializeField]
    private Address activeChunkAddress;
    [SerializeField]
    public GameManager gameManager;
    public ConfigManager cfgManager;
    public UIManager uiManager;
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
        uiManager = gameManager.uiManager;

        chunkList = new Dictionary<Address, Chunk>();

        activeChunkAddress = new Address(0, 0);

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

    }

    public string GetData()
    {
        // TODO align with new syntax
        // {chunkaddr:0,0,0}{tileaddr:-5,5,0}{biomeID:0,tileTypeID:0,tileID:0}
        // TODO ADD CURRENT CHUNK AND BASE CHUNK
        string result = "";
        foreach (Address address in chunkList.Keys)
        {
            result += $"{{chunkaddr:{address.ToString()}}}";
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

        chunkList = new Dictionary<Address, Chunk>(); // 0
        string[] dungeonSplit = SplitDungeonString(dungeonData); // 1
        string dungeonString = dungeonSplit[1]; // 1
        Dictionary<Address, string> chunkStrings = SplitDungeonData(dungeonString); // 1
        foreach (Address address in chunkStrings.Keys)
        {
            Address chunkAddr = address;
            Chunk newChunk = Instantiate(chunkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newChunk.SetData(chunkStrings[address]);
            newChunk.transform.SetParent(this.transform);
            chunkList.Add(address, newChunk);
            if (address.Equals(activeChunkAddress))
            {
                newChunk.isActivated = true;
            }
            else
            {
                newChunk.isActivated = false;
            }
        }
    }


    private Dictionary<Address, string> SplitDungeonData(string dungeonData)
    {
        // HERE
        string tiles_pattern = "{chunkaddr:(-?\\d+,-?\\d+,-?\\d+)}(.*)";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(dungeonData);
        Dictionary<Address, string> split = new Dictionary<Address, string>();

        foreach (Match match in matches)
        {
            split.Add(CompileAddress(match.Groups[1].Value), match.Groups[2].Value);
        }

        return split;
    }

    private Address CompileAddress(string addressString)
    {
        string[] addressList = addressString.Split(",");
        int[] addressListInt = addressList.Select(c => int.Parse(c)).ToArray();
        return new Address(addressListInt[0], addressListInt[1]);
    }

    public string[] SplitDungeonString(string dungeonString)
    {
        string[] split = dungeonString.Split("/");

        return split;
    }

    public void SwitchChunk(Address newActiveAddr)
    {
        // TODO validate newActiveAddr
        chunkList[activeChunkAddress].Deactivate();
        activeChunkAddress = newActiveAddr;
        chunkList[activeChunkAddress].Activate();
    }
}
