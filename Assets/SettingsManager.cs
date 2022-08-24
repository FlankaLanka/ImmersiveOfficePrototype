using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Michsky.UI.Shift;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    //Controls in game audio only. For voice audio settings, check out voice panel
    //Controls Camera settings too.

    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;

    [Header("Video")]
    [SerializeField] private TMP_InputField widthField;
    [SerializeField] private TMP_InputField heightField;
    [SerializeField] private TMP_InputField fpsField;
    [HideInInspector] public VideoCamera playerVideoController;

    private readonly int MAX_WIDTH = 640;
    private readonly int MAX_HEIGHT = 360;
    private readonly int MAX_FPS = 50;



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
    public void ApplyCameraSettings()
    {
        playerVideoController.setCamWidth = ConvertInputToResolution(widthField, MAX_WIDTH);
        playerVideoController.setCamHeight = ConvertInputToResolution(heightField, MAX_HEIGHT);
        playerVideoController.transmissionSpeed = ConvertInputToFPS(fpsField);
    }

    private int ConvertInputToResolution(TMP_InputField input, int max)
    {
        if (input.text == "")
            return max / 4;

        int.TryParse(input.text, out int inputInt);

        if (inputInt > max)
        {
            input.text = max.ToString();
            return max;
        }
        if (inputInt < 1)
        {
            input.text = 1.ToString();
            return 1;
        }
        return inputInt;
    }

    private int ConvertInputToFPS(TMP_InputField input)
    {
        //the reason we use max_fps/x is because we are using sleep to fake a camera fps change in VideoCamera.cs
        //default 5 fps
        if (input.text == "")
        {
            input.text = 5.ToString();
            return MAX_FPS / 5;
        }

        int.TryParse(input.text, out int inputInt);

        if (inputInt > MAX_FPS)
        {
            input.text = 50.ToString();
            return MAX_FPS / 50;
        }
        if (inputInt < 1)
        {
            input.text = 1.ToString();
            return MAX_FPS / 1;
        }
        return MAX_FPS / inputInt;
    }



}
