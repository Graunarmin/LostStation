using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Item))]
public abstract class Interactable : MonoBehaviour
{
    public string infoTextActive;
    public string infoTextInactive;
    protected TextMeshProUGUI displayText;

    public Image interactionIcon;

    // Start is called before the first frame update
    void Start(){
        this.enabled = false;
    }

    public virtual void Interact(){
        //wird überschrieben

        //Debug.Log("interacting with " + name);
    }

    protected void EnableInfoCanvas()
    {
    //    Reference.instance.objectInfoDisplay.gameObject.SetActive(true);
    //    displayText = Reference.instance.objectInfoDisplay.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    //    Reference.instance.objectInfoDisplay.gameObject.transform.LookAt(Reference.instance.player);
    }

    public virtual void ShowInfo(Prerequisite hasPrereq)
    {
        //EnableInfoCanvas();

        //is not clickable bc. Prerequ is not met
        if(hasPrereq && !hasPrereq.Complete)
        {
            //if(infoTextInactive != "")
            //{
            //    displayText.text = infoTextInactive;
            //}
        //is clickable bc. Prerequ is met
        }else if(!hasPrereq || (hasPrereq && hasPrereq.Complete))
        {
            if(interactionIcon != null)
            {
                interactionIcon.gameObject.SetActive(true);
            }
            
            //if (infoTextActive != "")
            //{
            //    displayText.text = infoTextActive;
            //}
        }
    }

    public virtual void HideInfo()
    {
        if (interactionIcon != null)
        {
            interactionIcon.gameObject.SetActive(false);
        }

        //Reference.instance.objectInfoDisplay.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        //Reference.instance.objectInfoDisplay.gameObject.gameObject.SetActive(false);
    }


}
