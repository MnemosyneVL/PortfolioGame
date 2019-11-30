using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class ImageItem
{
    public Sprite image;
    [TextArea(3, 10)]
    public string description;
}