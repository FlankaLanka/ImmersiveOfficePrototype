using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecievePlayerCamData : MonoBehaviour
{
    [SerializeField] private Texture chicken;
    [SerializeField] private RawImage playerFeed1;
    [SerializeField] private RawImage playerFeed2;

    //used for connecting player 3d feed to 2d feed
    private VideoCamera[] playerVideos;
    private VideoCamera playerVideo1;
    private VideoCamera playerVideo2;

    //used for not copying a muted webcam

    private void OnEnable()
    {
        playerVideos = FindObjectsOfType<VideoCamera>();

        //this helps order the player videos to have the local player be on left, player 2 be on right
        if(playerVideos[0].isLocalPlayer)
        {
            playerVideo1 = playerVideos[0];
            if(playerVideos.Length >= 2)
                playerVideo2 = playerVideos[1];
        }
        else
        {
            playerVideo1 = playerVideos[1];
            playerVideo2 = playerVideos[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerVideo1 != null)
            CopyImageFeed(playerFeed1, playerVideo1);

        //if player 2 hasnt joined, just render player1 camera
        if (playerVideo2 != null)
            CopyImageFeed(playerFeed2, playerVideo2);
    }

    private void OnDisable()
    {
        playerVideo1 = null;
        playerVideo2 = null;
    }

    private void CopyImageFeed(RawImage feed, VideoCamera playerVideo)
    {
        /*
        // this commented code is suppose to not allow the camera to copy the feed if player camera is muted
        //copy webcam feed, unless webcam off -> set to chicken
        if (playerVideo.webCamOff && playerSet)
            return;

        if(playerVideo.webCamOff && !playerSet)
        {
            feed.texture = chicken;
            playerSet = true;
            return;
        }

        //turning webcam back on
        playerSet = false;
        */
        feed.texture = playerVideo.planeIn3D.material.mainTexture;
    }
}
