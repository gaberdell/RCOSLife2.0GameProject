using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

/* Class used to support the implementation of a graphical settings menu.
 * 
 * Save settings & load settings methods use the PlayerPrefs Unity API to store 
 * and load desired preferences, and other methods are used to interact with
 * Unity UI.
 * 
 */
public class Settings : MonoBehaviour
{
    // Initialize the Settings
    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumePreference", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(isFullscreen));
    }

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown graphicsQualityDropdown;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void SetGrpahicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("GraphicsQualityPreference", index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveSettings()
    {
         
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", currentResolutionIndex);
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference", 0));
        volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference", 0.0f);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQualityPreference", 5));
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicsQualityPreference", 5);
    }
}
