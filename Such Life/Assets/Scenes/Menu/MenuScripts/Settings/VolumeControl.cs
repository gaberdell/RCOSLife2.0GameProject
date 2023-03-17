using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volume = PlayerPrefs.GetFloat("VolumePreference", 1.0f);
        volumeSlider.value = volume;
    }

    void OnVolumeChanged(float newVolume) 
    {
        volume = newVolume;
        PlayerPrefs.SetFloat("VolumePreference", volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
