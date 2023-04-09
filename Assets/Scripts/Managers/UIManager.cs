using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TMP_Text debugText;
    private GameObject gameUI;
    private Canvas gameUICanvas;
    private bool uiActive;
    private ConfigManager cfgManager;
    [SerializeField] Camera mainCamera;

    public GameObject dungeonOverlay;

    [SerializeField]
    private bool mapActive = false;
    public int tilesLayer;
    public int mapLayer;

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
        mapActive = dungeonOverlay.activeSelf;

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

        ReadTileUnderMouse();
    }

    private void ReadTileUnderMouse()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Vector2 ray = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        if (mapActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, ray, mapLayer);
            if (hit.collider && hit.collider.gameObject.name=="MapHexBackground")
            {
                SetDebugText($"MAP: {hit.collider.gameObject.GetComponentInParent<MapChunk>().address.ToString()}");
            }
            else
            {
                SetDebugText("");
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, ray, tilesLayer);
            if (hit.collider && hit.collider.gameObject.name=="Tile(Clone)")
            {
                SetDebugText($"TILE: {hit.collider.gameObject.GetComponent<Tile>().address.ToString()}");
            }
            else
            {
                SetDebugText("");
            }
        }

    }

    public void SetDebugText(string text)
    {
        debugText.text = text;
    }
}
