using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// all the settings in the 'graphics' page in the settings menu
// uses playerpref to store and save values
// note: brightness settings are not included here, it is in its own separate object and script 

public class GraphicsSettings : MonoBehaviour
{
    public TMPro.TMP_Dropdown graphicsQualityDropdown;

    public TMPro.TMP_Dropdown resolutionDropdown;
    public UnityEngine.Resolution[] resolutions;


    // Start is called before the first frame update
    void Start()
    {
        GraphicsQualityStart();
        ResolutionsStart();
    }


    void GraphicsQualityStart()
    {
        // set graphics quality
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQualityPreference", 5));
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicsQualityPreference", 5);
    }

    public void SetGrpahicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("GraphicsQualityPreference", index);
    }


    void ResolutionsStart()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            } 
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", currentResolutionIndex);
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void SetFullScreen (bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

}
