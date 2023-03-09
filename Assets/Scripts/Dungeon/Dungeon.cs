using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<int[], Chunk> chunkList;
    [SerializeField]
    private int[] activeChunkAddress;
    [SerializeField]
    private int[] activeChunk;

    void Start()
    {
        chunkList = new Dictionary<int[], Chunk>();

        // Inspector View Test
        activeChunkAddress = new int[] {1, 1, 0};

        // Create a chunk and adopt it
    }

    void Update()
    {
        
    }
}
