using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game UI Objects")]
    [SerializeField] private GameObject LoginCanvas;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject videoVoicePanel;
    [SerializeField] private TextMeshProUGUI nametext;

    [Header("UserInfo")]
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField ipAddressField;
    private VivoxLogin voiceChat;
    private NetworkManagerOverride netManager;

    //used for making sure player loads in before being able to toggle UI menu
    private GameUIManager uiManager;

    private void Start()
    {
        voiceChat = FindObjectOfType<VivoxLogin>();
        netManager = FindObjectOfType<NetworkManagerOverride>();
        uiManager = gameObject.GetComponent<GameUIManager>();
    }

    public void JoinGame()
    {
        //connect to voice chat server
        voiceChat.Login(nameField.text);

        //connect to host server
        nametext.text = "Hello, " + nameField.text;
        string ipAddress = ipAddressField.text;
        netManager.networkAddress = ipAddress;
        netManager.StartClient();

        LoginCanvas.SetActive(false);
    }

    public void HostGame()
    {
        //connect to voice chat server
        voiceChat.Login(nameField.text);

        //create game server
        nametext.text = "Hello, " + nameField.text;
        netManager.StartHost();

        LoginCanvas.SetActive(false);
    }

    //doesn't actually do any data sync, just sets panels and allows players to toggle visuals active
    public void SyncDataUI()
    {
        Loading.SetActive(true);
        videoVoicePanel.SetActive(true);
        uiManager.playerLoadedIn = true;
    }
}
