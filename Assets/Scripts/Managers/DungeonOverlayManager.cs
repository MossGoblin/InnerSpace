using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOverlayManager : MonoBehaviour
{

    private bool dungeonOverlayActive;
    public GameObject dungeonOverlay;

    void Start()
    {
        dungeonOverlayActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            dungeonOverlayActive = !dungeonOverlayActive;
        }

        dungeonOverlay.SetActive(dungeonOverlayActive);
    }
}
