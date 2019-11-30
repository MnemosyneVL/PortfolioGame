using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class InteractionCharacter : MonoBehaviour
{
    //<Comment>This script is responsible for character interaction with environment
    public KeyCode interactionKey;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public SimpleCameraController character;
    public Transform measurementPoint;
    public GameObject target;
    private List<GameObject> availableTargets = new List<GameObject>();
    //State Trackers
    private GameObject targetHolder;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        availableTargets.Clear();
        if (collision.transform.tag.Equals("Interactable"))
        {
            availableTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        availableTargets.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        CheckForTarget();
        if(Input.GetKeyDown(interactionKey))
        {
            InteractAction();
        }
        if (Input.GetKeyDown(upKey))
        {
            ActionUp();
        }
        if (Input.GetKeyDown(downKey))
        {
            ActionDown();
        }
        if (Input.GetKeyDown(leftKey))
        {
            ActionLeft();
        }
        if(Input.GetKeyDown(rightKey))
        {
            ActionRight();
        }

    }

    #region Actions with object
    private void InteractAction()
    {
        if (target != null)
        {
            
            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.Interact();
            }

        }
    }

    private void ActionUp()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.UpAction();
            }

        }
    }

    private void ActionDown()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.DownAction();
            }

        }
    }

    private void ActionLeft()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.LeftAction();
            }

        }
    }

    private void ActionRight()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.RightAction();
            }

        }
    }

    private void ActionStart()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.StartAction(character);
            }

        }
    }

    private void ActionEnd()
    {
        if (target != null)
        {

            if (target.GetComponent<InteractionObject>() != null)
            {
                InteractionObject interactionObject = target.GetComponent<InteractionObject>();
                interactionObject.EndAction();
            }

        }
    }
    #endregion

    /// <summary>
    /// Method updates the target field with the closest object which is in reach 
    /// or with a null if there are no objects in reach
    /// </summary>
    private void CheckForTarget()
    {
        if (availableTargets.Count > 0)
        {
            targetHolder = FindClosestTarget();
            if (target == targetHolder)
            {
                return;
            }
            else
            {
                ActionEnd();
                target = targetHolder;
                ActionStart();
            }
        }
        else
        {
            ActionEnd();
            target = null;
        }
    }


    /// <summary>
    /// Mechod finds the closest object to the player
    /// </summary>
    /// <returns></returns>
    private GameObject FindClosestTarget()
    {
        bool firstSet = false;
        float minDist = 0;
        GameObject closestTarget = measurementPoint.gameObject;
        foreach(GameObject target in availableTargets)
        {
            if(firstSet)
            {
                if(Vector2.Distance(target.transform.position, measurementPoint.position) < minDist)
                {
                    minDist = Vector2.Distance(target.transform.position, measurementPoint.position);
                    closestTarget = target;
                }
            }
            else
            {
                minDist = Vector3.Distance(target.transform.position, measurementPoint.position);
                closestTarget = target;
            }
        }
        return closestTarget;
    }

}
