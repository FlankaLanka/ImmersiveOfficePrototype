using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Shift;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Settings UI")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject videoRoomUI;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private ModalWindowManager quitUI;

    //used for not allowing player to toggle menu until loading in
    [HideInInspector]
    public bool playerLoadedIn = false;

    private void Update()
    {
        if (!playerLoadedIn)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
            HandleSettingsUI();

        if (Input.GetKeyDown(KeyCode.Escape))
            HandleQuitUI();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            HandleVideoRoom();
    }

    private void HandleVideoRoom()
    {
        if(!videoRoomUI.activeInHierarchy)
        {
            videoRoomUI.SetActive(true);
        }
        else
        {
            videoRoomUI.SetActive(false);
        }
    }


    private void HandleSettingsUI()
    {
        if(!mainUI.activeInHierarchy)
        {
            StartCoroutine(EnableMainUI());
        }
        else
        {
            //smooth transition out of ui, then disable
            StartCoroutine(DisableMainUI());
        }
    }

    // only handles opening modal, the rest is done in Shift
    private void HandleQuitUI()
    {
        if(!quitUI.gameObject.activeInHierarchy)
            quitUI.GetComponent<ModalWindowManager>().ModalWindowIn();
    }

    private IEnumerator EnableMainUI()
    {
        yield return null;
        mainUI.SetActive(true);
    }

    private IEnumerator DisableMainUI()
    {
        yield return null;
        mainUI.SetActive(false);
    }

}
