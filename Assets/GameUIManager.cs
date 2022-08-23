using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Shift;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject videoRoomUI;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private ModalWindowManager quitUI;

    private void Update()
    {
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
            //smooth transition into ui, then enable
            //get player name first time settings is opened
            if (nameText.text == "")
                nameText.text = "Hello, " + PlayerPrefs.GetString("Name");

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
