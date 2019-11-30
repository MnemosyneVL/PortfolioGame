using UnityEngine;

[System.Serializable]
public class DescriptionItem
{
    [TextArea(3, 10)]
    public string text;
    [TextArea(3, 10)]
    public string header;
}