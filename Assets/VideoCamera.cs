using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VideoCamera : NetworkBehaviour
{
    [SerializeField]
    private Renderer videoPlane;
    static WebCamTexture webCam;
    public byte[] webCamData;

    private int waited = 0; // NEED FIX BEFORE SUBMITTING

    // Start is called before the first frame update
    void Start()
    {
        //instantiate video camera
        if (webCam == null)
            webCam = new WebCamTexture();
        if (!webCam.isPlaying)
            webCam.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //converts webcam data into jpg then sends to server, server then calls all clients to update image
        if(isLocalPlayer && waited > 40)
        {
            Texture2D tex = new Texture2D(webCam.width, webCam.height, TextureFormat.RGB24, false);
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
            Texture2D tex = new Texture2D(6, 6);
            tex.LoadImage(videodata);
            videoPlane.material.mainTexture = tex;
        }
    }


}
