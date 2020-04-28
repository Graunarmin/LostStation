using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : ItemContainerManager
{
    #region Singleton
    public static PortalManager portal;
    private void Awake()
    {

        if (portal == null)
        {
            portal = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of PortalManager!");
        }
        slots = new ItemSlot[] {null};
    }
    #endregion

    [SerializeField] PortalPanel portalPanel;
    [SerializeField] PortalControlsCanvas controlPanel;
    [SerializeField] EndOfGame end;
    [SerializeField] CorrectSolutionReaction reaction;
    public Texture2D hoverCursorLastPost;
    public GameObject mousePointerHand;


    private AlienSlot activeSlot;

    protected override void Start()
    {
        container = new PortalContainer();
        slots = portalPanel.GetPillars();
    }

    //Gets the active slot from PortalPanel
    public void SetSlot(ItemSlot slot)
    {
        activeSlot = slot as AlienSlot;
    }

    public AlienSlot ActiveSlot()
    {
        return activeSlot;
    }

    public string ActiveSlotName()
    {
        Debug.Log("Active Slot: " + activeSlot.slotName);
        return activeSlot.slotName;
    }

    public override bool AddItem(Item item)
    {
        if (container.AddItem(item, GetIndexOfActiveSlot()))
        {
            UpdateUI(item, true);
            return true;
        }
        return false;
    }

    public override void RemoveItem(Item item)
    {
        container.RemoveItem(item);
        UpdateUI(null, false);
    }

    public override void RemoveItem(ItemAsset item)
    {
        container.RemoveItem(item);
        UpdateUI(null, false);
    }

    protected override void UpdateUI(Item item, bool add)
    {
        if (add)
        {
            slots[GetIndexOfActiveSlot()].AddItemToSlot(item);
        }
        else
        {
            slots[GetIndexOfActiveSlot()].ClearSlot();
        }
    }

    //Get the index of where the active Slot is in the slots array
    private int GetIndexOfActiveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i] == activeSlot)
            {
                return i;
            }
        }
        return -1;
    }

    //opens up canvas with controls
    public void OpenControls()
    {
        //Close the panel behind
        InventoryManager.invManager.CloseInventory();
        //open up canvas with controls
        controlPanel.Activate();
        controlPanel.ActivateControls();
    }

    //Checks the solution after the player hit die "Load" Button
    public void CheckSolution()
    {
        if (AliensPlacedCorrectly() && CorrectOrder())
        {
            Debug.Log("Loading ...");
            controlPanel.ActivateLoadingScreen();
            StartCoroutine(LoadPortal());

        }
        else if(AliensPlacedCorrectly() && !CorrectOrder())
        {
            Debug.Log("Error in Order!");
            controlPanel.ThrowErrorOrder();
            StartCoroutine(HideError());
        }
        else
        {
            Debug.Log("ERROR");
            controlPanel.ThrowError();
            StartCoroutine(HideError());
        }
    }

    //checks if all three aliens have been inserted into a pillar
    public bool AllAliensInside()
    {
        return (container.Size() == 3);
    }

    //checks if each alien is in it's repective pillar
    //if everyone is in the right place and the player opened the
    //controls it's obvious they did it from the earth pillar
    private bool AliensPlacedCorrectly()
    {
        return (portalPanel.AliensInRightSlot());
    }

    //checks if all aliens have been inserted in the correct orde
    private bool CorrectOrder()
    {
        List<Item> testList = container.GetContainer();
        if((testList[0].itemInfo.itemName == "FireAlien" || testList[0].itemInfo.itemName == "WaterAlien")
            && (testList[1].itemInfo.itemName == "FireAlien" || testList[1].itemInfo.itemName == "WaterAlien")
            && testList[2].itemInfo.itemName == "AirAlien")
        {
            return true;
        }
        return false;
    }

    private IEnumerator LoadPortal()
    {
        yield return new WaitForSecondsRealtime(5);

        //close the loadingScreen
        controlPanel.Close();

        //Animation
        if (reaction != null)
        {
            reaction.React();
        }

        //End the game and roll the credits:
        yield return new WaitForSecondsRealtime(35);
        end.EndGame();
    }

    private IEnumerator HideError()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        controlPanel.Close();
    }
}
