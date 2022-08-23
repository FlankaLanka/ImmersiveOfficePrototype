using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    //Controls in game audio only. For voice audio settings, check out voice panel
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        //normalize sound on start
        mixer.SetFloat("SFX", Mathf.Log10(0.5f) * 20);
        mixer.SetFloat("Music", Mathf.Log10(0.5f) * 20);
    }

    public void SetSFXVolume(float sliderVal)
    {
        if (sliderVal == 0f)
        {
            mixer.SetFloat("SFX", 0);
        }
        else
        {
            mixer.SetFloat("SFX", Mathf.Log10(sliderVal) * 20);
        }
    }

    public void SetMusicVolume(float sliderVal)
    {
        if (sliderVal == 0f)
        {
            mixer.SetFloat("Music", 0);
        }
        else
        {
            mixer.SetFloat("Music", Mathf.Log10(sliderVal) * 20);
        }
    }
}
