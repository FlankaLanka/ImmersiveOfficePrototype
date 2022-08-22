using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    //these first 2 are set in local player when player spawns
    [HideInInspector] public GameObject player;
    [HideInInspector] public Transform cameraFPSView;
    [HideInInspector] public Transform camera3rdPersonView;
    [SerializeField] private Transform cameraTopView;

    private GameCameraJitterEffects jitterEffects;
    private Transform mainCamera;
    private bool inTopView = false;
    private bool in3rdPersonView = false;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        jitterEffects = FindObjectOfType<GameCameraJitterEffects>();
    }
    // Update is called once per frame
    void Update()
    {
        //don't allow camera changes when player isnt connected
        if (player == null)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
            ToggleTopView();
        if (Input.mouseScrollDelta.y != 0)
            ToggleFPS3rdPerson(Input.mouseScrollDelta.y);
    }


    private void ToggleTopView()
    {
        //start effects then place camera in position
        jitterEffects.SetDigital(1f);
        jitterEffects.PlayDistortAudio();
        jitterEffects.JitterRestore(1, 1, 1, 1);

        if (inTopView)
        {
            //return to 3rd person
            SetCamera(camera3rdPersonView);
            inTopView = false;
            in3rdPersonView = true;
        }
        else
        {
            SetCameraTopView();
            inTopView = true;
            in3rdPersonView = false;
        }
    }

    private void ToggleFPS3rdPerson(float dir)
    {
        //don't allow camera changes when in top view or same view as scroll direction
        if (inTopView || (in3rdPersonView && dir < 0) || !in3rdPersonView && dir > 0)
            return;

        jitterEffects.SetDigital(0.5f);
        jitterEffects.PlayDistortAudio();
        jitterEffects.JitterRestore(1, 1, 1, 1);

        if (dir > 0)
        {
            SetCamera(cameraFPSView);
            in3rdPersonView = false;
        }
        else
        {
            SetCamera(camera3rdPersonView);
            in3rdPersonView = true;
        }
    }

    private void SetCamera(Transform cameraTransform)
    {
        mainCamera.transform.parent = player.transform;
        mainCamera.transform.position = cameraTransform.position;
        mainCamera.transform.eulerAngles = cameraTransform.eulerAngles;
    }

    private void SetCameraTopView()
    {
        mainCamera.transform.parent = null;
        mainCamera.transform.position = cameraTopView.position;
        mainCamera.transform.eulerAngles = cameraTopView.eulerAngles;
    }
}
