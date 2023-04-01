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

    void Start()
    {
        uiActive = false;
        gameUI = GameObject.Find("FunctionalUI");
        gameUICanvas = gameUI.GetComponent<Canvas>();

        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        cfgManager = gameManager.cfgManager;

        // TEST 
        debugText.text = "DEBUG TEST";
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

        MouseStuff();
    }

    private void MouseStuff()
    {
        int layerObject = 3;
        // Mouse Works
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        // Debug.Log($"MP: {mouseWorldPosition}");
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray, layerObject);
        if (hit.collider != null)
        {
            SetDebugText(hit.collider.gameObject.GetComponent<Tile>().address.ToString());
            // Debug.Log(hit.collider.gameObject.GetComponent<Tile>().address);
            // Debug.Log($"MP: {mouseWorldPosition}");
        }

    }

    public void SetDebugText(string text)
    {
        debugText.text = text;
    }
}
