using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using DG.Tweening;
using Kino;

public class PlayerGameCameraSetUp : NetworkBehaviour
{
    private DigitalGlitch dglitch;
    private AnalogGlitch aglitch;
    void Start()
    {
        if (isLocalPlayer)
        {
            //grab camera and set to appropriate position
            Transform cameraTransform = transform.Find("CameraTransform");
            Transform cam = GameObject.Find("Main Camera").transform;
            cam.parent = transform;
            cam.position = cameraTransform.position;
            cam.eulerAngles = cameraTransform.eulerAngles;

            //grab glitchs
            dglitch = cam.gameObject.GetComponent<DigitalGlitch>();
            aglitch = cam.gameObject.GetComponent<AnalogGlitch>();
            StablizeCamera();
        }
    }

    // stabilize camera over time
    private void StablizeCamera()
    {
        DOTween.To(() => dglitch.intensity, x => dglitch.intensity = x, 0, 7f);
        DOTween.To(() => aglitch.scanLineJitter, x => aglitch.scanLineJitter = x, 0, 10f);
        DOTween.To(() => aglitch.verticalJump, x => aglitch.verticalJump = x, 0, 6f);
        DOTween.To(() => aglitch.colorDrift, x => aglitch.colorDrift = x, 0, 5f);
    }    
}
