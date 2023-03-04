using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    // List of chunks
    // May be a dictionary with chunk addresses, based on coordinates
    private Dictionary<int[], Chunk> chunkList;

    void Start()
    {
        chunkList = new Dictionary<int[], Chunk>();

        // Create a chunk and adopt it
    }

    void Update()
    {
        
    }
}
