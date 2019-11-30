using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectPickerListItemObj : MonoBehaviour
{
    public Text tagText;
    public RectTransform rectTransform;
    public string tagName;
    public ProjectPickerHandler pickerHandler;
    public bool isSelected;


    public void OnclickEvent()
    {
        if (isSelected)
        {
            pickerHandler.RemoveSelectedTag(tagName);
        }
        else
        {
            pickerHandler.AddSelectedTag(tagName);
        }
        pickerHandler.RefreshViewports();
    }
}
