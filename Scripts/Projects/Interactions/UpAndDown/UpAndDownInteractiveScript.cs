using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownInteractiveScript : MonoBehaviour
{
    public UpAndDownAnimationScript animationScript;
    public Action OnStart;
    public Action OnEnd;
    public Action OnDown;
    public Action OnUp;

    #region Interactive Actions
    public void UpAction()
    {
        OnUp?.Invoke();
        animationScript.OnUp();
    }

    public void DownAction()
    {
        OnDown?.Invoke();
        animationScript.OnDwn();
    }
    public void OnStartAction()
    {
        Debug.Log("Start action");
        OnStart?.Invoke();
        animationScript.OnStart();
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        OnEnd?.Invoke();
        animationScript.OnEnd();
    }
    #endregion
}
