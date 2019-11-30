using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIRefDataHolder : MonoBehaviour
{
    [Header("References")]
    public Image colorPOI;
    public Text textPOI;
    public RectTransform ownTransform;
    [Header("Given References")]
    public MiniMapHandler miniMapHandler;
    [Header("Setup")]
    public CanvasGroup canvasGroup;
    public float lerpSpeed = 0.1f;

    public Vector3 teleportPosition;

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowPOIName()
    {
        StartCoroutine(CanvasAnimation(canvasGroup, canvasGroup.alpha, 1f, lerpSpeed));
    }

    public void HidePOIName()
    {
        StartCoroutine(CanvasAnimation(canvasGroup, canvasGroup.alpha, 0f, lerpSpeed));
    }

    public void TeleportToPOI()
    {
        if(teleportPosition != null)
        miniMapHandler.TeleportCharacter(teleportPosition);
    }


    private IEnumerator CanvasAnimation(CanvasGroup ui, float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        Vector2 vector2 = new Vector2(0, 0);
        float finalPosition = 0f;
        while (true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;

            float currentValue = Mathf.Lerp(start, end, finalPosition);
            ui.alpha = currentValue;

            if (finalPosition >= 1)
                break;
            yield return new WaitForEndOfFrame();
        }
    }
}
