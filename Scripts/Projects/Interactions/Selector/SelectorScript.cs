using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorScript : MonoBehaviour
{
    [Header("References")]
    public Image image;
    public Text text;
    public CanvasGroup canvasGroup;
    [Header("Animation")]
    public SelectorAnimationScript animationScript;
    //To be added left and right arrows animation for juice
    [Header("Other Settings")]
    public ControlsItem controlsItem;
    public float lerpSpeed;
    public Sprite defaultIcon;


    //Delivered fields
    private ScreenControlsHandler screenControlsRef;

    //Delivered Additional Actions //crutch3
    public Action OnStart;

    private List<SelectorObj> actions = new List<SelectorObj>();
    private int currentAction = 0;

    private void Start()
    {
        //canvas.sizeDelta = new Vector2(0,0);//should be 400 x 300 when extended
        canvasGroup.alpha = 0f;
    }

    #region Interactive Actions
    public void NextAction()
    {
        if(actions.Count > 0)
        {
            if(currentAction + 1 < actions.Count)
            {
                currentAction += 1;
                FillContent();
            }
            else
            {
                currentAction = 0;
                FillContent();
            }
        }
    }

    public void PreviousAction()
    {
        if (actions.Count > 0)
        {
            if (currentAction - 1 >= 0)
            {
                currentAction -= 1;
                FillContent();
            }
            else
            {
                currentAction = actions.Count - 1;
                FillContent();
            }
        }
    }

    public void UseCurrentAction()
    {
        if(actions.Count >0)
        {
            actions[currentAction].action();
        }
    }

    public void SetAdditionalOnStartAction(Action action)
    {
        OnStart = action;
    }

    public void OnStartAction()
    {
        Debug.Log("Start action");
        screenControlsRef.SetControlsData(controlsItem);
        screenControlsRef.ShowControls();
        animationScript.OnStart();
        StartCoroutine (CanvasAnimation(canvasGroup, canvasGroup.alpha, 1f, lerpSpeed));
        OnStart?.Invoke();
        //FillContent();
    }

    public void OnEndAction()
    {
        Debug.Log("End action");
        screenControlsRef.HideControls();
        animationScript.OnEnd();
        StartCoroutine(CanvasAnimation(canvasGroup, canvasGroup.alpha, 0f, lerpSpeed));
    }
    #endregion

    public void FillContent()
    {
        if(actions.Count > 0)
        {
            image.sprite = actions[currentAction].icon;
            text.text = actions[currentAction].nameOfAction;
        }
    }

    public void InicializeInteractive()
    {
        FillContent();
    }

    public void AddNewAction(string actionName, Action newAction)
    {
        AddNewAction(actionName, newAction, defaultIcon);
    }

    public void AddNewAction(string actionName, Action newAction, Sprite sprite)
    {
        actions.Add(new SelectorObj
        {
            nameOfAction = actionName,
            action = newAction,
            icon = sprite
        });
    }

    public void SetOnScreenControlsRef(ScreenControlsHandler screenControls)
    {
        screenControlsRef = screenControls;
    }

    private IEnumerator CanvasAnimation(CanvasGroup ui,float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        Vector2 vector2 = new Vector2(0, 0);
        float finalPosition = 0f;
        while(true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;

            float currentValue = Mathf.Lerp(start, end, finalPosition);
            //vector2.x = currentvalue;
            //vector2.y = currentvalue * 0.75f;
            //ui.sizedelta = vector2;
            ui.alpha = currentValue;

            if (finalPosition >= 1)
                break;
            yield return new WaitForEndOfFrame();
        }
    }

}
