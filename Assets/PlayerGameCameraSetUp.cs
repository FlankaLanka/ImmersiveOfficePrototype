using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using DG.Tweening;
using Kino;

public class PlayerGameCameraSetUp : NetworkBehaviour
{
    private PlayerCameraManager pcm;
    private GameCameraJitterEffects camJitter;
    void Start()
    {
        if (isLocalPlayer)
        {
            //grab camera and set to appropriate position for 3rd person
            Transform cameraTransform = transform.Find("CameraTransform");
            Transform cam = Camera.main.transform; //GameObject.Find("Main Camera").transform;
            cam.parent = transform;
            cam.position = cameraTransform.position;
            cam.eulerAngles = cameraTransform.eulerAngles;

            //for visuals, play with jitter
            camJitter = FindObjectOfType<GameCameraJitterEffects>();
            camJitter.JitterRestore(7, 10, 6, 5);

            //linking local player's camera positions to the camera manager
            pcm = FindObjectOfType<PlayerCameraManager>();
            pcm.player = gameObject;
            pcm.camera3rdPersonView = cameraTransform;
            Transform cameraTransformFPS = transform.Find("CameraTransformFPS");
            pcm.cameraFPSView = cameraTransformFPS;
        }
    }    
}
