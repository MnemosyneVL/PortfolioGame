using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFiller : MonoBehaviour
{
    public DescriptionFiller descriptionFiller;
    public ImageFiller imageFiller;
    public VideoFiller videoFiller;
    public LinkFiller linkFiller;
    public HeaderFiller headerFiller;

    private ProjectItem project;

    public void SetProject(ProjectItem item)
    {
        project = item;
    }

    public void FillContent(ref ProjectInteractivesPositions projectInteractivesPositions,ref ElementsExist elements)
    {
        if(project == null)
        {
            Debug.Log("no project Set");
            return;
        }
        headerFiller.SetContent(project.projectName, project.tags, project.id);
        headerFiller.CheckContent();
        projectInteractivesPositions.descriptionFiller = descriptionFiller;
        if (elements.isDescription)
        {
            descriptionFiller.SetContent(project.descriptions);
            descriptionFiller.CheckContent(ref projectInteractivesPositions);
        }
        projectInteractivesPositions.imageFiller = imageFiller;
        if (elements.isImages)
        {
            imageFiller.SetContent(project.images);
            imageFiller.CheckContent(ref projectInteractivesPositions);
        }
        projectInteractivesPositions.videoFiller = videoFiller;
        if (elements.isVideos)
        {
            videoFiller.SetContent(project.videos);
            videoFiller.CheckContent(ref projectInteractivesPositions);
        }
        projectInteractivesPositions.linkFiller = linkFiller;
        if (elements.isLinks)
        {
            linkFiller.SetContent(project.links);
            linkFiller.CheckContent(ref projectInteractivesPositions);
        }
    }
}
