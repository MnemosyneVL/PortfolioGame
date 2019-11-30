using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionFiller : MonoBehaviour
{
    [Header("DescriptionDiv")]
    public Text descriptionHeader;
    public Text descriptionParagraph;
    public ScrollRect scrollRect;

    [Header("OtherFields")]
    public ControlsItem controlsItem;

    //Delivered fields
    private ScreenControlsHandler screenControlsRef;
    private DescriptionItem[] descriptions;

    public void SetContent(DescriptionItem[] items)
    {
        descriptions = items;
    }
    public void CheckContent(ref ProjectInteractivesPositions projectInteractives)
    {
        if (descriptions[0].text.Length > 589)
        {
            projectInteractives.needDescUD = true;
        }
        FillContent();
    }

    public void FillContent()
    {
        if (descriptions != null)
        {
            descriptionHeader.text = descriptions[0].header;
            descriptionParagraph.text = descriptions[0].text;
        }
        else
        {
            Debug.Log("No descriptions set to the project");
        }
    }

    #region Interactives Actions
    public void NextPage()
    {
        if(scrollRect.verticalScrollbar.value > 0)
        {
            scrollRect.verticalScrollbar.value -= 1f/((descriptions[0].text.Length / 589f) * 3f);
            if (scrollRect.verticalScrollbar.value < 0)
                scrollRect.verticalScrollbar.value = 0;
            Debug.Log("Next Page "+ descriptions[0].text.Length);
        }

    }

    public void PreviousPage()
    {
        if (scrollRect.verticalScrollbar.value < 1)
        {
            scrollRect.verticalScrollbar.value += 1f/((descriptions[0].text.Length / 589f) * 3f);
            if (scrollRect.verticalScrollbar.value > 1)
                scrollRect.verticalScrollbar.value = 1;
            Debug.Log("Previous Page");
        }
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

    #endregion
    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }
}
