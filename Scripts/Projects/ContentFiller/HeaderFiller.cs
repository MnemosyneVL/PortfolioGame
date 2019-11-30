using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderFiller : MonoBehaviour
{
    [Header("HeaderDiv")]
    public Text projectNameText;
    public Text tagsText;

    private string[] _tags;
    private string _projectName;
    private int _id;

    public void SetContent(string projectName, string[] tags, int id)
    {
        _projectName = projectName;
        _tags = tags;
        _id = id;
    }

    public void CheckContent()
    {
        FillContent();
    }

    public void FillContent()
    {
        if(_projectName != null)
        {
            projectNameText.text = _projectName;
            tagsText.text = "";
            foreach(string tag in _tags)
            {
                tagsText.text += tag + "\n";
            }
        }
    }
}
