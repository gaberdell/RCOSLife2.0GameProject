using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    
    // Get prefered volume on start
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference", 0.0f);
    }

    // Set volume on audio mixer and update preference
    private void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumePreference", volume);
    }
}
