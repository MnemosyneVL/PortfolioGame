using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ProjectCanvasHolder : MonoBehaviour
{
    [Header("DescriptionDiv")]
    public Text descriptionHeader;
    public Text descriptionParagraph;
    [Header("ImagesDiv")]
    public Button imageButtonDown;
    public Button imageButtonUp;
    public Image img1;
    public Text imgDesc1;
    public Image img2;
    public Text imgDesc2;
    public Image img3;
    public Text imgDesc3;
    public Image img4;
    public Text imgDesc4;
    [Header("VideosDiv")]
    public Button videoButtonDown;
    public Button videoButtonUp;
    public RawImage videoImg;
    public VideoPlayer videoPlayer;
    public Text videoDesc;
    [Header("LinksDiv")]
    public Button gitHubButton;
    public Button link1Button;
}
