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
        base.OpenNotebook();
        if (Reference.instance.journal.gameObject.activeInHierarchy)
        {
            CloseNotebook();
        }
        else
        {
            GameManager.gameManager.SwitchCameras("2D");
            Reference.instance.journal.gameObject.SetActive(true);
            //show currentPage
            background.GetComponent<Image>().sprite = currentPage.pagePic;
        }

    }

    public override void CloseNotebook()
    {
        //if (!GameManager.gameManager.InspectorOpen())
        //{
        //    GameManager.gameManager.SwitchCameras("3D");
        //}
        base.CloseNotebook();
        Reference.instance.journal.gameObject.SetActive(false);
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

    //public override void ShowNextPage()
    //{
    //    //Debug.Log("Next Page");
    //    currentPage =  allPages[allPages.IndexOf(currentPage) + 1];
    //    background.GetComponent<Image>().sprite = currentPage.pagePic;
    //    CheckButtons();

    //}

    //public override void ShowPreviousPage()
    //{
    //    //Debug.Log("Previous Page");
    //    currentPage = allPages[allPages.IndexOf(currentPage) - 1];
    //    background.GetComponent<Image>().sprite = currentPage.pagePic;
    //    CheckButtons();
    //}

    //protected override bool HasFollowingPages()
    //{
    //    if(allPages.IndexOf(currentPage) != allPages.Count-1)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //protected override bool HasPreviousPages()
    //{
    //    if (allPages.IndexOf(currentPage) != 0)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //protected override void CheckButtons()
    //{
    //    if (!HasFollowingPages())
    //    {
    //        //Hide "Next" Button
    //        next.transform.localScale = new Vector3(0, 0, 0);
    //    }
    //    else
    //    {
    //        next.transform.localScale = new Vector3(1, 1, 1);
    //    }
    //    if (!HasPreviousPages())
    //    {
    //        //Hide "Previous" Button
    //        prev.transform.localScale = new Vector3(0, 0, 0);
    //    }
    //    else
    //    {
    //        prev.transform.localScale = new Vector3(1, 1, 1);
    //    }
    //}


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
            => !GameManager.gameManager.CurrentlyInteracting());

        //show pop-up that journal was updated
        updateInfoCanvas.gameObject.SetActive(true);
        //PlaySound
        //...

        yield return new WaitForSeconds(2.5f);

        //hide pop-up
        updateInfoCanvas.gameObject.SetActive(false);
    }
}
