using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlideInOnEnable : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private RectTransform helloPanel;
    [SerializeField] private RectTransform settingsPanel;
    [SerializeField] private RectTransform controlsPanel;

    [Header("StartPositions")]
    [SerializeField] private RectTransform helloStart;
    [SerializeField] private RectTransform settingsStart;
    [SerializeField] private RectTransform controlsStart;

    [Header("EndPositions")]
    [SerializeField] private RectTransform helloEnd;
    [SerializeField] private RectTransform settingsEnd;
    [SerializeField] private RectTransform controlsEnd;

    private void OnEnable()
    {
        //on enable always set to start of tween
        helloPanel.anchoredPosition = helloStart.anchoredPosition;
        settingsPanel.anchoredPosition = settingsStart.anchoredPosition;
        controlsPanel.anchoredPosition = controlsStart.anchoredPosition;

        //tween to correct position
        helloPanel.DOAnchorPos(helloEnd.anchoredPosition, 1f);
        settingsPanel.DOAnchorPos(settingsEnd.anchoredPosition, 1f);
        controlsPanel.DOAnchorPos(controlsEnd.anchoredPosition, 1f);
    }

    private void OnDisable()
    {
        //reset position in case panel closed before tweening completes
        helloPanel.anchoredPosition = helloEnd.anchoredPosition;
        settingsPanel.anchoredPosition = settingsEnd.anchoredPosition;
        controlsPanel.anchoredPosition = controlsEnd.anchoredPosition;
    }
}
