using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoFiller : MonoBehaviour
{
    [Header("VideosDiv")]
    public Button videoButtonDown;
    public Button videoButtonUp;
    public RawImage videoImg;
    public VideoPlayer videoPlayer;
    public Text videoDesc;
    public Image beforeVideoImg;

    [Header("OtherFields")]
    public ControlsItem controlsItem;

    [Header("SelectorFields")]
    public Sprite selectorPlayPause;
    public Sprite selectorStop;

    //Delivered fields
    private ScreenControlsHandler screenControlsRef;
    private VideoItem[] videos;
    private int numOfPages;
    private int currentPage;
    private bool isVidPlaying = false;

    public void SetContent(VideoItem[] items)
    {
        videos = items;
    }

    public void CheckContent(ref ProjectInteractivesPositions interactives)
    {
        numOfPages = 0;
        if (videos == null)
        {
            Debug.Log("No videos were set");
            return;
        }

        numOfPages = videos.Length;

        interactives.vidPages = numOfPages;

        if (numOfPages > 1)
        {
            interactives.needVidUD = true;
        }
        FillContent(1);
    }

    public void FillContent(int pageNr)
    {
        if (videos.Length > 0)
        {
            StopVideo();
           if(videos[pageNr-1].video != null && videos[pageNr-1].videoTexture != null)
            {
                videoImg.texture = videos[pageNr - 1].videoTexture;
                videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videos[pageNr - 1].video);
                if (videos[pageNr - 1].description != null)
                {
                    videoDesc.text = videos[pageNr - 1].description;
                }
                else
                {
                    videoDesc.text = " ";
                }
            }
            CheckButtons(pageNr);
            currentPage = pageNr;
        }
        else
        {
            Debug.Log("No videos set to the project");
        }
    }

    private void CheckButtons(int pageNr)
    {
        if (pageNr >= numOfPages)
        {
            videoButtonDown.interactable = false;
        }
        else
        {
            videoButtonDown.interactable = true;
        }
        if (pageNr <= 1)
        {
            videoButtonUp.interactable = false;
        }
        else
        {
            videoButtonUp.interactable = true;
        }
    }

    #region Interactives Actions

    public void NextPage()
    {
        if (currentPage < numOfPages)
            FillContent(currentPage + 1);

    }

    public void PreviousPage()
    {
        if (currentPage > 1)
            FillContent(currentPage - 1);
    }

    public void OnStartAction()
    {
        Debug.Log("Start action");
        screenControlsRef.SetControlsData(controlsItem);
        screenControlsRef.ShowControls();
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        screenControlsRef.HideControls();
    }

    public void PlayVideo()
    {
        if(isVidPlaying)
        {
            videoPlayer.Pause();
            isVidPlaying = false;
        }
        else
        {
            videoPlayer.Play();
            isVidPlaying = true;
            beforeVideoImg.enabled = false;
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        isVidPlaying = false;
        beforeVideoImg.enabled = true;
    }
    #endregion

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }
}
