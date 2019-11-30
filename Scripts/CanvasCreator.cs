using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CanvasCreator : MonoBehaviour
{
    public GameObject description;
    public GameObject images;
    public GameObject videos;
    public GameObject links;
    public RectTransform rectTransform;
    public float oneLengthUnitSize;
    public float canvasHight;


    private float positionsTaken = 0;
    private bool isDesc = false;
    private bool isImg = false;
    private bool isVid = false;
    private bool isLink = false;

    [Header("Public fields assigned through script")]
    public ProjectItem project;
    public int canvasSize = 0;

    public ElementsExist EditCanvas()
    {
        CheckForElements();
        canvasSize = CalculateCanvasLength();
        rectTransform.sizeDelta = new Vector2(oneLengthUnitSize * canvasSize, canvasHight);
        AdjustElementsSize();
        return new ElementsExist
        {
            isDescription = isDesc,
            isImages = isImg,
            isVideos = isVid,
            isLinks = isLink,
            canvasLength = canvasSize   
        };
    }

    private void CheckForElements()
    {
        if (project.descriptions.Length > 0)
            isDesc = true;
        if (project.images.Length > 0)
            isImg = true;
        if (project.videos.Length > 0)
            isVid = true;
        if (project.links.Length > 0)
            isLink = true;
    }

    public int CalculateCanvasLength()
    {
        int lengthInUnits = 0;

        if (isDesc)
        {
            lengthInUnits += 2;
        }
        else
        {
            description.SetActive(false);
        }

        if (isImg)
        {
            lengthInUnits += 3;
        }
        else
        {
            images.SetActive(false);
        }

        if (isVid)
        {
            lengthInUnits += 4;
        }
        else
        {
            videos.SetActive(false);
        }

        if (isLink)
        {
            lengthInUnits += 1;
        }
        else
        {
            links.SetActive(false);
        }

        return lengthInUnits;
    }

    public void AdjustElementsSize()
    {
        if (isDesc)
        {
            RectTransform descTransform = description.GetComponent<RectTransform>();
            descTransform.anchorMin = new Vector2(positionsTaken, 0);
            descTransform.anchorMax = new Vector2(10f / canvasSize * 0.2f, 1);
            positionsTaken += 10f / canvasSize * 0.2f;
        }
        if (isImg)
        {
            RectTransform imgTransform = images.GetComponent<RectTransform>();
            imgTransform.anchorMin = new Vector2(positionsTaken, 0);
            imgTransform.anchorMax = new Vector2(10f / canvasSize * 0.3f + positionsTaken, 1);
            positionsTaken += 10f / canvasSize * 0.3f;
        }
        if (isVid)
        {
            RectTransform vidTransform = videos.GetComponent<RectTransform>();
            vidTransform.anchorMin = new Vector2(positionsTaken, 0);
            vidTransform.anchorMax = new Vector2(10f / canvasSize * 0.4f + positionsTaken, 1);
            positionsTaken += 10f / canvasSize * 0.4f;
        }
        if (isLink)
        {
            RectTransform linkTransform = links.GetComponent<RectTransform>();
            linkTransform.anchorMin = new Vector2(positionsTaken, 0);
            linkTransform.anchorMax = new Vector2(10f / canvasSize * 0.1f + positionsTaken, 1);
            positionsTaken += 10f / canvasSize * 0.1f;
        }
    }

}
