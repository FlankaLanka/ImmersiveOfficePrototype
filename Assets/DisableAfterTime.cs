using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DisableAfterTime : MonoBehaviour
{
    [Header("Story")]
    [SerializeField] private Flowchart f;

    [Header("Audio")]
    [SerializeField] private AudioClip fixedBackgroundMusic;
    [SerializeField] private AudioSource backgroundMusic;
    private readonly float sec = 7f;
    private void OnEnable()
    {
        StartCoroutine(TimedDisable(sec));
    }

    //after visuals, change audio to good bgm and start story
    private IEnumerator TimedDisable(float sec)
    {
        yield return new WaitForSeconds(sec);
        backgroundMusic.clip = fixedBackgroundMusic;
        backgroundMusic.Play();
        f.SetBooleanVariable("RetrieveDataDone", true);
        gameObject.SetActive(false);
    }
}
