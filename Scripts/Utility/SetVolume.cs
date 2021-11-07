using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider MasterVolume;
    public Slider SFXVolume;
    public Slider MusicVolume;


    void Start()
    {
        MasterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        SFXVolume.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.75f);
        MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }



    public void SetMasterVolume(float sliderValue)
    {
        if (mixer != null)
        {
            // Debug.Log("Set Master");
            mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MasterVolume", sliderValue);
            PlayerPrefs.Save();
        }  
    }
    
    public void SetSFXVolume(float sliderValue)
    {
        if (mixer != null)
        {
            mixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("SoundEffectsVolume", sliderValue);
            PlayerPrefs.Save();
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (mixer != null)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
            PlayerPrefs.Save();
        }
    }
}