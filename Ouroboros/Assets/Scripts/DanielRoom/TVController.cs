using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    bool tvOn = true;

    public VideoPlayer videoPlayer;

    public RawImage tv;

    private void OnEnable()
    {
        RemoteController.OnButtonClick += ChangeTVState;
    }

    private void OnDisable()
    {
        GameManagerVRBase.OnGameEnd -= ChangeTVState;
    }
    
    public void ChangeTVState()
    {
        if (tvOn)
        {
            tvOn = false;
            videoPlayer.playbackSpeed = 0;
            tv.enabled = false ;
        }
        else if (!tvOn)
        {
            tvOn= true;
            videoPlayer.playbackSpeed = 1;
            tv.enabled = true;
        }
    }
}
