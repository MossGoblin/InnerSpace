using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DungeonMap : MonoBehaviour
{
    public Tile mapChunkPrefab;
    public Dungeon dungeon;
    private Dictionary<Address, Chunk> chunkList;
    private double tileSize;

    void Awake()
    {
        chunkList = dungeon.GetChunkList();
        foreach (Address chunkAddress in chunkList.Keys)
        {
            tileSize = 0.5;
            Tile newMapChunk = Instantiate(mapChunkPrefab, new Vector3(chunkAddress.addrP, chunkAddress.addrQ, 0), Quaternion.identity);
            float offsetVertical = (float)(tileSize * chunkAddress.addrR * 3 / 2);
            float offsetHorizontal = (float)(tileSize * (Math.Sqrt(3) * chunkAddress.addrQ + Math.Sqrt(3) / 2 * chunkAddress.addrR));

            newMapChunk.transform.position = transform.position + new Vector3(offsetHorizontal, offsetVertical);

            newMapChunk.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
