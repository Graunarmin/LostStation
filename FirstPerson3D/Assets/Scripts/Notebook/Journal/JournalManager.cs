using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : Notebook
{

    #region Singleton
    public static JournalManager journalManager;
    private void Awake()
    {

        if (journalManager == null)
        {
            journalManager = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of Journal!");
        }

    }
    #endregion

    public Canvas updateInfoCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //Subscribe to the event that new information can be added to the journal
        GameManager.OnNewJournalInfo += UpdateJournal;
        allPages.Add(currentPage);

    }

    public override void OpenNotebook()
    {
        //Debug.Log("Opening Journal");
        //CheckButtons();
        //base.OpenNotebook();
        if (GameManager.gameManager.JournalOpen())
        {
            CloseNotebook();
        }
        else
        {
            base.OpenNotebook();
            GameManager.gameManager.SwitchCameras("2D");
            Reference.instance.journal.gameObject.SetActive(true);
            //show currentPage
            background.GetComponent<Image>().sprite = currentPage.pagePic;
        }

    }

    public override void CloseNotebook()
    {
        Reference.instance.journal.gameObject.SetActive(false);
        base.CloseNotebook();
        
    }

    public void UpdateJournal(JournalPage page)
    {
        //Debug.Log("Update Journal");

        //if page is not already visible, we add it to the journal
        if (!JournalTracker.journalTracker.journalPages[page])
        {
            JournalTracker.journalTracker.journalPages[page] = true;
            //if the other page was already visible, we now see the double page
            if (JournalTracker.journalTracker.journalPages[page.otherPage])
            {
                currentPage = page.doublePage;
                //Debug.Log("Add " + page.gameObject.name);
                //replace single page by double page
                foreach(PageInfo p in allPages)
                {
                    if(p.pageNumber == currentPage.pageNumber)
                    {
                        allPages.Remove(p);
                        break;
                    }
                }
                allPages.Add(currentPage);
                allPages.Sort(new SortPagesByNumber());
            }
            //if this is the first of the two pages to be visible, we see only this one
            else
            {
                //Debug.Log("Add " + page.gameObject.name);
                currentPage = page.info;
                allPages.Add(currentPage);
                allPages.Sort(new SortPagesByNumber());
            }
            StartCoroutine(ShowUpdateIcon());
        }
    }

    public class SortPagesByNumber : Comparer<PageInfo>
    {
        public override int Compare(PageInfo x, PageInfo y)
        {
            return x.pageNumber.CompareTo(y.pageNumber);
        }
    }

    private IEnumerator ShowUpdateIcon()
    {
        //wait until the player is back in the game
        yield return new WaitUntil(()
            => !GameManager.gameManager.CurrentlyInteracting()
            || Reference.instance.elevatorControls.gameObject.activeInHierarchy);

        //show pop-up that journal was updated
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.newJournalPage);
        updateInfoCanvas.gameObject.SetActive(true);
        //PlaySound
        //...

        yield return new WaitForSecondsRealtime(2.5f);

        //hide pop-up
        updateInfoCanvas.gameObject.SetActive(false);
    }
}
