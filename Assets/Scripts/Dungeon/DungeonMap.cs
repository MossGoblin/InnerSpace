using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour
{
    public Tile mapChunkPrefab;
    public Dungeon dungeon;
    private Dictionary<Address, Chunk> chunkList;
    void Awake()
    {
        chunkList = dungeon.GetChunkList();
        foreach (Address chunkAddress in chunkList.Keys)
        {
            Tile newMapChunk = Instantiate(mapChunkPrefab, new Vector3(chunkAddress.addrP, chunkAddress.addrQ, 0), Quaternion.identity);
            newMapChunk.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
