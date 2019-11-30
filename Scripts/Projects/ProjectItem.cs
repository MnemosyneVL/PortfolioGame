using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName ="New Project Data", menuName ="Project Assets/ NewProject")]
public class ProjectItem : ScriptableObject
{
    public int id;
    public string projectName;
    public DescriptionItem[] descriptions;
    public ImageItem[] images;
    public VideoItem[] videos;
    public string[] tags;
    public LinkItem[] links;
}
