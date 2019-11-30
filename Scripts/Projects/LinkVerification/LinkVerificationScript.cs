using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkVerificationScript : MonoBehaviour
{
    public CanvasGroup imageCanvas;
    public Button acceptButton;
    public Button declineButton;
    public Link linkOnAcceptButton;
    public Text description;
    public float lerpSpeed = 0.1f;

    private string line1 = "Do you want to open \n ";
    private string line2 = "\n link in new tab?";

    // Start is called before the first frame update
    void Start()
    {
        imageCanvas.alpha = 0f;
        imageCanvas.blocksRaycasts = false;
    }

    public void DeliverLink(string linkDesc, string urlAdress)
    {
        description.text = line1 + linkDesc + line2;
        description.text.Replace("\\n", "\n");
        linkOnAcceptButton.link = urlAdress;
        OpenCanvas();
    }

    private void OpenCanvas()
    {
        StartCoroutine(CanvasAnimation(imageCanvas, imageCanvas.alpha, 1f, lerpSpeed));
        imageCanvas.blocksRaycasts = true;
    }

    private void CloseCanvas()
    {
        StartCoroutine(CanvasAnimation(imageCanvas, imageCanvas.alpha, 0f, lerpSpeed));
        imageCanvas.blocksRaycasts = false;
    }

    public void AcceptLinkOpen()
    {
        CloseCanvas();
    }

    public void DeclineLinkOpen()
    {
        CloseCanvas();
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
