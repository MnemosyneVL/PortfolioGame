using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityTemplateProjects;

public class TeleportHandler : MonoBehaviour
{
    [Header("Refs")]
    public GameObject viewportContent;
    public Text currentProjectName;
    public Text currentOrderNr;
    public ToggleGroup viewportToggleGroup;

    public SimpleCameraController character;
    public CanvasGroup teleporterMenu;
    public Sprite defaultProjectSprite;
    public GameObject tpPointPrefab;

    [Header("Other Settings")]
    public float lerpSpeed;
    public List<TeleporterObj> teleportPoints = new List<TeleporterObj>();

    private int teleportOrderNr = 0;
    private TeleporterObj currentDestination;
    private TeleporterObj currentClient;
    private int currentTPClientID;
    private bool menuInUse = false;

    void Start()
    {
        teleporterMenu.alpha = 0f;
        teleporterMenu.blocksRaycasts = false;
    }

    public void CreateNewTeleport(int l_id, string l_projectName, string[] l_tags, Vector3 l_teleportPos, Sprite l_sprite)
    {
        teleportPoints.Add(new TeleporterObj
        {
            id = l_id,
            orderNr = ++teleportOrderNr,
            teleportName = l_projectName,
            teleportTags = l_tags,
            teleportPosition = l_teleportPos,
            teleportImg = l_sprite
        });
    }

    public void CreateNewTeleport(int l_id, string l_projectName, string[] l_tags, Vector3 l_teleportPos)
    {
        CreateNewTeleport(l_id, l_projectName, l_tags, l_teleportPos, defaultProjectSprite);
    }

    #region Interaction Actions
    public void InteractionMainAction(int tpClient_Id)
    {
        if(!menuInUse)
        {
            UseMenu(tpClient_Id);
        }
        else
        {
            StopUsingMenu();
        }
        //Open menu logic
    }

    private void UseMenu(int tpClient_Id)
    {
        character.Immobilize();
        currentTPClientID = tpClient_Id;
        OpenTeleportMenu();
        menuInUse = true;
        FillClientContent(tpClient_Id);
        FillViewport();
    }
    private void StopUsingMenu()
    {
        character.Mobilize();
        CloseTeleportMenu();
        menuInUse = false;
        EmptyViewport();
    }

    public void UseCloseButton()
    {
        StopUsingMenu();
    }

    public void InteractionStartAction()
    {
        //Start of interaction
        Debug.Log("Teleporter object is currently in use");
    }

    public void InteractionEndAction()
    {
        //End of interaction
        Debug.Log("Teleporter object is no longer in use");
    }
    #endregion

    #region CanvasHandling
    private void OpenTeleportMenu()
    {
        StartCoroutine(CanvasAnimation(teleporterMenu, teleporterMenu.alpha, 1f, lerpSpeed));
        teleporterMenu.blocksRaycasts = true;
    }

    private void CloseTeleportMenu()
    {
        StartCoroutine(CanvasAnimation(teleporterMenu, teleporterMenu.alpha, 0f, lerpSpeed));
        teleporterMenu.blocksRaycasts = false;
    }

    #endregion

    #region Content Filling
    private void FillClientContent(int id)
    {
        currentClient = teleportPoints.Where(item => item.id == id).First();
        currentProjectName.text = currentClient.teleportName;
        currentOrderNr.text = currentClient.orderNr.ToString();
    }

    public void FillViewport()
    {
        foreach(TeleporterObj tpPosition in teleportPoints)
        {
            if(tpPosition.id != currentClient.id)
                CreateNewViewportPosition(tpPosition);
        }
    }

    public void CreateNewViewportPosition(TeleporterObj teleporterObj)
    {
        GameObject instantiatedObj;
        TeleportListItemObj teleportListItemObj;
        instantiatedObj = Instantiate(tpPointPrefab, viewportContent.transform);
        teleportListItemObj = instantiatedObj.GetComponent<TeleportListItemObj>();
        teleportListItemObj.projectName.text = teleporterObj.teleportName;
        teleportListItemObj.teleporterID = teleporterObj.id;
        foreach(string tag in teleporterObj.teleportTags)
        {
            teleportListItemObj.tags.text += tag + "\n";
        }
        teleportListItemObj.projectIcon.sprite = teleporterObj.teleportImg;
        teleportListItemObj.orderNr.text = teleporterObj.orderNr.ToString();
        teleportListItemObj.toggle.group = viewportToggleGroup;
        teleportListItemObj.SetNewDestination += SetTeleportationDestination;
        teleportListItemObj.rectTransform.sizeDelta = new Vector2(teleportListItemObj.rectTransform.sizeDelta.x, 125);//crutch 2

    }

    public void EmptyViewport()
    {
        foreach (Transform child in viewportContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion

    #region Teleportation Handling
    public void SetTeleportationDestination(int destintationID)
    {
        if (destintationID > 0)
        {
            Debug.Log("SetNewDestination Worked");
            currentDestination = teleportPoints.Where(item => item.id == destintationID).First();
        }
        else
        {
            currentDestination = null;
        }
    }

    public void TeleportButton()
    {
        if(currentDestination != null)
        {
            Teleport(currentDestination);
        }
    }

    public void Teleport(TeleporterObj desitnation)
    {
        WarpPlayer(desitnation.teleportPosition);
        StopUsingMenu();
    }

    private void WarpPlayer(Vector3 destination)
    {
        Debug.Log("Teleportation Happened");
        character.TeleportPlayer(destination);
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
