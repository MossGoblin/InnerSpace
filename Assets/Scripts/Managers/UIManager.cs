using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject gameUI;
    private Canvas gameUICanvas;
    private bool uiActive;

    private ConfigManager cfgManager;

    void Start()
    {
        uiActive = false;
        gameUI = GameObject.Find("FunctionalUI");
        gameUICanvas = gameUI.GetComponent<Canvas>();

        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        cfgManager = gameManager.cfgManager;
    }

    void Update()
    {
        if (cfgManager.config.persistentDungeonUI)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                uiActive = !uiActive;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                uiActive = true;
            }
            else
            {
                uiActive = false;
            }
        }
        gameUICanvas.enabled = uiActive;
        gameUI.gameObject.SetActive(uiActive);
    }
}
