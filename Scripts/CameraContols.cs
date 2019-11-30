using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContols : MonoBehaviour
{
    public Vector3 defaultOffset;
    public float lerpSpeed;
    public Transform objectToFollow;
    //[Header("Public Script Variables(Do not change)")]
    private Vector3 currentOffset;

    private void Start()
    {
        currentOffset = defaultOffset;
        transform.position =  objectToFollow.position + currentOffset;
    }
    private void Update()
    {
        Vector3 finalDestination = objectToFollow.position + currentOffset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, finalDestination, lerpSpeed);
        transform.position = Vector3.Lerp(transform.position ,lerpPosition,lerpSpeed);
    }

    public void SetNewCameraOffset(Vector3 offset)
    {
        currentOffset = offset;
    }

    public void ResetCameraOffset()
    {
        currentOffset = defaultOffset;
    }
}
