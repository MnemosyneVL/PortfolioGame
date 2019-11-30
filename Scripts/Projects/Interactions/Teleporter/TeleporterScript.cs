using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [Header("Refs")]
    public TeleporterAnimationScript animationScript;

    [Header("Other Settings")]
    public ControlsItem controlsItem;

    private TeleportHandler teleportHandler;
    private ScreenControlsHandler screenControlsRef;
    private int this_id;

    #region Interactive Actions

    public void UseCurrentAction()
    {
        Debug.Log("TeleporterShouldOpen");
        //Open Teleporter Menu
        teleportHandler.InteractionMainAction(this_id);
        screenControlsRef.HideControls();
        screenControlsRef.HideMessage();
    }

    public void OnStartAction()
    {
        //Set Controls
        screenControlsRef.SetControlsData(controlsItem);
        screenControlsRef.ShowControls();
        screenControlsRef.SetMessageData(controlsItem);
        screenControlsRef.ShowMessage();
        teleportHandler.InteractionStartAction();
        animationScript.OpenDoors();
    }

    public void OnEndAction()
    {
        //Clear Controls
        screenControlsRef.HideControls();
        screenControlsRef.HideMessage();
        teleportHandler.InteractionEndAction();
        animationScript.CloseDoors();
    }
    #endregion

    public void SetTeleporterHandlerRef(TeleportHandler tpHand)
    {
        teleportHandler = tpHand;
    }

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }

    public void SetID(int id)
    {
        this_id = id;
    }

}
