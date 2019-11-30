using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactMeSceneSelectorStartUp : MonoBehaviour
{
    [Header("References")]
    public GameObject selectorObject;
    public LinkVerificationScript linkVerificationScript;
    public ScreenControlsHandler screenControls;
    public Sprite selectorHyperlink;
    [Header("Contacts")]
    public List<ContactMeLink> contacts = new List<ContactMeLink>(); 

    private void Start()
    {
        SetupSelector();
    }

    private void SetupSelector()
    {
        SelectorScript selectorScript = selectorObject.GetComponent<SelectorScript>();

        selectorScript.SetOnScreenControlsRef(screenControls);

        foreach (ContactMeLink contact in contacts)
        {
            contact.SetLinkVerification(ref linkVerificationScript);
            selectorScript.AddNewAction(contact.description, contact.selectorAction, selectorHyperlink);
            selectorScript.InicializeInteractive();
        }

        InteractionObject interactionObject = selectorObject.GetComponent<InteractionObject>();
        interactionObject.SetDelegateDown(selectorScript.NextAction);
        interactionObject.SetDelegateUp(selectorScript.PreviousAction);
        interactionObject.SetDelegateInteract(selectorScript.UseCurrentAction);
        interactionObject.SetDelegateStart(selectorScript.OnStartAction);
        interactionObject.SetDelegateEnd(selectorScript.OnEndAction);
    }

    public void OpenContact(int contactNr)
    {
        contacts[contactNr].buttonAction();
    }
}
