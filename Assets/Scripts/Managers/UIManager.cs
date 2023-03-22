using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject gameUI;
    private Canvas gameUICanvas;
    private bool uiActive;

    void Awake()
    {
        uiActive = false;
        gameUI = GameObject.Find("FunctionalUI");
        gameUICanvas = gameUI.GetComponent<Canvas>();
    }

    void Update()
    {
        gameUICanvas.enabled = uiActive;
        gameUI.gameObject.SetActive(uiActive);
        
        if (Input.GetKeyDown("enter"))
        {
            uiActive = !uiActive;
        }        
    }
}
