using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectInteractivesPositions
{
    public int id;
    public string projectName;
    public string[] tags;
    public Sprite gameIcon;
    public ScreenControlsHandler screenControls;
    public TeleportHandler teleportHandler;
    public ImageViewerHandler imageViewer;
    public LinkVerificationScript linkVerifier;

    public Vector3 teleportPos;

    public DescriptionFiller descriptionFiller;
    public Vector3 descriptionUpDwn;
    public bool needDescUD = false;

    public ImageFiller imageFiller;
    public Vector3 image1;
    public Vector3 image2;
    public Vector3 image3;
    public Vector3 image4;
    public Vector3 imageUpDwn;
    public bool needImgUD = false;
    public int imgPages;
    public int imgAmount;

    public VideoFiller videoFiller;
    public Vector3 videoPlay;
    public Vector3 videoUpDwn;
    public bool needVidUD = false;
    public int vidPages;

    public LinkFiller linkFiller;
    public Vector3 link1;
    public bool needLinkSelector = false;
    public int linkAmount;

    public Vector3 resetTriggerPosEnd;
    public Vector3 resetTriggerPosStart;
    public Vector3Int railStart;
    public Vector3Int railEnd;
    public ElementsExist elements;
}
