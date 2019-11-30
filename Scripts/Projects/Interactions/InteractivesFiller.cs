using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivesFiller : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject teleporterPrefab;
    public GameObject upDownPrefab;
    public GameObject selectorPrefab;
    public GameObject singlePurposePrefab;
    public GameObject portalPrefab;
    public GameObject portal2Prefab;
    public GameObject limiterPrefab;
    [Header("References")]
    public TeleportHandler teleportHandler;
    [Header("Settings")]
    public Vector3 offset;


    public void CreateInteractives(in ProjectInteractivesPositions positions)
    {
        CreateTeleporter(positions);
        CreateProjectResetTriggers(positions);
        if (positions.elements.isDescription)
        {
            if (positions.needDescUD)
            {
                CreateUpDownDescription(positions);
            }
        }
        if (positions.elements.isImages)
        {
            if (positions.needImgUD)
            {
                CreateUpDownImages(positions);
            }
            CreateSelectorImages(positions);
        }
        if (positions.elements.isVideos)
        {
            if (positions.needVidUD)
            {
                CreateUpDownVideos(positions);
            }
            CreateSelectorVideos(positions);
        }
        if (positions.elements.isLinks)
        {
            if (positions.needLinkSelector)
            {
                CreateSelectorLinks(positions);
            }
        }

    }

    public void CreateLevelInteractives(ref LevelInteractivesPositions positions, ref ProjectInteractivesPositions refs)
    {
        CreateExitPortals(in positions, in refs);
        CreateContactMePortals(in positions, in refs);
        CreateMapLimiters(in positions, in refs);
    }

    public void CreateTeleporter(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedTeleporter = Instantiate(teleporterPrefab, positions.teleportPos + offset, Quaternion.identity);
        if (positions.gameIcon != null)
        {
            teleportHandler.CreateNewTeleport(positions.id, positions.projectName, positions.tags, positions.teleportPos, positions.gameIcon);
        }
        else
        {
            teleportHandler.CreateNewTeleport(positions.id, positions.projectName, positions.tags, positions.teleportPos);
        }
        TeleporterScript teleporterScript = instantiatedTeleporter.GetComponent<TeleporterScript>();
        teleporterScript.SetID(positions.id);
        teleporterScript.SetOnScreenControlsRef(positions.screenControls);
        teleporterScript.SetTeleporterHandlerRef(positions.teleportHandler);



        InteractionObject interactionObject = instantiatedTeleporter.GetComponent<InteractionObject>();
        interactionObject.SetDelegateInteract(teleporterScript.UseCurrentAction);
        interactionObject.SetDelegateStart(teleporterScript.OnStartAction);
        interactionObject.SetDelegateEnd(teleporterScript.OnEndAction);

    }

    public void CreateUpDownDescription(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedUpDown = Instantiate(upDownPrefab, positions.descriptionUpDwn + offset, Quaternion.identity);
        UpAndDownInteractiveScript upDownScript = instantiatedUpDown.GetComponent<UpAndDownInteractiveScript>();

        upDownScript.OnDown = positions.descriptionFiller.NextPage;
        upDownScript.OnUp = positions.descriptionFiller.PreviousPage;
        upDownScript.OnStart = positions.descriptionFiller.OnStartAction;
        upDownScript.OnEnd = positions.descriptionFiller.OnEndAction;


        positions.descriptionFiller.SetOnScreenControlsRef(positions.screenControls);

        InteractionObject interactionObject = instantiatedUpDown.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(upDownScript.DownAction);
        interactionObject.SetDelegateUp(upDownScript.UpAction);
        interactionObject.SetDelegateStart(upDownScript.OnStartAction);
        interactionObject.SetDelegateEnd(upDownScript.OnEndAction);
    }

    public void CreateUpDownImages(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedUpDown = Instantiate(upDownPrefab, positions.imageUpDwn + offset, Quaternion.identity);
        UpAndDownInteractiveScript upDownScript = instantiatedUpDown.GetComponent<UpAndDownInteractiveScript>();

        upDownScript.OnDown = positions.imageFiller.NextPage;
        upDownScript.OnUp = positions.imageFiller.PreviousPage;
        upDownScript.OnStart = positions.imageFiller.OnStartAction;
        upDownScript.OnEnd = positions.imageFiller.OnEndAction;

        positions.imageFiller.SetOnScreenControlsRef(positions.screenControls);

        InteractionObject interactionObject = instantiatedUpDown.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(upDownScript.DownAction);
        interactionObject.SetDelegateUp(upDownScript.UpAction);
        interactionObject.SetDelegateStart(upDownScript.OnStartAction);
        interactionObject.SetDelegateEnd(upDownScript.OnEndAction);
    }

    public void CreateSelectorImages(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedSelector = Instantiate(selectorPrefab, positions.image1 + offset, Quaternion.identity);
        SelectorScript selectorScript = instantiatedSelector.GetComponent<SelectorScript>();

        positions.imageFiller.SetOnScreenControlsRef(positions.screenControls);
        positions.imageFiller.SetImageViewerRef(positions.imageViewer);
        selectorScript.SetOnScreenControlsRef(positions.screenControls);
        selectorScript.SetAdditionalOnStartAction(positions.imageFiller.AdditionalOnStart);

        selectorScript.AddNewAction("Open first image" , positions.imageFiller.OpenImage1, positions.imageFiller.selectorViewImage);
        if(positions.imgAmount>1)
            selectorScript.AddNewAction("Open second image", positions.imageFiller.OpenImage2, positions.imageFiller.selectorViewImage);
        if(positions.imgAmount>2)
            selectorScript.AddNewAction("Open third image", positions.imageFiller.OpenImage3, positions.imageFiller.selectorViewImage);
        if(positions.imgAmount>3)
            selectorScript.AddNewAction("Open fourth image", positions.imageFiller.OpenImage4, positions.imageFiller.selectorViewImage);

        selectorScript.InicializeInteractive();

        InteractionObject interactionObject = instantiatedSelector.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(selectorScript.NextAction);
        interactionObject.SetDelegateUp(selectorScript.PreviousAction);
        interactionObject.SetDelegateLeft(positions.imageFiller.PreviousImage);
        interactionObject.SetDelegateRight(positions.imageFiller.NextImage);
        interactionObject.SetDelegateInteract(selectorScript.UseCurrentAction);
        interactionObject.SetDelegateStart(selectorScript.OnStartAction);
        interactionObject.SetDelegateEnd(selectorScript.OnEndAction);
    }

    public void CreateUpDownVideos(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedUpDown = Instantiate(upDownPrefab, positions.videoUpDwn + offset, Quaternion.identity);
        UpAndDownInteractiveScript upDownScript = instantiatedUpDown.GetComponent<UpAndDownInteractiveScript>();

        upDownScript.OnDown = positions.videoFiller.NextPage;
        upDownScript.OnUp = positions.videoFiller.PreviousPage;
        upDownScript.OnStart = positions.videoFiller.OnStartAction;
        upDownScript.OnEnd = positions.videoFiller.OnEndAction;


        positions.videoFiller.SetOnScreenControlsRef(positions.screenControls);

        InteractionObject interactionObject = instantiatedUpDown.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(upDownScript.DownAction);
        interactionObject.SetDelegateUp(upDownScript.UpAction);
        interactionObject.SetDelegateStart(upDownScript.OnStartAction);
        interactionObject.SetDelegateEnd(upDownScript.OnEndAction);
    }

    public void CreateSelectorVideos(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedSelector = Instantiate(selectorPrefab, positions.videoPlay + offset, Quaternion.identity);
        SelectorScript selectorScript = instantiatedSelector.GetComponent<SelectorScript>();

        selectorScript.SetOnScreenControlsRef(positions.screenControls);

        selectorScript.AddNewAction("Play/Pause video", positions.videoFiller.PlayVideo, positions.videoFiller.selectorPlayPause);
        selectorScript.AddNewAction("Stop Video", positions.videoFiller.StopVideo, positions.videoFiller.selectorStop);
        selectorScript.InicializeInteractive();

        InteractionObject interactionObject = instantiatedSelector.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(selectorScript.NextAction);
        interactionObject.SetDelegateUp(selectorScript.PreviousAction);
        interactionObject.SetDelegateInteract(selectorScript.UseCurrentAction);
        interactionObject.SetDelegateStart(selectorScript.OnStartAction);
        interactionObject.SetDelegateEnd(selectorScript.OnEndAction);
    }

    public void CreateProjectResetTriggers(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedTrigger = Instantiate(singlePurposePrefab, positions.resetTriggerPosStart + offset, Quaternion.identity);
        InteractionObject interactionObject = instantiatedTrigger.GetComponent<InteractionObject>();
        if (positions.elements.isVideos)
        {
            interactionObject.SetDelegateStart(positions.videoFiller.StopVideo);
        }

        if (positions.elements.isImages)
        {
            interactionObject.SetAdditionalOnStartAction(positions.imageFiller.AdditionalOnStart);
        }

        instantiatedTrigger = Instantiate(singlePurposePrefab, positions.resetTriggerPosEnd + offset, Quaternion.identity);
        interactionObject = instantiatedTrigger.GetComponent<InteractionObject>();
        if (positions.elements.isVideos)
        {
            interactionObject.SetDelegateStart(positions.videoFiller.StopVideo);
        }

        if (positions.elements.isImages)
        {
            interactionObject.SetAdditionalOnStartAction(positions.imageFiller.AdditionalOnStart);
        }
    }

    public void CreateSelectorLinks(in ProjectInteractivesPositions positions)
    {
        GameObject instantiatedSelector = Instantiate(selectorPrefab, positions.link1 + offset, Quaternion.identity);
        SelectorScript selectorScript = instantiatedSelector.GetComponent<SelectorScript>();

        selectorScript.SetOnScreenControlsRef(positions.screenControls);
        positions.linkFiller.SetLinkVerifier(positions.linkVerifier);

        selectorScript.AddNewAction("Top Link", positions.linkFiller.UseFirstLink, positions.linkFiller.selectorHyperlink);
        if(positions.linkAmount>1)
            selectorScript.AddNewAction("Bottom Link", positions.linkFiller.UseSecondLink, positions.linkFiller.selectorHyperlink);
        selectorScript.InicializeInteractive();
        InteractionObject interactionObject = instantiatedSelector.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(selectorScript.NextAction);
        interactionObject.SetDelegateUp(selectorScript.PreviousAction);
        interactionObject.SetDelegateInteract(selectorScript.UseCurrentAction);
        interactionObject.SetDelegateStart(selectorScript.OnStartAction);
        interactionObject.SetDelegateEnd(selectorScript.OnEndAction);
    }

    private void CreateExitPortals(in LevelInteractivesPositions positions, in ProjectInteractivesPositions refs)
    {
        //First Portal
        GameObject instantiatedPortal = Instantiate(portalPrefab, positions.exitPortalStart + offset, Quaternion.identity);
        PortalScript portalScript = instantiatedPortal.GetComponent<PortalScript>();

        portalScript.SetOnScreenControlsRef(refs.screenControls);

        portalScript.SetDestination(0);
        portalScript.controlsItem.onScreenMessage = "This astral train will lead you back to introduction";

        InteractionObject interactionObject = instantiatedPortal.GetComponent<InteractionObject>();
        interactionObject.SetDelegateInteract(portalScript.UseCurrentAction);
        interactionObject.SetDelegateStart(portalScript.OnStartAction);
        interactionObject.SetDelegateEnd(portalScript.OnEndAction);
        //Second Portal
        instantiatedPortal = Instantiate(portalPrefab, positions.exitPortalEnd + offset, Quaternion.identity);
        portalScript = instantiatedPortal.GetComponent<PortalScript>();

        portalScript.SetOnScreenControlsRef(refs.screenControls);

        portalScript.SetDestination(0);
        portalScript.controlsItem.onScreenMessage = "This astral train will lead you back to introduction";
        interactionObject = instantiatedPortal.GetComponent<InteractionObject>();
        interactionObject.SetDelegateInteract(portalScript.UseCurrentAction);
        interactionObject.SetDelegateStart(portalScript.OnStartAction);
        interactionObject.SetDelegateEnd(portalScript.OnEndAction);
    }

    private void CreateContactMePortals(in LevelInteractivesPositions positions, in ProjectInteractivesPositions refs)
    {
        //First Portal
        GameObject instantiatedPortal = Instantiate(portal2Prefab, positions.contactMePortalStart + offset, Quaternion.identity);
        PortalScript portalScript = instantiatedPortal.GetComponent<PortalScript>();

        portalScript.SetOnScreenControlsRef(refs.screenControls);

        portalScript.SetDestination(2);
        portalScript.controlsItem.onScreenMessage = "This astral train will lead to ways of contacting me";

        InteractionObject interactionObject = instantiatedPortal.GetComponent<InteractionObject>();
        interactionObject.SetDelegateInteract(portalScript.UseCurrentAction);
        interactionObject.SetDelegateStart(portalScript.OnStartAction);
        interactionObject.SetDelegateEnd(portalScript.OnEndAction);
        //Second Portal
        instantiatedPortal = Instantiate(portal2Prefab, positions.contactMePortalEnd + offset, Quaternion.identity);
        portalScript = instantiatedPortal.GetComponent<PortalScript>();

        portalScript.SetOnScreenControlsRef(refs.screenControls);

        portalScript.SetDestination(2);
        portalScript.controlsItem.onScreenMessage = "This astral train will lead to ways of contacting me";
        interactionObject = instantiatedPortal.GetComponent<InteractionObject>();
        interactionObject.SetDelegateInteract(portalScript.UseCurrentAction);
        interactionObject.SetDelegateStart(portalScript.OnStartAction);
        interactionObject.SetDelegateEnd(portalScript.OnEndAction);
    }

    private void CreateMapLimiters(in LevelInteractivesPositions positions, in ProjectInteractivesPositions refs)
    {
        //Left Map Limiter
        GameObject instantiatedLimiter = Instantiate(limiterPrefab, positions.levelLimiterLeft + offset, Quaternion.identity);
        LevelMarginTriggerScript limiterScript = instantiatedLimiter.GetComponent<LevelMarginTriggerScript>();

        limiterScript.rightLimiter = false;
        limiterScript.SetOnScreenControlsRef(refs.screenControls);
        limiterScript.controlsItem.onScreenMessage = "Projects are in the opposite dirrection";

        InteractionObject interactionObject = instantiatedLimiter.GetComponent<InteractionObject>();
        interactionObject.SetDelegateStart(limiterScript.OnStartAction);
        interactionObject.SetDelegateEnd(limiterScript.OnEndAction);
        //Right Map Limiter
        instantiatedLimiter = Instantiate(limiterPrefab, positions.levelLimiterRight + offset, Quaternion.identity);
        limiterScript = instantiatedLimiter.GetComponent<LevelMarginTriggerScript>();

        limiterScript.rightLimiter = true;
        limiterScript.SetOnScreenControlsRef(refs.screenControls);
        limiterScript.controlsItem.onScreenMessage = "That's All Folks)";

        interactionObject = instantiatedLimiter.GetComponent<InteractionObject>();
        interactionObject.SetDelegateStart(limiterScript.OnStartAction);
        interactionObject.SetDelegateEnd(limiterScript.OnEndAction);
    }
}
