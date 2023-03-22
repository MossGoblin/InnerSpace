using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    private ConfigManager cfgManager;
    // private GameObject persistentDungeonUIToggleObject;
    private Toggle persistentDungeonUIToggle;

    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        cfgManager = gameManager.cfgManager;

        // persistentDungeonUIToggle = persistentDungeonUIToggleObject.GetComponent<Toggle>();
        if (GameObject.Find("ToggleDungeonUI"))
        {
            persistentDungeonUIToggle = GameObject.Find("ToggleDungeonUI").GetComponent<Toggle>();
            persistentDungeonUIToggle.isOn = cfgManager.config.persistentDungeonUI;
        }
    }

    void Update()
    {
        
    }

    public void TogglePersistantDungeonUI()
    {
        cfgManager.config.persistentDungeonUI = !cfgManager.config.persistentDungeonUI;
        Debug.Log($"Persistent Dungeon UI Toggled to {cfgManager.config.persistentDungeonUI}");
    }
}
