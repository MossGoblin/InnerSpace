using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOverlayManager : MonoBehaviour
{

    private bool dungeonOverlayActive;
    public GameObject dungeonOverlay;
    public Dungeon dungeon;
    private bool currentChunkActive;

    void Start()
    {
        dungeonOverlayActive = false;
        currentChunkActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            dungeonOverlayActive = !dungeonOverlayActive;
        }

        dungeonOverlay.SetActive(dungeonOverlayActive);
        HandleCurrentChunkActivation();

    }

    private void HandleCurrentChunkActivation()
    {
        if (dungeonOverlayActive && currentChunkActive)
        {
            dungeon.DeactivateCurrentChunk();
            currentChunkActive = false;
        }
        if (!dungeonOverlayActive && !currentChunkActive)
        {
            dungeon.ActivateCurrentChunk();
            currentChunkActive = true;
        }
    }
}
