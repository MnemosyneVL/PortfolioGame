using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MiniMapHandler : MonoBehaviour
{
    [Header("References")]
    public CameraContols cameraControls;
    public SimpleCameraController character;
    public RectTransform characterPointer;
    public RectTransform mapLine;
    public CanvasGroup canvasGroup;
    public ScrollRect scrollView;
    [Header("Essentials")]
    public GameObject prefabPOI;
    [Header("Options")]
    public bool manualSetup;
    public float minimapScaleFactor;
    public Color defaultPOIColor;
    public Vector3 cameraPos;
    public float lerpSpeed = 0.1f;
    [Header("Manual Minimap Setup")]
    public GameObject leftMapLimiter;
    public GameObject rightMapLimiter;
    public MinimapManualPOI[] pointsOfInterest;


    private Vector3 minimapStart;
    private Vector3 minimapEnd;
    private List<MiniMapPOI> teleportPositions = new List<MiniMapPOI>();
    private bool isUsed = false;
    private float characterDeltaPos;
    private float minimapLengthValue;
    private Vector3 oldCharPos;


    void Start()
    {
        if(manualSetup)
        {
            ManualMinimapSetup();
            SetMapLineLenght();
            CreateMapPOIs();
        }
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
        UpdateCharacterPointer();
        oldCharPos = characterPointer.localPosition;
    }

    void Update()
    {

        UpdateCharacterPointer();
        if (characterPointer.localPosition != oldCharPos)
        {
            CentralizeViewOnCharacter();
        }
        oldCharPos = characterPointer.localPosition;

    }
    #region MiniMap Controls
    public void UseMinimap()
    {
        if (isUsed)
        {
            CloseMinimap();
            isUsed = false;
        }
        else
        {
            OpenMinimap();
            isUsed = true;
            CentralizeViewOnCharacter();
        }
    }
    public void OpenMinimap()
    {
        StartCoroutine(CanvasAnimation(canvasGroup, canvasGroup.alpha, 1f, lerpSpeed));
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        cameraControls.SetNewCameraOffset(cameraPos);
    }
    public void CloseMinimap()
    {
        StartCoroutine(CanvasAnimation(canvasGroup, canvasGroup.alpha, 0f, lerpSpeed));
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        cameraControls.ResetCameraOffset();
    }
    #endregion
    #region MiniMap General

    private void UpdateCharacterPointer()
    {
        float characterOnMapPos = 0f;
        characterDeltaPos = character.transform.position.x - minimapStart.x;
        characterOnMapPos = (characterDeltaPos / minimapScaleFactor)- (mapLine.sizeDelta.x / 2f);
        characterPointer.localPosition = new Vector3(characterOnMapPos, characterPointer.localPosition.y, characterPointer.localPosition.z);
    }

    private void CentralizeViewOnCharacter()
    {
        scrollView.horizontalNormalizedPosition = characterDeltaPos / minimapLengthValue;
    }

    public void TeleportCharacter(Vector3 position)
    {
        character.TeleportPlayer(position);
    }
    #endregion

    #region Minimap Creation Common

    private void SetMapLineLenght()
    {
        minimapLengthValue = minimapEnd.x - minimapStart.x;
        mapLine.sizeDelta = new Vector2(minimapLengthValue / minimapScaleFactor, mapLine.sizeDelta.y);
    }

    private void CreateMapPOIs()
    {
        foreach(MiniMapPOI poi in teleportPositions)
        {
            CreateNewMapPOI(poi);
        }
    }

    private void CreateNewMapPOI(MiniMapPOI newPOI)
    {
        GameObject instantiatedPOI = Instantiate(prefabPOI, mapLine);
        POIRefDataHolder poiData = instantiatedPOI.GetComponent<POIRefDataHolder>();
        float objDeltaPos = newPOI.position.x - minimapStart.x;
        float objOnMapPos = (objDeltaPos / minimapScaleFactor) - (mapLine.sizeDelta.x / 2f);
        poiData.ownTransform.localPosition = new Vector3(objOnMapPos, poiData.ownTransform.localPosition.y, poiData.ownTransform.localPosition.z);
        poiData.colorPOI.color = newPOI.color;
        poiData.textPOI.text = newPOI.description;
        poiData.miniMapHandler = this;
        poiData.teleportPosition = newPOI.position;
    }

    public void AddNewMinimapPoint(Vector3 pointPosition, string pointDescription)
    {
        teleportPositions.Add(new MiniMapPOI
        {
            position = pointPosition,
            description = pointDescription,
            color = defaultPOIColor
        });
    }
    public void AddNewMinimapPoint(Vector3 pointPosition, string pointDescription,Color pointColor)
    {
        teleportPositions.Add(new MiniMapPOI
        {
            position = pointPosition,
            description = pointDescription,
            color = pointColor
        });
    }
    #endregion

    #region Minimap Creation Manual

    private void ManualMinimapSetup()
    {
        minimapStart = leftMapLimiter.transform.position;
        minimapEnd = rightMapLimiter.transform.position;
        foreach(MinimapManualPOI point in pointsOfInterest)
        {
            AddNewMinimapPoint(point.objectOfInterest.transform.position, point.pointDescription, point.color);
        }
    }

    #endregion

    #region Minimap Creation Automatic

    public void SetMinimapLimiters(LevelInteractivesPositions levelInteractives)
    {
        minimapStart = levelInteractives.levelLimiterLeft;
        minimapEnd = levelInteractives.levelLimiterRight;
    }

    public void InitializeMinimap()
    {
        SetMapLineLenght();
        CreateMapPOIs();
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
