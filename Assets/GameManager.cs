using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using Michsky.UI.Shift;
using VivoxUnity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private GameObject LoginCanvas;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField ipAddressField;
    private VivoxLogin voiceChat;
    private NetworkManagerOverride netManager;
    // Start is called before the first frame update

    private void Start()
    {
        voiceChat = FindObjectOfType<VivoxLogin>();
        netManager = FindObjectOfType<NetworkManagerOverride>();
    }

    public void JoinGame()
    {
        //connect to voice chat server
        voiceChat.Login(nameField.text);

        //connect to host server
        PlayerPrefs.SetString("Name", nameField.text);
        string ipAddress = ipAddressField.text;
        netManager.networkAddress = ipAddress;
        netManager.StartClient();

        LoginCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }

    public void HostGame()
    {
        //connect to voice chat server
        voiceChat.Login(nameField.text);

        //create game server
        PlayerPrefs.SetString("Name", nameField.text);
        netManager.StartHost();

        LoginCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
