using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UIManager uiManager;
    private ConfigManager cfgManager;
    private AssetManager assetManager;
    private LogManager logger;
    private ClockManager clocker;
    private Camera mainCamera;
    private Dungeon dungeon;

    void Start()
    {
        LoadManagers();
    }

    void Update()
    {
        
    }

    private void LoadManagers()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        logger = gameManager.logger;
        clocker = gameManager.clocker;
        cfgManager = gameManager.cfgManager;
        assetManager = gameManager.assetManager;
        uiManager = dungeon.uiManager;
    }

}


// Inventory
// Health
// Interaction (attack, defense, operate)
// Equipment?