using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProjectCreator : MonoBehaviour
{

    public Vector3Int startPosition = new Vector3Int(0,0,0);
    public GameObject projectPrefab;
    public WorldBuilder worldBuilder;
    public InteractivesFiller interactivesBuilder;
    public Tilemap wallTiles;
    public ScreenControlsHandler onScreenControls;
    public TeleportHandler onScreenTeleport;
    public ImageViewerHandler imageViewer;
    public LinkVerificationScript linkVerifier;
    public MiniMapHandler miniMapHandler;

    public ProjectItem[] currentProjects;
    private int numberOfProjects;
    private int projectVariable = 0;

    private GameObject instantiatedProject;
    private CanvasCreator instantiatedCanvas;
    private ContentFiller instantiatedFiller;
    private Vector3Int positionVar;
    private ElementsExist elementsExist;
    private ProjectInteractivesPositions projectInteractives;
    private LevelInteractivesPositions levelInteractives;

    private void Start()
    {
        currentProjects = GlobalDataStorage.GetInstance().GetSelectedProjects();
        numberOfProjects = currentProjects.Length;
        if(numberOfProjects > 0)
        {
            projectVariable = 0;
            startPosition = worldBuilder.CreateStart(startPosition, out levelInteractives);
            NextProjectCanvas(startPosition);
            for (int i = 1; i<numberOfProjects; i ++)
            {
                NextProjectCanvas(positionVar);
            }
            worldBuilder.CreateEnd(positionVar, ref levelInteractives);
            interactivesBuilder.CreateLevelInteractives(ref levelInteractives, ref projectInteractives);
            miniMapHandler.AddNewMinimapPoint(levelInteractives.exitPortalStart, "Back to Main Scene", new Color32(141, 224, 255, 255));
            miniMapHandler.AddNewMinimapPoint(levelInteractives.exitPortalEnd, "Back to Main Scene", new Color32(141, 224, 255, 255));
            miniMapHandler.AddNewMinimapPoint(levelInteractives.contactMePortalStart, "Ways of Contacting me", new Color32(255, 220, 155, 255));
            miniMapHandler.AddNewMinimapPoint(levelInteractives.contactMePortalEnd, "Ways of Contacting me", new Color32(255, 220, 155, 255));
            miniMapHandler.SetMinimapLimiters(levelInteractives);
            miniMapHandler.InitializeMinimap();
        }
        else
        {
            //<TO BE ADDED> No Projects were selected. Return to main scene or else.
            Debug.Log("No projects were selected");
        }
    }


    public void NextProjectCanvas(Vector3Int nextPosition)
    {
        //CheckProjectVAriable
        instantiatedProject = Instantiate(projectPrefab, wallTiles.GetCellCenterWorld(nextPosition), Quaternion.identity);
        instantiatedCanvas = instantiatedProject.GetComponent<CanvasCreator>();
        instantiatedCanvas.project = currentProjects[projectVariable];
        elementsExist = instantiatedCanvas.EditCanvas();
        positionVar = worldBuilder.CreateWalls(elementsExist, nextPosition, instantiatedProject, out projectInteractives);
        instantiatedFiller = instantiatedProject.GetComponent<ContentFiller>();
        instantiatedFiller.SetProject(currentProjects[projectVariable]);
        instantiatedFiller.FillContent(ref projectInteractives, ref elementsExist);
        projectInteractives.id = currentProjects[projectVariable].id;
        projectInteractives.projectName = currentProjects[projectVariable].projectName;
        projectInteractives.tags = currentProjects[projectVariable].tags;
        projectInteractives.screenControls = onScreenControls;
        projectInteractives.teleportHandler = onScreenTeleport;
        projectInteractives.imageViewer = imageViewer;
        projectInteractives.linkVerifier = linkVerifier;
        projectVariable += 1;
        interactivesBuilder.CreateInteractives(projectInteractives);
        TransferInteractivesToMinimap(projectInteractives);
        //interactivesBuilder.CreateInteractives(projectInteractives,instantiatedFiller);
        //^Create Interactives
        //NextProject;
    }
    private void TransferInteractivesToMinimap(ProjectInteractivesPositions interactives)
    {
        miniMapHandler.AddNewMinimapPoint(interactives.teleportPos, "\" " + interactives.projectName + " \" Project");
    }
}
