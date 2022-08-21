using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Shift;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject controlsUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject voiceVideoUI;
    [SerializeField] private GameObject quitUI;

    public void SetUIActiveOnConnect() => voiceVideoUI.SetActive(true);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            HandleSettingsUI();

        if (Input.GetKeyDown(KeyCode.Escape))
            HandleQuitUI();
    }

    private void HandleSettingsUI()
    {
        settingsUI.SetActive(!settingsUI.activeInHierarchy);
    }


    // only handles opening modal, the rest is done in Shift
    private void HandleQuitUI()
    {
        if (!quitUI.activeInHierarchy)
            quitUI.GetComponent<ModalWindowManager>().ModalWindowIn();
    }

}
