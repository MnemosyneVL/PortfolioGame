using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
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

    [Header("OtherFields")]
    public Sprite noImage;
    public ControlsItem controlsItem;

    [Header("SelectorFields")]
    public Sprite selectorViewImage;

    //Delivered fields
    private ScreenControlsHandler screenControlsRef;
    private ImageViewerHandler imageViewer;
    private ImageItem[] images;
    private int numOfPages;
    private int currentPage;

    public void SetContent(ImageItem[] items)
    {
        images = items;
    }

    public void CheckContent(ref ProjectInteractivesPositions interactives)
    {
        numOfPages = 0;
        if (images == null)
        {
            Debug.Log("No images were set");
            return;
        }
        interactives.imgAmount = images.Length;
        numOfPages = (int)Math.Floor((decimal)images.Length / 4);
        if (images.Length % 4 > 0)
        {
            numOfPages++;
        }
        interactives.imgPages = numOfPages;

        if(numOfPages > 1)
        {
            interactives.needImgUD = true;
        }
        FillContent(1);
    }

    public void FillContent(int pageNr)
    {
        if (images.Length > 0)
        {
            img1.sprite = noImage;
            imgDesc1.text = " ";
            img2.sprite = noImage;
            imgDesc2.text = " ";
            img3.sprite = noImage;
            imgDesc3.text = " ";
            img4.sprite = noImage;
            imgDesc4.text = " ";

            if (4 * (pageNr - 1) < images.Length)
            {
                if (images[4 * (pageNr - 1)] != null)
                {
                    img1.sprite = images[4 * (pageNr - 1)].image;
                    imgDesc1.text = images[4 * (pageNr - 1)].description;
                }
                else
                {
                    Debug.Log("Image missing on " + images[4 * (pageNr - 1) ] + " in " + this);
                }
            }
            if (4 * (pageNr - 1) + 1 < images.Length)
            {
                if (images[4 * (pageNr - 1) + 1] != null)
                {
                    img2.sprite = images[4 * (pageNr - 1) + 1].image;
                    imgDesc2.text = images[4 * (pageNr - 1) + 1].description;
                }
                else
                {
                    Debug.Log("Image missing on " + images[4 * (pageNr - 1) + 1] + " in " + this);
                }
            }
            if (4 * (pageNr - 1) + 2 < images.Length)
            {
                if (images[4 * (pageNr - 1) + 2] != null)
                {
                    img3.sprite = images[4 * (pageNr - 1) + 2].image;
                    imgDesc3.text = images[4 * (pageNr - 1) + 2].description;
                }
                else
                {
                    Debug.Log("Image missing on " + images[4 * (pageNr - 1) + 2] + " in " + this);
                }
            }
            if (4 * (pageNr - 1) + 3 < images.Length)
            {
                if (images[4 * (pageNr - 1) + 3] != null)
                {
                    img4.sprite = images[4 * (pageNr - 1) + 3].image;
                    imgDesc4.text = images[4 * (pageNr - 1) + 3].description;
                }
                else
                {
                    Debug.Log("Image missing on " + images[4 * (pageNr - 1) + 3] +" in "+ this);
                }
            }
            CheckButtons(pageNr);
            currentPage = pageNr;
            Debug.Log(currentPage);

        }
        else
        {
            Debug.Log("No descriptions set to the project");
        }
    }

    private void CheckButtons(int pageNr)
    {
        if (pageNr >= numOfPages)
        {
            imageButtonDown.interactable = false;
        }
        else
        {
            imageButtonDown.interactable = true;
        }
        if (pageNr <= 1)
        {
            imageButtonUp.interactable = false;
        }
        else
        {
            imageButtonUp.interactable = true;
        }
    }

    #region Interactives Action

    public void NextPage()
    {
        if (currentPage + 1 <= numOfPages)
        {
            Debug.Log($"Next page, ;(currentPage)");
            FillContent(currentPage + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPage - 1 > 0)
        {
            Debug.Log($"(currentPage)");
            FillContent(currentPage - 1);
        }
    }

    public void AdditionalOnStart()
    {
        imageViewer.DeliverImages(images);
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

    public void OpenImage1()
    {
        imageViewer.InteractionMainAction(4 * (currentPage - 1));
        screenControlsRef.HideControls();
        Debug.Log("Image Filler Script: To be added");
        Debug.Log("Opens Current Image 1");
    }

    public void OpenImage2()
    {
        if (4 * (currentPage - 1) + 1 <= images.Length - 1)
        {
            imageViewer.InteractionMainAction(4 * (currentPage - 1) + 1);
            screenControlsRef.HideControls();
        }
        else
        {
            OpenImage1();
        }
        Debug.Log("Image Filler Script: To be added");
        Debug.Log("Opens Current Image 1");
    }

    public void OpenImage3()
    {
        if (4 * (currentPage - 1) + 2 <= images.Length - 1)
        {
            imageViewer.InteractionMainAction(4 * (currentPage - 1) + 2);
            screenControlsRef.HideControls();
        }
        else
        {
            OpenImage2();
        }
        Debug.Log("Image Filler Script: To be added");
        Debug.Log("Opens Current Image 1");
    }

    public void OpenImage4()
    {
        if (4 * (currentPage - 1) + 3 <= images.Length - 1)
        {
            imageViewer.InteractionMainAction(4 * (currentPage - 1) + 3);
            screenControlsRef.HideControls();
        }
        else
        {
            OpenImage3();
        }
        Debug.Log("Image Filler Script: To be added");
        Debug.Log("Opens Current Image 1");
    }

    public void NextImage()
    {
        imageViewer.NextImage();
        Debug.Log("Next image from keyboard buttons");
    }

    public void PreviousImage()
    {
        imageViewer.PreviousImage();
        Debug.Log("Previous image from keyboard buttons");
    }
    #endregion

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }

    public void SetImageViewerRef(ImageViewerHandler viewer)
    {
        imageViewer = viewer;
    }
}
