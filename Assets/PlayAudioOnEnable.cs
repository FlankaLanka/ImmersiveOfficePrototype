using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour
{
    private AudioSource a;

    private void Awake()
    {
        a = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        a.Play();
    }
}
