using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Chunk : MonoBehaviour
{
    private GameManager gameManager;
    private ConfigManager cfgManager;
    private LogManager logger;
    private ClockManager clocker;
    private Dungeon dungeon;
    public int chunkRadius;
    
    // XXX public
    public Dictionary<string, Tile> tileList;
    private int tileCount;
    private int biomeID;
    public Tile tilePrefab;
    private double tileSize;

    void Start()
    {
        tileSize = 0.5; // half hight of tile sprite over pixels per unit = 100/100

        // get Managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;

        // get parent dungeon
        dungeon = GetComponentInParent<Dungeon>();

        chunkRadius = cfgManager.config.chunkSize;

        // Create empty tile dictionary
        // the triangle number equation is (n*(n+1)) / 2, however in this case n = chunkRadius - 1, so for ease the + is substituted for a -
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;
        tileList = new Dictionary<string, Tile>();

        // Initiate tileList
        // Iterate coordinates ring by ring

        // XXX Deactivate new tile creation
        for (int cp = -chunkRadius + 1; cp <= chunkRadius - 1; cp++)
        {
            for (int cq = chunkRadius - 1; cq >= -chunkRadius + 1; cq--)
            {
                int cr = 0 - cp - cq;
                if (Math.Abs(cr) <= chunkRadius - 1)
                {
                    string coord = Serializer.AddressToString(new int[] { cp, cq, cr });
                    Tile newTile = Instantiate(
                        tilePrefab,
                        new Vector3(0, 0, 0),
                        Quaternion.identity
                    );
                    newTile.addrP = cp;
                    newTile.addrQ = cq;
                    newTile.addrR = cr;
                    newTile.biomeID = biomeID;
                    newTile.transform.SetParent(this.transform);
                    tileList.Add(coord, newTile);
                }
            }
        }

        if (tileList.Count != tileCount)
        {
            logger.LogError(
                $"Chunk created with {tileList.Count} tiles instead of {tileCount}",
                true
            );
        }

        logger.LogInfo($"Chunk generated with {tileCount} tiles", true);
        foreach (string coord in tileList.Keys)
        {
            logger.LogDebug($"pqr: {coord}", false);
        }

        PlaceTiles();
    }

    void Update() { }

    public Dictionary<string, Tile> getChunkData()
    {
        return tileList;
    }

    private void PlaceTiles()
    {
        foreach (string addr in tileList.Keys)
        {
            float offsetVertical = (float)(tileSize * tileList[addr].addrR * 3 / 2);
            float offsetHorizontal = (float)(tileSize * (Math.Sqrt(3) * tileList[addr].addrQ + Math.Sqrt(3) / 2 * tileList[addr].addrR));

            tileList[addr].transform.position = transform.position + new Vector3(offsetHorizontal, offsetVertical);
        }
    }

    public string GetData()
    {
        string result = "";
        result += "{";
        foreach (string address in tileList.Keys)
        {
            result += $"\"{address}\" : ";
            result += tileList[address].GetData();
        }
        result = result.Remove(result.Length - 1);
        result += "},";

        return result;
    }

    public void SetData(string chunkData)
    {
        // parse tile data
        string tiles_pattern = "(-?\\d+,-?\\d+,-?\\d+)\":({\"biomeID\":\\s*\\d+,\\s*\"tileTypeID\":\\s*\\d+,\\s*\"tileID\":\\s*\\d+})";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(chunkData);
        // var match_0 = matches[0];
        // var match_1 = matches[1];
        foreach (Match match in matches)
        {
            Tile newTile = Instantiate(
                tilePrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );
            int[] tileAddr = Serializer.AddressToArray(match.Groups[1].Value);
            newTile.addrP = tileAddr[0];
            newTile.addrQ = tileAddr[1];
            newTile.addrR = tileAddr[2];
            newTile.transform.SetParent(this.transform);
            newTile.SetData(match.Groups[1].Value, match.Groups[2].Value);
            tileList.Add(match.Groups[1].Value, newTile);
        }
        // XXX TEST
        Debug.Log(tileList.Count);
        PlaceTiles();
    }
}
