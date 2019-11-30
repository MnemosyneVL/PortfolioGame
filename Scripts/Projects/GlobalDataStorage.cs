using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataStorage : MonoBehaviour
{
    [SerializeField]
    protected ProjectItem[] allProjectItems;
    [SerializeField]
    protected ProjectItem[] selectedProjectItems;
    private int idVariable=0;
    private List<string> tagsArray = new List<string>();

    static GlobalDataStorage instance;
    private void Awake()
    {
        if( instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (allProjectItems.Length > 0)
        {
            foreach (ProjectItem project in allProjectItems)
            {
                project.id = ++idVariable;
            }
            selectedProjectItems = allProjectItems;
        }
    }

    public static GlobalDataStorage GetInstance()
    {
        return instance;
    }

    public ProjectItem[] GetAllProjects()
    {
        if (allProjectItems == null)
        {
            Debug.Log("No Selected Project Items Exist");
            return null;
        }
        return allProjectItems;
    }

    public ProjectItem[] GetSelectedProjects()
    {
        if (selectedProjectItems == null)
        {
            Debug.Log("No Selected Project Items Exist");
            return null;
        }
        return selectedProjectItems;
    }

    public void SetSelectedProjects(ProjectItem[] projectItems)
    {
        selectedProjectItems = projectItems;
    }

    public void ResetSelectedProjects()
    {
        selectedProjectItems = allProjectItems;
    }
    
    public List<string> GetTags()
    {
        if(tagsArray.Count > 0)
        {
            return tagsArray;
        }
        else
        {
            foreach (ProjectItem project in allProjectItems)
            {
                foreach (string tag in project.tags)
                {
                    if(!tagsArray.Contains(tag))
                    {
                        tagsArray.Add(tag);
                    }
                }
            }
            return tagsArray;
        }
    }

}
