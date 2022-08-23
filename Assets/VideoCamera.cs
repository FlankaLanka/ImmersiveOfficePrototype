using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.TextCore;

public class VideoCamera : NetworkBehaviour
{
    [SerializeField] private Texture2D chicken; // muted image
    public Renderer planeIn3D;// video plane
    static WebCamTexture webCam;
    public byte[] webCamData;

    public bool webCamOff = true;
    private int transmissionCounter = 0;
    private readonly int transmissionSpeed = 5; // simulates certain fps on camera 50/tranmissionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate video camera
        if (webCam == null)
            webCam = new WebCamTexture(320, 180);
        if (isLocalPlayer)
            FindObjectOfType<ToggleVideoAndVoice>().playercam = this;

        //start by initializing video to off
        planeIn3D.material.mainTexture = chicken;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        //webcam off
        if (webCamOff)
        {
            if (webCam.isPlaying)
            {
                //stop webcam on first frame, set to chicken image
                webCam.Stop();
                CmdSetToDefaultImage();
            }
            return;
        }

        //webcam on
        if (!webCam.isPlaying)
            webCam.Play();

        if(transmissionCounter < transmissionSpeed)
        {
            //converts webcam data from texture to bytes then sends to server
            Texture2D tex = new Texture2D(webCam.width, webCam.height, TextureFormat.RGB24, false);
            tex.SetPixels(webCam.GetPixels());
            tex.Apply();
            webCamData = tex.EncodeToJPG();
            CmdUpdateVideoCam(webCamData);
            transmissionCounter = 0;
        }
        transmissionCounter++;
    }

    [Command]
    private void CmdUpdateVideoCam(byte[] videodata)
    {
        RpcUpdateVideoCam(videodata);
    }

    [ClientRpc]
    private void RpcUpdateVideoCam(byte[] videodata)
    {
        if (videodata != null)
        {
            //loading the new texture in after converting it back from bytes
            Texture2D newtex = new(2,2);
            newtex.LoadImage(videodata);
            planeIn3D.material.mainTexture = newtex;
        }
    }

    [Command]
    private void CmdSetToDefaultImage()
    {
        RpcUpdateToDefault();
    }

    [ClientRpc]
    private void RpcUpdateToDefault()
    {
        planeIn3D.material.mainTexture = chicken;
    }
}
