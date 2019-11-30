using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [Header("Settings")]
    public bool oneUse;
    public bool messageTrigger;
    private bool used;
    [Header("References")]
    public ScreenControlsHandler screenControlsRef;
    public ControlsItem controlsItem;
    public InteractionObject interactionObject;

    private void Awake()
    {
        interactionObject.SetDelegateStart(OnStartAction);
        interactionObject.SetDelegateEnd(OnEndAction);
    }

    #region Interactive Actions
    public void OnStartAction()
    {
        if(used)
        {
            return;
        }
        Debug.Log("Start action");
        if (messageTrigger)
        {
            screenControlsRef.SetControlsData(controlsItem);
            screenControlsRef.ShowControls();
            screenControlsRef.SetMessageData(controlsItem);
            screenControlsRef.ShowMessage();
        }
        if (oneUse)
        {
            used = true;
        }
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        if (messageTrigger)
        {
            screenControlsRef.HideMessage();
            screenControlsRef.HideControls();
        }
    }
    #endregion
}
