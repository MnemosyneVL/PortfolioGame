using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class ProjectPickerHandler : MonoBehaviour
{
    [Header("Settings")]
    public float lerpSpeed = 0.2f;
    [Header("Refs")]
    public GameObject poolContent;
    public GameObject selectedContent;
    public Text projectsFoundNr;
    public InputField inputField;
    public CanvasGroup pickerMenu;
    public SimpleCameraController character;
    public GameObject tagCanvasPrefab;

    private GlobalDataStorage globalData;



    private List<string> allTags = new List<string>();

    private List<string> tagsPool = new List<string>();
    private List<string> tagsSelected = new List<string>();
    private List<ProjectItem> currentSelectedItems = new List<ProjectItem>();

    private bool menuInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        globalData = GlobalDataStorage.GetInstance();
        allTags = globalData.GetTags();
        tagsPool.AddRange(allTags);
        pickerMenu.alpha = 0f;
        pickerMenu.blocksRaycasts = false;
        SearchSelectedTags();
        SetSelectedProjects(currentSelectedItems.ToArray());
    }

    private void UseMenu()
    {
        character.Immobilize();
        OpenMenu();
        menuInUse = true;
        FillContent();
        projectsFoundNr.text = currentSelectedItems.Count.ToString();
    }
    private void StopUsingMenu()
    {
        character.Mobilize();
        CloseMenu();
        menuInUse = false;
        ClearAll();
    }

    public void UseCloseButton()
    {
        StopUsingMenu();
    }

    #region Interaction Actions
    public void InteractionMainAction()
    {
        if (!menuInUse)
        {
            UseMenu();
        }
        else
        {
            StopUsingMenu();
        }
        //Open menu logic
    }

    public void InteractionStartAction()
    {
        //Start of interaction
        Debug.Log("Project Picker object is currently in use");
    }

    public void InteractionEndAction()
    {
        //End of interaction
        Debug.Log("Project Picker object is no longer in use");
    }
    #endregion

    #region Menu Handling

    public void OpenMenu()
    {
        StartCoroutine(CanvasAnimation(pickerMenu, pickerMenu.alpha, 1f, lerpSpeed));
        pickerMenu.blocksRaycasts = true;
    }

    public void CloseMenu()
    {
        StartCoroutine(CanvasAnimation(pickerMenu, pickerMenu.alpha, 0f, lerpSpeed));
        pickerMenu.blocksRaycasts = false;
        SetSelectedProjects(currentSelectedItems.ToArray());
    }

    public void FillContent()
    {
        foreach (string tag in tagsPool)
        {
            CreateNewViewportPosition(tag, poolContent, false);
        }
        foreach (string tag in tagsSelected)
        {
            CreateNewViewportPosition(tag, selectedContent, true);
        }
    }

    public void ClearAll()
    {
        foreach (Transform child in poolContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in selectedContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateNewViewportPosition(string tagName, GameObject viewport, bool isSelected)
    {
        GameObject instantiatedObj;
        ProjectPickerListItemObj projectPickerListItemObj;
        instantiatedObj = Instantiate(tagCanvasPrefab, viewport.transform);
        projectPickerListItemObj = instantiatedObj.GetComponent<ProjectPickerListItemObj>();
        projectPickerListItemObj.tagName = tagName;
        projectPickerListItemObj.tagText.text = tagName;
        projectPickerListItemObj.pickerHandler = this;
        projectPickerListItemObj.rectTransform.sizeDelta = new Vector2(projectPickerListItemObj.rectTransform.sizeDelta.x, 75);//crutch 2
        projectPickerListItemObj.isSelected = isSelected;
    }

    #endregion

    #region Project Picking Handling

    public void AddSelectedTag(string tagName)
    {
        if (tagsPool.Contains(tagName))
        {
            tagsPool.Remove(tagName);
            tagsSelected.Add(tagName);
        }
    }

    public void RemoveSelectedTag(string tagName)
    {
        if (tagsSelected.Contains(tagName))
        {
            tagsSelected.Remove(tagName);
            tagsPool.Add(tagName);
        }
    }

    public void ClearSelectedTags()
    {
        tagsSelected.Clear();
        tagsPool.Clear();
        tagsPool.AddRange(allTags);
        RefreshViewports();
    }

    public void RefreshViewports()
    {
        ClearAll();
        FillContent();
        SearchSelectedTags();
    }

    public void SearchSelectedTags() //Version 1. Without "SearchPower"
    {
        ClearSelectedProjects();
        if (tagsSelected.Count > 0)
        {
            foreach (ProjectItem project in globalData.GetAllProjects())
            {
                bool canAdd = true;
                foreach (ProjectItem selectedProject in currentSelectedItems)
                {
                    if (project.id == selectedProject.id)
                    {
                        canAdd = false;
                        break;
                    }
                }
                if (canAdd)
                {
                    foreach (string projectTag in project.tags)
                    {
                        if (canAdd)
                        {
                            foreach (string selectedTag in tagsSelected)
                            {
                                if (selectedTag == projectTag)
                                {
                                    currentSelectedItems.Add(project);
                                    canAdd = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach (ProjectItem project in globalData.GetAllProjects())
            {
                currentSelectedItems.Add(project);
            }
        }
        projectsFoundNr.text = currentSelectedItems.Count.ToString();
    }

    void SetSelectedProjects(ProjectItem[] selectedProjects)
    {
        globalData.SetSelectedProjects(selectedProjects);
    }

    private void ClearSelectedProjects()
    {
        currentSelectedItems.Clear();
    }

    private void SearchForTag(string searchTag)
    {
        //AddsearchTag;
    }


    #endregion


    private IEnumerator CanvasAnimation(CanvasGroup canvasGroup, float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        float finalPosition = 0f;
        while (true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;
            float currentValue = Mathf.Lerp(start, end, finalPosition);

            canvasGroup.alpha = currentValue;


            if (finalPosition >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
