using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Slider brightnessSlider;
    Resolution[] resolutions;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumePreference", volume);
    }

    public void SetBrightness(float brightness)
    {
        PlayerPrefs.SetFloat("BrightnessPreference", brightness);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
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
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference", 1));
        volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference", 0.0f);
        brightnessSlider.value = PlayerPrefs.GetFloat("BrightnessPreference", 0.0f);
    }
}
