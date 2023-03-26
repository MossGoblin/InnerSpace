using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenuManager : MonoBehaviour
{
    private ConfigManager cfgManager;
    // private GameObject persistentDungeonUIToggleObject;
    private Toggle persistentDungeonUIToggle;
    public AudioMixer masterAudioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropDown;

    void Awake()
    {
        if (resolutionDropDown)
        {
            resolutions = Screen.resolutions;
            resolutionDropDown.ClearOptions();
            List<string> resolutionOptions = new List<string>();
            Resolution currentResolutionIndex;
            foreach (Resolution resolution in resolutions)
            {
                string option = resolution.width + " x " + resolution.height;
                resolutionOptions.Add(option);
                if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = resolution;
                }
            }
            resolutionDropDown.AddOptions(resolutionOptions);
            //resolutionDropDown.value = currentResolutionIndex
            // HERE https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=2s
        }

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

    public void TogglePersistantDungeonUI(bool persistentDungeonUI)
    {
        cfgManager.config.persistentDungeonUI = persistentDungeonUI;
        Debug.Log($"Persistent Dungeon UI Toggled to {persistentDungeonUI}");
    }


    public void SetMasterVolume(float masterVolumeControl)
    {
        Debug.Log(masterVolumeControl);
        cfgManager.config.volumeMaster = masterVolumeControl;
        masterAudioMixer.SetFloat("MasterVolume", masterVolumeControl);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
