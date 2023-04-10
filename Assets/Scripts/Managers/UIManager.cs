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
    public float distance = 500f;


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
        mouseWorldPosition.z = -20f;
        Vector2 ray = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        if (mapActive)
        {
            // RaycastHit2D hit = Physics2D.Raycast(origin: mouseWorldPosition, direction: ray, layerMask: mapLayer);
            RaycastHit2D hit = Physics2D.Raycast(origin: mouseWorldPosition, direction: ray);
            // if (hit.collider && hit.collider.gameObject.name=="MapHexBackground")
            if (hit.collider && hit.collider.gameObject.layer == mapLayer)
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
            RaycastHit2D hit = Physics2D.Raycast(origin: mouseWorldPosition, direction: ray);
            // if (hit.collider && hit.collider.gameObject.name=="Tile(Clone)")
            if (hit.collider && hit.collider.gameObject.layer == tilesLayer)
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
