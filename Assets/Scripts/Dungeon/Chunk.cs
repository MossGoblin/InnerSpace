using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Dungeon dungeon;
    public Logger logger;
    private int chunkRadius;
    private Dictionary<int[], Tile> tileList;
    private int tileCount;
    private int biomeID;


    void Start()
    {
        chunkRadius = dungeon.chunkRadius;
        // the triangle number equation is (n(n+1)) / 2, however in this case n = - chunkRadius, so for ease the + is substituted for a -
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
                    tileList.Add(coord, new Tile(cp, cq, cr, biomeID));
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
            logger.LogDebug($"pqr: {coord[0]} : {coord[1]} : {coord[2]}", true);
        }
   }

    void Update()
    {
        
    }
}
