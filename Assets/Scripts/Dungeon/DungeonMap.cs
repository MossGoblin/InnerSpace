using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DungeonMap : MonoBehaviour
{
    public GameObject mapChunkPrefab;
    public Dungeon dungeon;
    private Dictionary<Address, Chunk> chunkList;
    private double tileHeight;
    private double tileSize;
    private double tilePPU;
    private AssetManager assetManager;
    float minimumScaleFactor = 0.6f;

    void Start()
    {
        // get managers
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        assetManager = gameManager.assetManager;

        tilePPU = 100;
        tileHeight = 442;
        tileSize = tileHeight / (4 * tilePPU); // half hight of tile sprite over pixels per unit = 100/100; 'height' is half vertical size (e.g. height of png)

        // calculate the offset from the active chunk
        Address activeChunkAddress = dungeon.GetActiveChunkAddress();
        float mapOffsetVertical = (float)(tileSize * activeChunkAddress.addrR * 3 / 2);
        float mapOffsetHorizontal = (float)(tileSize * (Math.Sqrt(3) * activeChunkAddress.addrQ + Math.Sqrt(3) / 2 * activeChunkAddress.addrR));
        Vector3 mapOffset = new Vector3(mapOffsetHorizontal, mapOffsetVertical);

        chunkList = dungeon.GetChunkList();
        foreach (Address chunkAddress in chunkList.Keys)
        {
            int chunkBiomeID = chunkList[chunkAddress].biomeID;

            Sprite chunkSprite = assetManager.GetChunkPrefab(chunkBiomeID);
            GameObject newMapChunk = Instantiate(mapChunkPrefab, new Vector3(chunkAddress.addrP, chunkAddress.addrQ, 0), Quaternion.identity);

            MapChunk newMapChunkScript = newMapChunk.GetComponent<MapChunk>();
            newMapChunkScript.SetMinimumDecayLevel(chunkList[chunkAddress].decay);
            newMapChunkScript.SetScaleFactor(minimumScaleFactor);
            newMapChunkScript.SetSprite(chunkSprite);
            newMapChunkScript.address = chunkAddress;

            newMapChunk.transform.SetParent(this.transform);

            float offsetVertical = (float)(tileSize * chunkAddress.addrR * 3 / 2);
            float offsetHorizontal = (float)(tileSize * (Math.Sqrt(3) * chunkAddress.addrQ + Math.Sqrt(3) / 2 * chunkAddress.addrR));
            newMapChunk.transform.position = transform.position + new Vector3(offsetHorizontal, offsetVertical) - mapOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
