using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecievePlayerCamData : MonoBehaviour
{
    [SerializeField] private Texture chicken;
    [SerializeField] private RawImage playerFeed1;
    [SerializeField] private RawImage playerFeed2;
    private VideoCamera[] playerVideos;
    
    //used for not copying a muted webcam
    private bool player1Set;
    private bool player2Set;

    private void OnEnable()
    {
        playerVideos = FindObjectsOfType<VideoCamera>();
        player1Set = false;
        player2Set = false;
    }

    // Update is called once per frame
    void Update()
    {
        CopyImageFeed(playerFeed1, playerVideos[0], ref player1Set);
        //if player 2 hasnt joined, just render player1 camera
        if (playerVideos.Length >= 2)
            CopyImageFeed(playerFeed2, playerVideos[1], ref player2Set);
    }

    private void CopyImageFeed(RawImage feed, VideoCamera playerVideo, ref bool playerSet)
    {
        /*
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
