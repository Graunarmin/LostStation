using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Notebook : MonoBehaviour
{
    [SerializeField] protected Button next;
    [SerializeField] protected Button prev;
    [SerializeField] protected List<PageInfo> allPages = new List<PageInfo>();
    [SerializeField] protected Image background;
    [SerializeField] private TextMeshProUGUI pageText;
    [SerializeField] protected PageInfo currentPage;

    public virtual void OpenNotebook()
    {
        //Debug.Log("Open Notebook");
        CheckButtons();
    }

    public virtual void CloseNotebook()
    {
        if(!GameManager.gameManager.JournalOpen() && !GameManager.gameManager.InventoryOpen())
        {
            GameManager.gameManager.SwitchCameras("3D");
        }

        //close the canvas (override)
    }

    public virtual void ShowNextPage()
    {
        //Debug.Log("Next Page");
        currentPage = allPages[allPages.IndexOf(currentPage) + 1];
        background.GetComponent<Image>().sprite = currentPage.pagePic;
        CheckButtons();
        if(pageText != null)
        {
            UpdateText();
        }
    }

    public virtual void ShowPreviousPage()
    {
        //Debug.Log("Previous Page");
        currentPage = allPages[allPages.IndexOf(currentPage) - 1];
        background.GetComponent<Image>().sprite = currentPage.pagePic;
        CheckButtons();
        if (pageText != null)
        {
            UpdateText();
        }
    }

    protected virtual bool HasFollowingPages()
    {
        if (allPages.IndexOf(currentPage) != allPages.Count - 1)
        {
            return true;
        }
        return false;
    }

    protected virtual bool HasPreviousPages()
    {
        if (allPages.IndexOf(currentPage) != 0)
        {
            return true;
        }
        return false;
    }

    protected virtual void CheckButtons()
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

    protected virtual void UpdateText()
    {
        if (pageText.transform.parent.gameObject.activeInHierarchy)
        {
            pageText.text = currentPage.pageText;
        }
    }

    public virtual void ShowText(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        
        if(pointerData.button == PointerEventData.InputButton.Left)
        {
            if (!pageText.transform.parent.gameObject.activeInHierarchy)
            {
                pageText.transform.parent.gameObject.SetActive(true);
                pageText.text = currentPage.pageText;
            }
            else
            {
                HideText();
            }
        }
    }

    public virtual void HideText()
    {
        //Debug.Log("Hiding Text");
        pageText.text = "";
        pageText.transform.parent.gameObject.SetActive(false);
    }
}
