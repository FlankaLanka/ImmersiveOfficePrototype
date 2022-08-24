using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.TextCore;

public class VideoCamera : NetworkBehaviour
{
    static WebCamTexture webCam;
    private Texture2D textureRead;
    private Texture2D textureWrite;
    public Renderer planeIn3D; //video plane -> used by ReievePlayerCamData for 2D video call
    [SerializeField] private byte[] webCamData;
    [HideInInspector] public bool webCamOff = true; //For Toggle Camera On/Off

    [Header("Resolution and FPS")]
    public int setCamWidth = 160;
    public int setCamHeight = 90;
    // simulates certain fps on camera 50/tranmissionSpeed; => set to 5fps for demo purposes
    private int transmissionCounter = 0;
    public int transmissionSpeed = 10;

    [Header("Mute Camera Image")]
    [SerializeField] private Texture2D chicken;

    //connecting to 2d call
    public RecievePlayerCamData playerFeed2D;

    // Start is called before the first frame update
    void Start()
    {
        //connect to video tuning (FPS, Resolution) settings
        if (isLocalPlayer)
        {
            FindObjectOfType<SettingsManager>().playerVideoController = this;
            FindObjectOfType<ToggleVideoAndVoice>().playercam = this;
        }

        //instantiate video camera and write texture
        if (webCam == null)
            webCam = new WebCamTexture(320, 180);

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
            //stop webcam on first frame, set to chicken image
            if (webCam.isPlaying)
            {
                webCam.Stop();
                CmdSetToDefaultImage();
            }
            return;
        }

        //webcam on
        if (!webCam.isPlaying)
            webCam.Play();

        if(transmissionCounter >= transmissionSpeed) // simulating 5fps camera for demo
        {
            //gets webcam data, resize to 160x90, send over network as bytes
            textureRead = new Texture2D(webCam.width, webCam.height, TextureFormat.RGB24, false);
            textureRead.SetPixels(webCam.GetPixels());
            textureRead.Apply();
            textureRead = ResizeTextureCarryImage(textureRead, setCamWidth, setCamHeight);
            webCamData = textureRead.EncodeToJPG();
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
            textureWrite = new(2, 2);
            textureWrite.LoadImage(videodata);
            planeIn3D.material.mainTexture = textureWrite;
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

    //resize image to increase performance, used for demo purposes
    private Texture2D ResizeTextureCarryImage(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}
