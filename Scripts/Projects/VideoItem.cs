using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class VideoItem
{
    public RenderTexture videoTexture;
    public string video;
    [TextArea(3, 10)]
    public string description;
}