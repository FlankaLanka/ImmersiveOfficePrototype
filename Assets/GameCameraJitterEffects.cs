using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Kino;

public class GameCameraJitterEffects : MonoBehaviour
{
    //Write Jitter methods here
    [SerializeField] private AudioSource glitchAudio;
    private DigitalGlitch dglitch;
    private AnalogGlitch aglitch;
    private bool isRestoring = false;

    private void Start()
    {
        dglitch = FindObjectOfType<DigitalGlitch>();
        aglitch = FindObjectOfType<AnalogGlitch>();
    }

    public void JitterRestore(float digitalDur, float scanLineDur, float verticalDur, float colorDur)
    {
        if (isRestoring)
            return;

        DOTween.To(() => dglitch.intensity, x => dglitch.intensity = x, 0, digitalDur);
        DOTween.To(() => aglitch.scanLineJitter, x => aglitch.scanLineJitter = x, 0, scanLineDur);
        DOTween.To(() => aglitch.verticalJump, x => aglitch.verticalJump = x, 0, verticalDur);
        DOTween.To(() => aglitch.colorDrift, x => aglitch.colorDrift = x, 0, colorDur);
        isRestoring = true;
        StartCoroutine(RestoreCooldown(Mathf.Max(digitalDur, scanLineDur, verticalDur, colorDur)));
    }

    //prevents multiple tweens happening to the camera jitters
    private IEnumerator RestoreCooldown(float duration)
    { 
        yield return new WaitForSeconds(duration); 
        isRestoring = false; 
    }

    //all sets must be 0 - 1
    public void SetDigital(float intensity)
    {
        dglitch.intensity = intensity > 1 ? 1 : intensity;
        dglitch.intensity = intensity < 0 ? 0 : intensity;
    }

    public void SetVertical(float intensity)
    {
        aglitch.verticalJump = intensity > 1 ? 1 : intensity;
        aglitch.verticalJump = intensity < 0 ? 0 : intensity;
    }

    public void SetColor(float intensity)
    {
        aglitch.colorDrift = intensity > 1 ? 1 : intensity;
        aglitch.colorDrift = intensity < 0 ? 0 : intensity;
    }

    public void SetScanline (float intensity)
    {
        aglitch.scanLineJitter = intensity > 1 ? 1 : intensity;
        aglitch.scanLineJitter = intensity < 0 ? 0 : intensity;
    }

    public void SetHorizontal(float intensity)
    {
        aglitch.horizontalShake = intensity > 1 ? 1 : intensity;
        aglitch.horizontalShake = intensity < 0 ? 0 : intensity;
    }

    public void PlayDistortAudio()
    {
        glitchAudio.Play();
    }
}
