using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class ImageViewerHandler : MonoBehaviour
{
    public CanvasGroup imageCanvas;
    public Button leftButton;
    public Button rightButton;
    public Image image;
    public Text description;
    public SimpleCameraController character;
    public float lerpSpeed = 0.1f;

    private ImageItem[] images;
    private bool menuInUse = false;
    private int currentImageNr = 0;

    void Start()
    {
        imageCanvas.alpha = 0f;
        imageCanvas.blocksRaycasts = false;
    }

    public void DeliverImages(ImageItem[] sprites)
    {
        currentImageNr = 0;
        images = sprites;
        FillContent(images[0]);
    }
    #region Interaction Options
    public void NextImage()
    {
        if (currentImageNr+1 > images.Length - 1)
        {
            currentImageNr = 0;
            FillContent(images[currentImageNr]);
            CheckButtons();
        }
        else
        {
            currentImageNr++;
            FillContent(images[currentImageNr]);
            CheckButtons();
        }
    }
    public void PreviousImage()
    {
        if (currentImageNr-1 < 0)
        {
            currentImageNr = images.Length - 1;
            FillContent(images[currentImageNr]);
            CheckButtons();
        }
        else
        {
            currentImageNr--;
            FillContent(images[currentImageNr]);
            CheckButtons();
        }
    }

    public void InteractionMainAction(int imageNr)
    {
        if (!menuInUse)
        {
            FillContent(images[imageNr]);
            currentImageNr = imageNr;
            CheckButtons();
            UseMenu();
        }
        else
        {
            StopUsingMenu();
        }
    }

    private void UseMenu()
    {
        character.Immobilize();
        OpenTeleportMenu();
        menuInUse = true;
    }
    private void StopUsingMenu()
    {
        character.Mobilize();
        CloseTeleportMenu();
        menuInUse = false;
    }
    
    #endregion

    #region CanvasHandling
    private void CheckButtons()
    {
        if(currentImageNr + 1 > images.Length - 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
        if (currentImageNr - 1 < 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
    }
    private void OpenTeleportMenu()
    {
        StartCoroutine(CanvasAnimation(imageCanvas, imageCanvas.alpha, 1f, lerpSpeed));
        imageCanvas.blocksRaycasts = true;
    }

    private void CloseTeleportMenu()
    {
        StartCoroutine(CanvasAnimation(imageCanvas, imageCanvas.alpha, 0f, lerpSpeed));
        imageCanvas.blocksRaycasts = false;
    }

    private void FillContent(ImageItem sprite)
    {
        image.sprite = sprite.image;
        description.text = sprite.description;
    }

    #endregion

    private IEnumerator CanvasAnimation(CanvasGroup canvasGroup, float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        float finalPosition = 0f;
        while (true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;
            float currentValue = Mathf.Lerp(start, end, finalPosition);

            canvasGroup.alpha = currentValue;


            if (finalPosition >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
