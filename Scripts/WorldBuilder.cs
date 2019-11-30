using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldBuilder : MonoBehaviour
{
    public ProjectCreator projectCreator;
    public Tilemap wallTiles;
    public Tilemap groundTiles;
    public Tile[] grounds;
    public Tile[] walls;
    public int[] projectStartLayout;
    private ProjectInteractivesPositions projectInteractives;

    private LevelInteractivesPositions levelInteractives;


    public Vector3Int CreateStart(Vector3Int startPosition, out LevelInteractivesPositions interPositions)
    {
        Vector3Int endPos = new Vector3Int(0,0,0);
        levelInteractives = new LevelInteractivesPositions();
        int n = -7;
        int startEnd = 2;
        for (int i = n; i <= startEnd; i++)
        {
            wallTiles.SetTile(startPosition + new Vector3Int(i, -2, 0), walls[1]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, -1, 0), walls[1]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 0, 0), walls[0]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 1, 0), walls[0]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 2, 0), walls[0]);
            groundTiles.SetTile(startPosition + new Vector3Int(i, 0, 0), grounds[0]);
            if(i == -3)
            {
                levelInteractives.levelLimiterLeft = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if(i == -2)
            {
                levelInteractives.exitPortalStart = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0)); 
            }
            if (i == 0)
            {
                levelInteractives.contactMePortalStart = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if (i == 1)
            {
                levelInteractives.playerSpawnPos = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if (i == startEnd)
            {
                endPos = startPosition + new Vector3Int(i, 0, 0);
            }

        }
        interPositions = levelInteractives;
        return endPos;
    }


    public Vector3Int CreateWalls(ElementsExist elements, Vector3Int startPosition, GameObject projectCanvas, out ProjectInteractivesPositions interPositions)
    {
        Vector3Int endPos;
        projectInteractives = new ProjectInteractivesPositions();
        endPos = CreateWallsStart(startPosition);
        MoveCanvas(projectCanvas,endPos);
        endPos = CreateWallsMiddle(elements, endPos);
        endPos = CreateWallsEnd(endPos);
        projectInteractives.elements = elements;
        interPositions = projectInteractives;
        return endPos;
    }

    public Vector3Int CreateEnd(Vector3Int startPosition, ref LevelInteractivesPositions interPositions)
    {
        Vector3Int endPos = new Vector3Int(0,0,0);
        int n = 0;
        int endEnd = 7;
        for (int i = n; i <= endEnd; i++)
        {
            wallTiles.SetTile(startPosition + new Vector3Int(i, -2, 0), walls[1]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, -1, 0), walls[1]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 0, 0), walls[0]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 1, 0), walls[0]);
            wallTiles.SetTile(startPosition + new Vector3Int(i, 2, 0), walls[0]);
            groundTiles.SetTile(startPosition + new Vector3Int(i, 0, 0), grounds[0]);
            if (i == 3)
            {
                interPositions.levelLimiterRight = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if (i == 2)
            {
                interPositions.contactMePortalEnd = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if (i == 0)
            {
                interPositions.exitPortalEnd = wallTiles.GetCellCenterWorld(startPosition + new Vector3Int(i, 0, 0));
            }
            if(i== endEnd)
            {
                endPos = startPosition + new Vector3Int(i, 0, 0);
            }
           
        }
        return endPos;
    }


    private Vector3Int CreateWallsStart(Vector3Int startPos)
    {
        Vector3Int endPos;
        wallTiles.SetTile(startPos + new Vector3Int(0, -2, 0), walls[1]);
        wallTiles.SetTile(startPos + new Vector3Int(0, -1, 0), walls[1]);
        wallTiles.SetTile(startPos, walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(0, 1, 0), walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(0, 2, 0), walls[0]);
        groundTiles.SetTile(startPos, grounds[0]);
        projectInteractives.teleportPos = wallTiles.GetCellCenterWorld(startPos);
        wallTiles.SetTile(startPos + new Vector3Int(1, -2, 0), walls[1]);
        wallTiles.SetTile(startPos + new Vector3Int(1, -1, 0), walls[1]);
        wallTiles.SetTile(startPos + new Vector3Int(1, 0, 0), walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(1, 1, 0), walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(1, 2, 0), walls[0]);
        groundTiles.SetTile(startPos + new Vector3Int(1, 0, 0), grounds[0]);
        projectInteractives.resetTriggerPosStart = wallTiles.GetCellCenterWorld(startPos + new Vector3Int(1, 0, 0));

        endPos = startPos + new Vector3Int(2, 0, 0);
        return endPos;
    }

    private void MoveCanvas(GameObject projectCanvas, Vector3Int startPos)
    {
        projectCanvas.transform.position = wallTiles.GetCellCenterWorld(startPos + new Vector3Int(-1, 0, 0));
    }

    private Vector3Int CreateWallsMiddle(ElementsExist elements, Vector3Int startPos)
    {
        Vector3Int endPos;
        Vector3Int step = startPos;
        int interactiveBooking = 0;
        if (elements.isDescription)
        {
            projectInteractives.descriptionUpDwn = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking, 0, 0));
            interactiveBooking += 2;
        }
        if (elements.isImages)
        {
            projectInteractives.imageUpDwn = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking, 0, 0));
            projectInteractives.image1 = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking + 1, 0, 0));
            interactiveBooking += 3;
        }
        if(elements.isVideos)
        {
            projectInteractives.videoUpDwn = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking, 0, 0));
            projectInteractives.videoPlay = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking + 1, 0, 0));
            interactiveBooking += 4;
        }
        if (elements.isLinks)
        {
            projectInteractives.link1 = wallTiles.GetCellCenterWorld(startPos +
                new Vector3Int(interactiveBooking - 1, 0, 0));//Crutch
            interactiveBooking += 1;
        }
        projectInteractives.railStart = startPos;
        projectInteractives.railEnd = startPos + new Vector3Int(elements.canvasLength, 0, 0);

        for (int i=0; i < elements.canvasLength; i++ )
        {
            step = startPos + new Vector3Int(i, 0, 0);
            groundTiles.SetTile(step, grounds[0]);
            wallTiles.SetTile(step + new Vector3Int(0, -2, 0), walls[1]);
            wallTiles.SetTile(step + new Vector3Int(0, -1, 0), walls[1]);
        }
        return endPos = step;
    }

    private Vector3Int CreateWallsEnd(Vector3Int startPos)
    {
        Vector3Int endPos = new Vector3Int(0, 0, 0);
        wallTiles.SetTile(startPos + new Vector3Int(0, -2, 0), walls[1]);
        wallTiles.SetTile(startPos + new Vector3Int(0, -1, 0), walls[1]);
        wallTiles.SetTile(startPos, walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(0, 1, 0), walls[0]);
        wallTiles.SetTile(startPos + new Vector3Int(0, 2, 0), walls[0]);
        groundTiles.SetTile(startPos, grounds[0]);
        projectInteractives.resetTriggerPosEnd = wallTiles.GetCellCenterWorld(startPos);

        endPos = startPos + new Vector3Int(1, 0, 0);
        return endPos;
    }
}
