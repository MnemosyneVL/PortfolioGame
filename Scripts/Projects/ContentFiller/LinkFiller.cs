using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkFiller : MonoBehaviour
{
    [Header("LinksDiv")]
    public Button gitHubButton;
    public Button link1Button;
    public Link gitHubLink;
    public Link otherLink;
    public Text gitHubText;
    public Text otherText;

    public Color baseGitColor;
    public Color baseOtherColor;
    public Sprite defaultImage;

    [Header("SelectorFields")]
    public Sprite selectorHyperlink;

    private LinkItem[] links;
    private LinkVerificationScript linkVerifierCanvas;

    public void SetContent(LinkItem[] items)
    {
        links = items;
    }

    public void SetLinkVerifier(LinkVerificationScript reference)
    {
        linkVerifierCanvas = reference;
    }

    public void CheckContent(ref ProjectInteractivesPositions interactivesPositions)
    {
        if(links == null)
        {
            Debug.Log("No Links set");
            return;
        }
        interactivesPositions.linkAmount = links.Length;
        if(links.Length > 0)
        {
            interactivesPositions.needLinkSelector = true;
        }
        FillContent();
    }

    public void FillContent()
    {
        if (links.Length > 0)
        {

            if(links.Length > 0)
            {
                gitHubLink.link = links[0].link;
                if (links[0].image != null)
                {
                    gitHubButton.image.sprite = links[0].image;
                }
                if (links[0].description != null)
                {
                    gitHubText.text = links[0].description;
                }
                else
                {
                    gitHubText.text = " ";
                }
                if (links[0].color != null)
                {
                    gitHubButton.image.color = links[0].color;
                }
                else
                {
                    gitHubButton.image.color = baseGitColor;
                }
            }
            else
            {
                gitHubLink.gameObject.SetActive(false);
            }
            if (links.Length >1)
            {
                otherLink.link = links[1].link;
                if (links[1].image != null)
                {
                    link1Button.image.sprite = links[1].image;
                }
                else
                if (links[0].description != null)
                {
                    otherText.text = links[1].description;
                }
                else
                {
                    otherText.text = " ";
                }
                if (links[0].color != null)
                {
                    link1Button.image.color = links[1].color;
                }
                else
                {
                    link1Button.image.color = baseGitColor;
                }
            }
            else
            {
                link1Button.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("No links set to the project");
        }
    }

    #region
    public void UseFirstLink()
    {
        if (gitHubLink.link != null)
        {
            linkVerifierCanvas.DeliverLink(gitHubText.text, gitHubLink.link);
        }
    }

    public void UseSecondLink()
    {
        if (otherLink.link != null)
        {
            linkVerifierCanvas.DeliverLink(otherText.text, otherLink.link);
        }
    }
    #endregion
}
