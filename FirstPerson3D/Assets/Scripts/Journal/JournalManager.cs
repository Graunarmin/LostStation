using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    public Image background;
    public Button next;
    public Button prev;
    public Canvas updateInfoCanvas;
    private List<PageInfo> allPages = new List<PageInfo>();
    public PageInfo currentPage;

    // Start is called before the first frame update
    void Awake()
    {
        //Subscribe to the event that new information can be added to the journal
        GameManager.OnNewJournalInfo += UpdateJournal;
        allPages.Add(currentPage);

    }

    public void OpenJournal()
    {
        //Debug.Log("Opening Journal");
        CheckButtons();
        if (Reference.instance.journal.gameObject.activeInHierarchy)
        {
            CloseJournal();
        }
        else
        {
            GameManager.gameManager.SwitchCameras("2D");
            Reference.instance.journal.gameObject.SetActive(true);
            //show currentPage
            background.GetComponent<Image>().sprite = currentPage.pagePic;
        }

    }

    public void CloseJournal()
    {
        if (!GameManager.gameManager.InspectorOpen())
        {
            GameManager.gameManager.SwitchCameras("3D");
        }
        Reference.instance.journal.gameObject.SetActive(false);
    }

    public void UpdateJournal(JournalPage page)
    {
        Debug.Log("Update Journal");
        //show update Symbol on User Interface
        //...

        //if page is not already visible, we add it to the journal
        if (!JournalTracker.journalTracker.journalPages[page])
        {
            JournalTracker.journalTracker.journalPages[page] = true;
            //if the other page was already visible, we now see the double page
            if (JournalTracker.journalTracker.journalPages[page.otherPage])
            {
                currentPage = page.doublePage;
                Debug.Log("Add " + page.gameObject.name);
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
                Debug.Log("Add " + page.gameObject.name);
                currentPage = page.info;
                allPages.Add(currentPage);
                allPages.Sort(new SortPagesByNumber());
            }
            StartCoroutine(ShowUpdateIcon());
        }
    }

    public void ShowNextPage()
    {
        //Debug.Log("Next Page");
        currentPage =  allPages[allPages.IndexOf(currentPage) + 1];
        background.GetComponent<Image>().sprite = currentPage.pagePic;
        CheckButtons();

    }

    public void ShowPreviousPage()
    {
        //Debug.Log("Previous Page");
        currentPage = allPages[allPages.IndexOf(currentPage) - 1];
        background.GetComponent<Image>().sprite = currentPage.pagePic;
        CheckButtons();
    }

    private bool HasFollowingPages()
    {
        if(allPages.IndexOf(currentPage) != allPages.Count-1)
        {
            return true;
        }
        return false;
    }

    private bool HasPreviousPages()
    {
        if (allPages.IndexOf(currentPage) != 0)
        {
            return true;
        }
        return false;
    }

    private void CheckButtons()
    {
        if (!HasFollowingPages())
        {
            //Hide "Next" Button
            next.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            next.transform.localScale = new Vector3(1, 1, 1);
        }
        if (!HasPreviousPages())
        {
            //Hide "Previous" Button
            prev.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            prev.transform.localScale = new Vector3(1, 1, 1);
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
            => !GameManager.gameManager.CurrentlyInteracting());

        //show pop-up that journal was updated
        updateInfoCanvas.gameObject.SetActive(true);
        //PlaySound
        //...

        yield return new WaitForSeconds(2);

        //hide pop-up
        updateInfoCanvas.gameObject.SetActive(false);
    }
}
