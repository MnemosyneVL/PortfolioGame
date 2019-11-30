using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    [Header("Settings")]
    public int portalTo;
    [Header("Animation")]
    public PortalAnimationScript animationScript;
    [Header("References")]
    public ScreenControlsHandler screenControlsRef;
    public ControlsItem controlsItem;
    public InteractionObject interactionObject;
    private SceneController sceneController;
    private GlobalDataStorage globalData;

    private void Awake()
    {
        interactionObject.SetDelegateStart(OnStartAction);
        interactionObject.SetDelegateEnd(OnEndAction);
        interactionObject.SetDelegateInteract(UseCurrentAction);
    }

    private void Start()
    {
        sceneController = SceneController.GetInstance();
        globalData = GlobalDataStorage.GetInstance();
    }

    #region Interactive Actions
    public void OnStartAction()
    {
        Debug.Log("Start action");
        screenControlsRef.SetControlsData(controlsItem);
        screenControlsRef.ShowControls();
        screenControlsRef.SetMessageData(controlsItem);
        screenControlsRef.ShowMessage();
        animationScript.OpenDoors();
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        screenControlsRef.HideControls();
        screenControlsRef.HideMessage();
        animationScript.CloseDoors();
    }

    public void UseCurrentAction()
    {
        if(portalTo == 0)
        {
            globalData.ResetSelectedProjects();
        }
        sceneController.ChangeScene(portalTo);
    }
    #endregion

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }

    public void SetDestination(int sceneNumber)
    {
        portalTo = sceneNumber;
    }
}
