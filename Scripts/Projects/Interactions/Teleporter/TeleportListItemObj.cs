using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportListItemObj : MonoBehaviour
{
    public Toggle toggle;
    public int teleporterID;
    public Text orderNr;
    public Text projectName;
    public Text tags;
    public Image projectIcon;
    public RectTransform rectTransform;

    public Action<int> SetNewDestination { get; set; }
    private void Awake()
    {
        toggle.onValueChanged.AddListener((value) =>
        {
            Checker(value);
        });
    }
    public void Checker(bool isOn)
    {
        if (isOn)
        {
            DestinationChanger(teleporterID);
        }
        else
        {
            DestinationChanger(0);
        }
    }

    public void DestinationChanger(int id)
    {
        Debug.Log("Destination Changed");
        SetNewDestination?.Invoke(id);
    }
}
