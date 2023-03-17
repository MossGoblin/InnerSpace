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

        // get parent dungeon and inherit managers
        dungeon = GetComponentInParent<Dungeon>();
        logger = dungeon.logger;
        clocker = dungeon.clocker;
        cfgManager = dungeon.cfgManager;

        chunkRadius = cfgManager.config.chunkSize;


        // XXX Report chunk creation
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;

        if (tileList.Count != tileCount)
        {
            logger.LogError(
                $"Chunk generated with {tileList.Count} tiles instead of {tileCount}",
                true
            );
        }

        logger.LogInfo($"Chunk generated with {tileList.Count} tiles", true);
        foreach (string coord in tileList.Keys)
        {
            logger.LogDebug($"pqr: {coord}", false);
        }

        // XXX
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
        // {tileaddr:-5,5,0}{biomeID:0,tileTypeID:0,tileID:0}
        foreach (string address in tileList.Keys)
        {
            result += $"{{tileaddr:{address}}}{tileList[address].GetData()}";
        }

        return result;
    }

    public void SetData(string chunkData)
    {
        // parse tile data
        if (tileList == null)
        {
            tileList = new Dictionary<string, Tile>();
        }
        string tiles_pattern = "{tileaddr:(-?\\d+,-?\\d+,-?\\d+)}{(biomeID:\\d+,tileTypeID:\\d+,tileID:\\d+)}";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(chunkData);
        foreach (Match match in matches)
        {
            string tileAddrString = match.Groups[1].Value;
            string tileData = match.Groups[2].Value;
            int[] tileAddr = Serializer.AddressToArray(tileAddrString);
            Tile newTile = Instantiate(
                tilePrefab,
                new Vector3(tileAddr[0], tileAddr[1], tileAddr[2]),
                Quaternion.identity
            );
            newTile.addrP = tileAddr[0];
            newTile.addrQ = tileAddr[1];
            newTile.addrR = tileAddr[2];
            newTile.transform.SetParent(this.transform);
            newTile.SetData(tileAddrString, tileData);
            tileList.Add(tileAddrString, newTile);
        }
    }

    public void Activate()
    {
        foreach (string address in tileList.Keys)
        {
            tileList[address].gameObject.SetActive(true);
        }
        // TODO place tiles
    }

    public void Deactivate()
    {
        foreach (string address in tileList.Keys)
        {
            tileList[address].gameObject.SetActive(false);
        }
    }

    public void CreateEmpty()
    {
        // Create empty tile dictionary
        // the triangle number equation is (n*(n+1)) / 2, however in this case n = chunkRadius - 1, so for ease the + is substituted for a -
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;
        tileList = new Dictionary<string, Tile>();

        // Initiate tileList
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

    }
}
