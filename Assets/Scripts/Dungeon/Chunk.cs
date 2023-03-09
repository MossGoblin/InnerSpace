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


    void Start()
    {
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
                    newTile.addr_p = cp;
                    newTile.addr_q = cq;
                    newTile.addr_r = cr;
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
        Debug.Log(tileList);


   }

    void Update()
    {
        
    }
}
