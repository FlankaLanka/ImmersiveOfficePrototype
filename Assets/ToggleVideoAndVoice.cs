using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToggleVideoAndVoice : MonoBehaviour
{
    //Controls voice audio here. To find in game audio controller, check out settings manager

    public VideoCamera playercam;
    [SerializeField] private TextMeshProUGUI voiceText;
    [SerializeField] private TextMeshProUGUI videoText;

    [Header("Mic Panel")]
    [SerializeField] private GameObject micPanel;
    [SerializeField] private Image micIcon;
    [SerializeField] private Image headsetIcon;

    private VivoxLogin voiceChat;

    private void Start()
    {
        voiceChat = FindObjectOfType<VivoxLogin>();
    }

    public void ToggleVoiceOn()
    {
        //join channel, "room1" is arbitrary for now, since this is only a quick demo
        voiceChat.JoinChannel("room1");
        //change UI
        voiceText.text = "Voice Chat (ON)";
        voiceText.color = Color.green;

        micPanel.SetActive(true);
    }

    public void ToggleVoiceOff()
    {
        voiceChat.Leave_Channel();
        voiceText.text = "Voice Chat (OFF)";
        voiceText.color = Color.red;

        micPanel.SetActive(false);
    }

    public void ToggleVideoOn()
    {
        playercam.webCamOff = false;
        videoText.text = "Video Camera (ON)";
        videoText.color = Color.green;
    }

    public void ToggleVideoOff()
    {
        playercam.webCamOff = true;
        videoText.text = "Video Camera (OFF)";
        videoText.color = Color.red;
    }

    public void VoiceInSlider(float sliderVal)
    {
        if(sliderVal == 0f)
        {
            voiceChat.client.AudioInputDevices.Muted = true;
            micIcon.color = Color.red;
        }
        else
        {
            voiceChat.client.AudioInputDevices.Muted = false;
            voiceChat.client.AudioInputDevices.VolumeAdjustment = (int)((sliderVal - 0.5f) * 20f);
            micIcon.color = Color.green;
        }
    }
    public void VoiceOutSlider(float sliderVal)
    {
        if (sliderVal == 0f)
        {
            voiceChat.client.AudioOutputDevices.Muted = true;
            headsetIcon.color = Color.red;
        }
        else
        {
            voiceChat.client.AudioOutputDevices.Muted = false;
            voiceChat.client.AudioOutputDevices.VolumeAdjustment = (int)((sliderVal - 0.5f) * 20f);
            headsetIcon.color = Color.green;
        }
    }    

}
