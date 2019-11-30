using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LinkItem
{
    public string link;
    [TextArea(3, 10)]
    public string description;
    public Sprite image;
    public Color color;
}
