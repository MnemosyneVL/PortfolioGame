using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class LevelMarginTriggerScript : MonoBehaviour
{
    [Header("Settings")]
    public bool rightLimiter;
    [Header("References")]
    public SimpleCameraController character;
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
        Debug.Log("Start action");
        if (interactionObject.character != null)
        {
            character = interactionObject.character;
        }
        screenControlsRef.SetMessageData(controlsItem);
        screenControlsRef.ShowMessage();
        if(rightLimiter)
        {
            character.ImmobilizeRight();
        }
        else
        {
            character.ImmobilizeLeft();
        }
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        screenControlsRef.HideMessage();
        character.Mobilize();
    }
    #endregion

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }
}
