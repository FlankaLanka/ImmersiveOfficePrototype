using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] private AudioClip fixedBackgroundMusic;
    [SerializeField] private AudioSource backgroundMusic;
    private readonly float sec = 7f;
    private void OnEnable()
    {
        StartCoroutine(TimedDisable(sec));
    }

    //after visuals, change audio to fixed
    private IEnumerator TimedDisable(float sec)
    {
        yield return new WaitForSeconds(sec);
        backgroundMusic.clip = fixedBackgroundMusic;
        backgroundMusic.Play();
        gameObject.SetActive(false);
    }
}
