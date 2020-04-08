using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Item))]
public abstract class Interactable : MonoBehaviour
{ 
    public Image interactionIcon;

    protected TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start(){
        this.enabled = false;
    }

    public virtual void Interact(){
        //wird überschrieben
    }

    
    public virtual void ShowInfo(bool allPrerequsComplete)
    {
        if (allPrerequsComplete)
        {
            if (interactionIcon != null)
            {
                interactionIcon.gameObject.SetActive(true);
            }
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

    protected void EnableInfoCanvas()
    {
        //    Reference.instance.objectInfoDisplay.gameObject.SetActive(true);
        //    displayText = Reference.instance.objectInfoDisplay.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        //    Reference.instance.objectInfoDisplay.gameObject.transform.LookAt(Reference.instance.player);
    }
}
