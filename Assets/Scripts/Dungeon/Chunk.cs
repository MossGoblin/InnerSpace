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
    [SerializeField]
    public Dictionary<Address, Tile> tileList;
    private int tileCount;

    [SerializeField]
    public int biomeID;
    public Tile tilePrefab;
    private double tileSize;
    private double tileHeight = 442;
    private double tilePPU = 100;

    public bool isActive = false;
    public bool isActivated = false;
    public float decay;

    void Start()
    {
        SetTileSize();

        // get managers
        LoadManagers();
        chunkRadius = cfgManager.config.chunkSize;

        // Create empty tile dictionary
        // the triangle number equation is (n*(n+1)) / 2, however in this case n = chunkRadius - 1, so for ease the + is substituted for a -
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;

        // TMP null check is fix for the dungeon generator

        if (tileList == null)
        {
            Debug.Log($"HIT NULL CHECK : CHUNK START()");
            return;
        }

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

        logger.LogInfo($"Chunk generated with {tileList.Count} tiles", true);
        foreach (Address coord in tileList.Keys)
        {
            logger.LogDebug($"pqr: {coord}", false);
        }
    }

    private void LoadManagers()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        assetManager = gameManager.assetManager;
    }

    private void SetTileSize()
    {
        tileSize = tileHeight / (4 * tilePPU); // half hight of tile sprite over pixels per unit = 100/100; 'height' is half vertical size (e.g. height of png)
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

            tileList[addr].transform.position = transform.position + new Vector3(offsetHorizontal, offsetVertical);
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

    public void SetData(int chunkBiomeID, float decayLevel, string chunkData)
    {
        if (tileList == null)
        {
            tileList = new Dictionary<Address, Tile>();
        }

        // parse tile data
        string tiles_pattern =
            "(-?\\d+,-?\\d+,-?\\d+)\":({\\s*tileTypeID:\\s*\\d+,\\s*tileID:\\s*\\d+})";
        Regex rg = new Regex(tiles_pattern);
        MatchCollection matches = rg.Matches(chunkData);
        biomeID = chunkBiomeID;
        decay = decayLevel;

        foreach (Match match in matches)
        {
            Tile newTile = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTile.transform.SetParent(this.transform);
            Address newTileAddr = CompileAddress(match.Groups[1].Value);
            newTile.SetData(newTileAddr, biomeID, match.Groups[2].Value);
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
        // TMP fix for the dungeon generator
        if (tileList == null || tileList.Keys.Count == 0)
        {
            return;
        }
        foreach (Address tileAddr in tileList.Keys)
        {
            tileList[tileAddr].gameObject.SetActive(false);
        }
        isActive = false;
    }

    public void GenerateRandomTiles()
    {
        LoadManagers();
        cfgManager.LoadConfig();
        chunkRadius = cfgManager.config.chunkSize;

        
        tileList = new Dictionary<Address, Tile>();


        // Initiate tileList
        for (int cp = -chunkRadius + 1; cp <= chunkRadius - 1; cp++)
        {
            for (int cq = chunkRadius - 1; cq >= -chunkRadius + 1; cq--)
            {
                int cr = 0 - cp - cq;
                if (Math.Abs(cr) <= chunkRadius - 1)
                {
                    Address coord = new Address(cp, cq);
                    Tile newTile = Instantiate(
                        tilePrefab,
                        new Vector3(0, 0, 0),
                        Quaternion.identity
                    );
                    newTile.address.addrP = cp;
                    newTile.address.addrQ = cq;
                    newTile.address.addrR = cr;
                    newTile.biomeID = biomeID;
                    newTile.transform.SetParent(this.transform);
                    // XXX
                    AssetManager.TileSprite newTileSpriteData = assetManager.GetRandomSprite(newTile.biomeID);
                    newTile.tileTypeID = newTile.biomeID;
                    newTile.tileID = newTileSpriteData.tileSpriteIndex;
                    tileList.Add(coord, newTile);
                }
            }
        }
    }
}
