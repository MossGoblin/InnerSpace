using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class Dungeon : MonoBehaviour
{
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

        chunkList = new Dictionary<Address, Chunk>();

        activeChunkAddress = new Address(-1, 1);

        // Test auto-load
        string dungeonData = cfgManager.LoadDungeon();
        SetData(dungeonData);

        // XXX
        Debug.Log($"DungeonUI Persistence is: {cfgManager.config.persistentDungeonUI}");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            logger.LogDebug("Save Dungeon", true);
            cfgManager.SaveDungeon(this);
        }
    }

    public string GetData()
    {
        string result = "";
        result += $"{{activeChunkAddress:{activeChunkAddress.addrP},{activeChunkAddress.addrQ}}}";
        foreach (Address address in chunkList.Keys)
        {
            result +=
                $"{{chunkaddr:{address.ToString()},chunkBiomeID:{chunkList[address].biomeID},chunkDecay:{chunkList[address].decay.ToString("0.00")}}}";
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
        // HERE Dungeon META in dungeonSplit[0]
        activeChunkAddress = ParseActiveChunkAddress(dungeonSplit[0]);
        string[] dungeonStrings = dungeonSplit.Where((item, index) => index != 0).ToArray(); // 1
        Dictionary<Address, List<string>> chunkStrings = SplitDungeonData(dungeonStrings); // 1
        foreach (Address address in chunkStrings.Keys)
        {
            Address chunkAddr = address;
            Chunk newChunk = Instantiate(chunkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // HERE
            int chunkBiomeID = Int32.Parse(chunkStrings[address][0]);
            float decayLevel = (float)Double.Parse(chunkStrings[address][1]);
            newChunk.SetData(chunkBiomeID, decayLevel, chunkStrings[address][2]);
            newChunk.transform.SetParent(this.transform);
            chunkList.Add(address, newChunk);
            if (address.Equals(activeChunkAddress))
            {
                newChunk.isActivated = true;
                logger.LogDebug($"Activating chunk {address.ToString()}", true);
            }
            else
            {
                newChunk.isActivated = false;
            }
        }
    }

    private Dictionary<Address, List<string>> SplitDungeonData(string[] dungeonData)
    {
        // HERE
        Dictionary<Address, List<string>> split = new Dictionary<Address, List<string>>();
        foreach (string dungeonString in dungeonData)
        {
            string tiles_pattern =
                "{chunkaddr:(-?\\d+,-?\\d+,-?\\d+),chunkBiomeID:(\\d+),chunkDecay:(\\d+\\.\\d+)}(.*)";
            Regex rg = new Regex(tiles_pattern);
            MatchCollection matches = rg.Matches(dungeonString);

            foreach (Match match in matches)
            {
                split.Add(
                    CompileAddress(match.Groups[1].Value),
                    new List<string>
                    {
                        match.Groups[2].Value,
                        match.Groups[3].Value,
                        match.Groups[4].Value
                    }
                );
            }
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
        string[] split = dungeonString.Split("\r\n");

        return split;
    }

    public void SwitchChunk(Address newActiveAddr)
    {
        // TODO validate newActiveAddr
        chunkList[activeChunkAddress].Deactivate();
        activeChunkAddress = newActiveAddr;
        chunkList[activeChunkAddress].Activate();
    }

    public void DeactivateCurrentChunk()
    {
        chunkList[activeChunkAddress].isActivated = false;
    }

    public void ActivateCurrentChunk()
    {
        chunkList[activeChunkAddress].isActivated = true;
    }

    public Dictionary<Address, Chunk> GetChunkList()
    {
        return chunkList;
    }

    private Address ParseActiveChunkAddress(string dungeonMeta)
    {
        string text_pattern = "{activeChunkAddress:(-?\\d+,-?\\d+)}";
        Regex rg = new Regex(text_pattern);
        MatchCollection matches = rg.Matches(dungeonMeta);
        Address activeChunkAddress = CompileAddress(matches[0].Groups[1].Value);
        return activeChunkAddress;
    }

    public Address GetActiveChunkAddress()
    {
        return activeChunkAddress;
    }
}
