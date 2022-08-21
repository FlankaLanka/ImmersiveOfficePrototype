using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleVideoAndVoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI voiceText;
    [SerializeField] private TextMeshProUGUI videoText;
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
    }

    public void ToggleVoiceOff()
    {
        voiceChat.Leave_Channel();
        voiceText.text = "Voice Chat (OFF)";
        voiceText.color = Color.red;
    }

    public void ToggleVideoOn()
    {
        videoText.text = "Video Camera (ON)";
        videoText.color = Color.green;
    }

    public void ToggleVideoOff()
    {
        videoText.text = "Video Camera (OFF)";
        videoText.color = Color.red;
    }


}
