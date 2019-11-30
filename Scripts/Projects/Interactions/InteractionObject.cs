using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class InteractionObject : MonoBehaviour
{
    public SimpleCameraController character;

    private Action OnUp { get; set; }
    private Action OnDown { get; set; }
    private Action OnLeft { get; set; }
    private Action OnRight { get; set; }
    private Action OnInteraction { get; set; }
    private Action OnStart { get; set; }
    private Action OnEnd { get; set; }

    #region Actions
    public void UpAction()
    {
        OnUp?.Invoke();
    }

    public void DownAction()
    {
        OnDown?.Invoke();
    }

    public void LeftAction()
    {
        OnLeft?.Invoke();
    }

    public void RightAction()
    {
        OnRight?.Invoke();
    }

    public void Interact()
    {
        OnInteraction?.Invoke();
    }

    public void StartAction()
    {
        OnStart?.Invoke();
    }

    public void StartAction(SimpleCameraController characterRef)
    {
        character = characterRef;
        StartAction();
    }

    public void EndAction()
    {
        OnEnd?.Invoke();
    }
    #endregion

    #region Set Delegates
    public void SetDelegateUp(Action action)
    {
        OnUp = action;
    }

    public void SetDelegateDown(Action action)
    {
        OnDown = action;
    }

    public void SetDelegateLeft(Action action)
    {
        OnLeft = action;
    }

    public void SetDelegateRight(Action action)
    {
        OnRight = action;
    }

    public void SetDelegateInteract(Action action)
    {
        OnInteraction = action;
    }

    public void SetDelegateStart(Action action)
    {
        OnStart = action;
    }

    public void SetAdditionalOnStartAction(Action action)
    {
        OnStart += action;
    }

    public void SetDelegateEnd(Action action)
    {
        OnEnd = action;
    }
    #endregion


}
