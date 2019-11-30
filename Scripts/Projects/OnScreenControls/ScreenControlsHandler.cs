using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenControlsHandler : MonoBehaviour
{
    public float lerpSpeed;

    [Header("References")]
    public CanvasGroup upButtonCanvas;
    public Text upButtonDesc;
    public CanvasGroup downButtonCanvas;
    public Text downButtonDesc;
    public CanvasGroup leftButtonCanvas;
    public Text leftButtonDesc;
    public CanvasGroup rightButtonCanvas;
    public Text rightButtonDesc;
    public CanvasGroup interactButtonCanvas;
    public Text interactButtonDesc;
    public CanvasGroup otherButtonCanvas;
    public Text otherButtonDesc;
    public CanvasGroup otherButton2Canvas;
    public Text otherButton2Desc;
    public CanvasGroup otherButton3Canvas;
    public Text otherButton3Desc;
    public CanvasGroup messageCanvas;
    public Text messageDesc;
    public Canvas controlsCanvas;
    [Header("Refs To Visuals")]
    public Image upDeco1;
    public Image upDeco2;
    public Image downDeco1;
    public Image downDeco2;
    public Image leftDeco1;
    public Image leftDeco2;
    public Image rightDeco1;
    public Image rightDeco2;
    public Image interactDeco1;
    public Image interactDeco2;
    public Image minimapDeco1;
    public Image minimapDeco2;

    private void Start()
    {
        ClearControlsData();
        upButtonCanvas.alpha = 0f;
        downButtonCanvas.alpha = 0f;
        leftButtonCanvas.alpha = 0f;
        rightButtonCanvas.alpha = 0f;
        interactButtonCanvas.alpha = 0f;
        otherButtonCanvas.alpha = 0f;
        otherButton2Canvas.alpha = 0f;
        otherButton3Canvas.alpha = 0f;
        messageCanvas.alpha = 0f;
    }

    public void SetControlsData(ControlsItem controls)
    {
        ClearControlsData();
        upButtonDesc.text = controls.upBtnDescription;
        downButtonDesc.text = controls.dwnBtnDescription;
        leftButtonDesc.text = controls.leftBtnDescription;
        rightButtonDesc.text = controls.rightBtnDescription;
        interactButtonDesc.text = controls.interactBtnDescription;
        otherButtonDesc.text = controls.otherBtnDescription;
        otherButton2Desc.text = controls.otherBtn2Description;
        otherButton3Desc.text = controls.otherBtn3Description;
        SetVisualsColors(controls.mainClr, controls.secondaryClr);
    }

    private void SetVisualsColors(Color mainColor, Color secondaryColor)
    {
        upDeco1.color = mainColor;
        upDeco2.color = secondaryColor;
        downDeco1.color = mainColor;
        downDeco2.color = secondaryColor;
        leftDeco1.color = mainColor;
        leftDeco2.color = secondaryColor;
        rightDeco1.color = mainColor;
        rightDeco2.color = secondaryColor;
        interactDeco1.color = mainColor;
        interactDeco2.color = secondaryColor;
        minimapDeco1.color = mainColor;
        minimapDeco2.color = secondaryColor;
    }

    public void ClearControlsData()
    {
        upButtonDesc.text = "";
        downButtonDesc.text = "";
        leftButtonDesc.text = "";
        rightButtonDesc.text = "";
        interactButtonDesc.text = "";
        otherButtonDesc.text = "";
        otherButton2Desc.text = "";
        otherButton3Desc.text = "";
        messageDesc.text = "";
    }

    public void ShowControls()
    {
        if(upButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(upButtonCanvas, upButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (downButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(downButtonCanvas, downButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (leftButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(leftButtonCanvas, leftButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (rightButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(rightButtonCanvas, rightButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (interactButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(interactButtonCanvas, interactButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (otherButtonDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(otherButtonCanvas, otherButtonCanvas.alpha, 1f, lerpSpeed));
        }
        if (otherButton2Desc.text != "")
        {
            StartCoroutine(CanvasAnimation(otherButton2Canvas, otherButton2Canvas.alpha, 1f, lerpSpeed));
        }
        if (otherButton3Desc.text != "")
        {
            StartCoroutine(CanvasAnimation(otherButton3Canvas, otherButton3Canvas.alpha, 1f, lerpSpeed));
        }
    }

    public void HideControls()
    {
        StartCoroutine(CanvasAnimation(upButtonCanvas ,upButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(downButtonCanvas ,downButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(leftButtonCanvas ,leftButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(rightButtonCanvas ,rightButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(interactButtonCanvas ,interactButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(otherButtonCanvas ,otherButtonCanvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(otherButton2Canvas ,otherButton2Canvas.alpha, 0f, lerpSpeed));
        StartCoroutine(CanvasAnimation(otherButton3Canvas ,otherButton3Canvas.alpha, 0f, lerpSpeed));
    }

    public void SetMessageData(ControlsItem controls)
    {
        messageDesc.text = controls.onScreenMessage;
    }

    public void ClearMessageData()
    {
        messageDesc.text = "";
    }

    public void ShowMessage()
    {
        if (messageDesc.text != "")
        {
            StartCoroutine(CanvasAnimation(messageCanvas, messageCanvas.alpha, 1f, lerpSpeed));
        }
    }

    public void HideMessage()
    {
        StartCoroutine(CanvasAnimation(messageCanvas, messageCanvas.alpha, 0f, lerpSpeed));
    }

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
