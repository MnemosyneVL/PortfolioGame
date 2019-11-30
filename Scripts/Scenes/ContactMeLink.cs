using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContactMeLink 
{
    public string linkAdress;
    public string description;
    public Action selectorAction;
    public Action buttonAction;
    public Link linkRef;
    private LinkVerificationScript linkVerification;

    public ContactMeLink()
    {
        selectorAction = OpenLinkwCanvas;
        buttonAction = OpenLink;
    }

    public void SetLinkVerification(ref LinkVerificationScript l_verificationScript)
    {
        linkVerification = l_verificationScript;
    }

    public void OpenLinkwCanvas()
    {
        linkRef.link = linkAdress;
        linkVerification.DeliverLink(description, linkRef.link);
    }

    public void OpenLink()
    {
        linkRef.link = linkAdress;
    }
}
