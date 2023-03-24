using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessProfile brightnessLayer;
    AutoExposure exposure;
    
    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        brightnessSlider.value = PlayerPrefs.GetFloat("BrightnessPreference", 1.00f);
    }

    public void SetBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
            PlayerPrefs.SetFloat("BrightnessPreference", value);
        }
        else
        {
            exposure.keyValue.value = 0.01f;
            PlayerPrefs.SetFloat("BrightnessPreference", 0.01f);
        }
    }
}
