using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDoorCanvas : MonoBehaviour, IPuzzleCanvas
{
    public Collectable crowbar;
    //The page that get's added in case the crowbar was not collected
    public JournalPage journalPage;

    public GameObject content;

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(ActivatePopUp());
    }

    public void Close()
    {
        gameObject.SetActive(false);
        content.SetActive(false);
    }

    private IEnumerator ActivatePopUp()
    {
        yield return new WaitForSecondsRealtime(1);
        content.SetActive(true);
    }

    public void TryAgain()
    {
        Close();
    }

    public void Smash()
    {
        if(InventoryManager.invManager.GetContainerSize() > 0)
        {
            if (InventoryManager.invManager.ContainerContainsItem(crowbar))
            {
                Debug.Log("Crowbar collected");
                Reference.instance.keyPad.SmashedKeyPad();
            }
            else
            {
                Debug.Log("Crowbar not collected");
                Reference.instance.keyPad.Close();
                //Add Journalpage "ouw"
                GameManager.gameManager.FireNewJournalEntry(journalPage);
            }
        }
        else
        {
            Debug.Log("Crowbar not collected");
            Reference.instance.keyPad.Close();
            //Add Journalpage "ouw"
            GameManager.gameManager.FireNewJournalEntry(journalPage);
        }
        Close();
        
    }

    public void Leave()
    {
        Reference.instance.keyPad.Close();
        Close();
    }
}
