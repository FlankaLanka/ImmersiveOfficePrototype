using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VideoCamera : NetworkBehaviour
{
    [SerializeField] private Texture2D chicken;
    [SerializeField] private Renderer videoPlane;
    static WebCamTexture webCam;
    public byte[] webCamData;
    private int waited = 0; // NEED FIX BEFORE SUBMITTING

    public bool webCamOff = true;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate video camera
        if (webCam == null)
            webCam = new WebCamTexture();
        if (isLocalPlayer)
            FindObjectOfType<ToggleVideoAndVoice>().playercam = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

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

        if (!webCam.isPlaying)
            webCam.Play();
        if (waited > 50) //simuating 1 frame per second for test
        {
            //converts webcam data from texture to bytes then sends to server
            Texture2D tex = new Texture2D(webCam.width / 2, webCam.height / 2, TextureFormat.RGB24, false);
            tex.SetPixels(webCam.GetPixels());
            tex.Apply();
            webCamData = tex.EncodeToJPG();
            CmdUpdateVideoCam(webCamData);
            waited = 0;
        }
        waited++;
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
            Texture2D newtex = new Texture2D(8,8);
            newtex.LoadImage(videodata);
            videoPlane.material.mainTexture = newtex;
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
        videoPlane.material.mainTexture = chicken;
    }


}
