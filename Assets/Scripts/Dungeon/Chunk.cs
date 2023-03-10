using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private GameManager gameManager;
    private ConfigManager cfgManager;
    private LogManager logger;
    private ClockManager clocker;
    private Dungeon dungeon;
    public int chunkRadius;
    private Dictionary<int[], Tile> tileList;
    private int tileCount;
    private int biomeID;
    public Tile tilePrefab;

    // HERE
    private int tileSize;


    void Start()
    {

        tileSize = 250;

        // get Managers
        gameManager = FindObjectOfType<GameManager>();
        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;

        // get parent dungeon
        dungeon = GetComponentInParent<Dungeon>();

        chunkRadius = cfgManager.config.chunkSize;

        // Create empty tile dictionary
        // the triangle number equation is (n*(n+1)) / 2, however in this case n = chunkRadius - 1, so for ease the + is substituted for a -
        tileCount = ((chunkRadius * (chunkRadius - 1)) / 2) * 6 + 1;
        tileList = new Dictionary<int[], Tile>();

        // Initiate tileList
        // Iterate coordinates ring by ring

        for (int cp = -chunkRadius + 1; cp <= chunkRadius - 1; cp++)
        {
            for (int cq = chunkRadius - 1; cq >= -chunkRadius + 1; cq--)
            {
                int cr = 0 - cp - cq;
                if (Math.Abs(cr) <= chunkRadius - 1)
                {
                    int[] coord = new int[] { cp, cq, cr };
                    Tile newTile = Instantiate(tilePrefab, new Vector3 (0,0,0), Quaternion.identity);
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
            logger.LogError($"Chunk created with {tileList.Count} tiles instead of {tileCount}", true);
        }

        logger.LogInfo($"Chunk generated with {tileCount} tiles", true);
        foreach (int[] coord in tileList.Keys)
        {
            logger.LogDebug($"pqr: {coord[0]} : {coord[1]} : {coord[2]}", false);
        }

        // TEST SAVE CHUNK LIST
        cfgManager.dungeonData = tileList;
        cfgManager.SaveDungeon();

        PlaceTiles();

   }

    void Update()
    {
        
    }

    private void PlaceTiles()
    {
        // XXX iterate tiles
        foreach (int[] addr in tileList.Keys)
        {
            // XXX calculate position based on address
            // width = height = 0.768
            // R == vertical
            // delta hight = 3/4 * height
            // P == horizontal
            // delta width = width

            // HERE
            // SCALE (MAGIC NUMBER)
            float posVertical = (float)(0.768 * (tileList[addr].addrR * 3/4));
            float posHorizontal = (float)(0.768 * (Math.Sqrt(3) * tileList[addr].addrQ / 2 + Math.Sqrt(3) / 4 * tileList[addr].addrR));

            // float posVertical = (float)(tileList[addr].addrR * (3 / 2));
            // float posHorizontal = (float)(tileList[addr].addrP * (Math.Sqrt(3)));

            tileList[addr].transform.position = transform.position + new Vector3(posHorizontal, posVertical);
            // SCALING
        }
    }
}
