using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAnimationScript : MonoBehaviour
{
    public GameObject frontLeftDoor;
    public GameObject frontRightDoor;
    public float lerpTime = 0.5f;
    public float endOffset = 1f;

    private float initialLeftPos;
    private float initialRightPos;

    public void Start()
    {
        initialLeftPos = frontLeftDoor.transform.position.x;
        initialRightPos = frontRightDoor.transform.position.x;
    }
    public void OpenDoors()
    {
        StartCoroutine(DoorAnimation(frontLeftDoor.transform, frontLeftDoor.transform.position.x, initialLeftPos - endOffset, lerpTime));
        StartCoroutine(DoorAnimation(frontRightDoor.transform, frontRightDoor.transform.position.x, initialRightPos + endOffset, lerpTime));
    }

    public void CloseDoors()
    {
        StartCoroutine(DoorAnimation(frontLeftDoor.transform, frontLeftDoor.transform.position.x, initialLeftPos, lerpTime));
        StartCoroutine(DoorAnimation(frontRightDoor.transform, frontRightDoor.transform.position.x, initialRightPos, lerpTime));
    }


    private IEnumerator DoorAnimation(Transform objTransform, float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        float finalPosition = 0f;
        
        while (true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;
            float currentValue = Mathf.Lerp(start, end, finalPosition);

            objTransform.position = new Vector3(currentValue, objTransform.position.y, objTransform.position.z);


            if (finalPosition >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
