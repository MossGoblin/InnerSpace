using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class Chunk : MonoBehaviour
{
    private GameManager gameManager;
    private ConfigManager cfgManager;
    private AssetManager assetManager;
    private LogManager logger;
    private ClockManager clocker;
    public int chunkRadius;

    // XXX public
    public Dictionary<Address, Tile> tileList;
    private int tileCount;

    [SerializeField]
    public int biomeID;
    public Tile tilePrefab;
    private double tileHeight;
    private double tileSize;
    private double tilePPU;

    public bool isActive = false;
    public bool isActivated = false;

    void Start()
    {
        tilePPU = 100;
        tileHeight = 442;

        tileSize = tileHeight / (4 * tilePPU); // half hight of tile sprite over pixels per unit = 100/100; 'height' is half vertical size (e.g. height of png)

        // get managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        assetManager = gameManager.assetManager;

        chunkRadius = cfgManager.config.chunkSize;

        // Create empty tile dictionary
        // the triangle number equation is (n*(n+1)) / 2, however in this case n = chunkRadius - 1, so for ease the + is substituted for a -
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;

        foreach (Tile tile in tileList.Values)
        {
            AssetManager.TileSprite tileSpriteData = assetManager.GetRandomSprite(tile.biomeID);
            SpriteRenderer tileSpriteRenderer = tile.GetComponentInParent<SpriteRenderer>();
            tileSpriteRenderer.sprite = tileSpriteData.tileSprite;
        }

        if (tileList.Count != tileCount)
        {
            logger.LogError(
                $"Chunk created with {tileList.Count} tiles instead of {tileCount}",
                true
            );
        }

        logger.LogInfo($"Chunk generated with {tileCount} tiles", true);
        foreach (Address coord in tileList.Keys)
        {
            logger.LogDebug($"pqr: {coord}", false);
        }

        if (isActive)
        {
            PlaceTiles();
        }
    }

    void Update()
    {
        if (isActivated && !isActive)
        {
            Activate();
            this.isActive = true;
        }
        else if (!isActivated)
        {
            Deactivate();
            this.isActive = false;
        }
    }

    public Dictionary<Address, Tile> getChunkData()
    {
        return tileList;
    }

    private void PlaceTiles()
    {
        foreach (Address addr in tileList.Keys)
        {
            float offsetVertical = (float)(tileSize * tileList[addr].address.addrR * 3 / 2);
            float offsetHorizontal = (float)(
                tileSize
                * (
                    Math.Sqrt(3) * tileList[addr].address.addrQ
                    + Math.Sqrt(3) / 2 * tileList[addr].address.addrR
                )
            );

            tileList[addr].transform.position =
                transform.position + new Vector3(offsetHorizontal, offsetVertical);
        }
    }

    public string GetData()
    {
        string result = "";
        result += "{";
        foreach (Address address in tileList.Keys)
        {
            result += $"\"{address}\":";
            result += tileList[address].GetData();
        }
        result = result.Remove(result.Length - 1);
        result += "},";

        return result;
    }

    public void SetData(int chunkBiomeID, string chunkData)
    {
        // parse tile data
        string tiles_pattern =
            "(-?\\d+,-?\\d+,-?\\d+)\":({\\s*tileTypeID:\\s*\\d+,\\s*tileID:\\s*\\d+})";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(chunkData);
        biomeID = chunkBiomeID;

        foreach (Match match in matches)
        {
            Tile newTile = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTile.transform.SetParent(this.transform);
            Address newTileAddr = CompileAddress(match.Groups[1].Value);
            newTile.SetData(newTileAddr, biomeID, match.Groups[2].Value);
            if (tileList == null)
            {
                tileList = new Dictionary<Address, Tile>();
            }
            tileList.Add(newTileAddr, newTile);
        }
    }

    private Address CompileAddress(string addressString)
    {
        string[] addressList = addressString.Split(",");
        int[] addressListInt = addressList.Select(c => int.Parse(c)).ToArray();
        return new Address(addressListInt[0], addressListInt[1]);
    }

    public void Activate()
    {
        foreach (Address tileAddr in tileList.Keys)
        {
            tileList[tileAddr].gameObject.SetActive(true);
        }
        isActive = true;
        PlaceTiles();
    }

    public void Deactivate()
    {
        foreach (Address tileAddr in tileList.Keys)
        {
            tileList[tileAddr].gameObject.SetActive(false);
        }
        isActive = false;
    }
}
