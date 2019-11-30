using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectPickerScript : MonoBehaviour
{

    [Header("References")]
    public ProjectPickerHandler pickerHandler;
    public ScreenControlsHandler screenControlsRef;
    public ControlsItem controlsItem;
    public InteractionObject interactionObject;
    public ScreenAnimationScript screenAnimation;

    private void Awake()
    {
        interactionObject.SetDelegateStart(OnStartAction);
        interactionObject.SetDelegateEnd(OnEndAction);
        interactionObject.SetDelegateInteract(UseCurrentAction);
    }

    #region Interactive Actions
    public void OnStartAction()
    {
        Debug.Log("Start action");
        screenControlsRef.SetControlsData(controlsItem);
        screenControlsRef.ShowControls();
        pickerHandler.InteractionStartAction();
        screenAnimation.OnStart();
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        screenControlsRef.HideControls();
        pickerHandler.InteractionEndAction();
        screenAnimation.OnEnd();
    }

    public void UseCurrentAction()
    {
        pickerHandler.InteractionMainAction();
        screenControlsRef.HideControls();

    }
    #endregion
}
