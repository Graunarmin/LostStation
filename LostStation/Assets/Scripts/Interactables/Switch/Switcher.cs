using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Switcher : Interactable
{
    public bool state;
    public bool changeOnClick;
    //public string infoTextSwitchedOff;
    public Image switchOn;
    public Image switchOff;

    //event setup
    public delegate void OnStateChange();
    public event OnStateChange Change;

    public override void Interact()
    {
        if (changeOnClick)
        {
            ChangeState();
        }
        
    }

    public void ChangeState()
    {
        state = !state;

        //if(GetComponent<StateReactor>() != null){
        //    GetComponent<StateReactor>().React();
        //}

        //"I changed, everyone listening: do with that what you will"
        if (Change != null)
        {
            Change();
        }
    }

    public override void ShowInfo(bool allPrerequsComplete)
    {
        if (changeOnClick && allPrerequsComplete)
        {
            //switched on
            if (state)
            {
                if (switchOn != null && switchOff != null)
                {
                    switchOff.gameObject.SetActive(false);
                    switchOn.gameObject.SetActive(true);
                }
            }
            //switched off
            else
            {
                if (switchOn != null && switchOff != null)
                {
                    switchOff.gameObject.SetActive(true);
                    switchOn.gameObject.SetActive(false);
                }
            }

        }
    }

    public override void HideInfo()
    {
        if (switchOn != null && switchOff != null)
        {
            switchOn.gameObject.SetActive(false);
            switchOff.gameObject.SetActive(false);
        }
    }


    //public override void ShowInfo(Prerequisite hasPrereq)
    //{
    //    //EnableInfoCanvas();

    //    //is not clickable bc. Prerequ is not met
    //    //if (hasPrereq && !hasPrereq.Complete)
    //    //{
    //    //    if(infoTextInactive != "")
    //    //    {
    //    //        displayText.text = infoTextInactive;
    //    //    }
    //    //}
    //    //is clickable bc. Prerequ is met or it has none
    //    //else
    //    if (!hasPrereq || hasPrereq && hasPrereq.Complete)
    //    {
    //        //switched on
    //        if (state)
    //        {
    //            if(switchOn != null && switchOff != null)
    //            {
    //                switchOn.gameObject.SetActive(false);
    //                switchOff.gameObject.SetActive(true);
    //            }
    //            //if (infoTextSwitchedOn != "")
    //            //{
    //            //    displayText.text = infoTextSwitchedOn;
    //            //}
    //        }
    //        //switched off
    //        else
    //        {
    //            if (switchOn != null && switchOff != null)
    //            {
    //                switchOn.gameObject.SetActive(true);
    //                switchOff.gameObject.SetActive(false);
    //            }

    //            //if (infoTextActive != "")
    //            //{
    //            //    displayText.text = infoTextActive;
    //            //}
    //        }
            
    //    }
    //}

}
