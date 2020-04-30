using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{

    #region Singleton
    public static AlienManager mrSaru;
    private void Awake()
    {

        if (mrSaru == null)
        {
            mrSaru = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of Mr. Saru!");
        }
    }
    #endregion
    [SerializeField] LightSwitch lightSwitch;

    [SerializeField] List<Alien> aliens = new List<Alien>();
    [SerializeField] Region alienRegion;
    [SerializeField] JournalPage journalPage;
    private List<Alien> collectedAliens = new List<Alien>();

    public delegate void FoundAllAliens();
    public static event FoundAllAliens OnFoundAllAliens;

    private void Start()
    {
        foreach(Alien alien in aliens)
        {
            alien.gameObject.SetActive(false);
        }
        CraftingManager.OnFilterEquipped += TestForLights;
        //CraftingManager.OnFilterEquipped += MakeAliensVisible;
        FilterTimer.OnFilterBroken += PutAliensBack;
    }

    private void TestForLights()
    {
        //Aliens are only visible if flashlight is on
        StartCoroutine(WaitForFlashlightOn());
    }

    private IEnumerator WaitForFlashlightOn()
    {
        yield return new WaitUntil(()
            => Reference.instance.flashlight.SwitchedOn());

        //Then we can make all aliens visible
        MakeAliensVisible();
        //and start counting the collected aliens
        StartCoroutine(CountAliens());
    }

    private void MakeAliensVisible()
    {
        //make all aliens visible
        foreach(Alien alien in aliens)
        {
            alien.gameObject.SetActive(true);
        }
        alienRegion.EnterRegion();
    }

    private void PutAliensBack()
    {
        MakeAliensInvisible();
        //stop counting the aliens
        StopCoroutine(CountAliens());
        foreach (Alien alien in collectedAliens)
        {
            InventoryManager.invManager.RemoveItem(alien);
        }
        //and clear the List of collected aliens
        collectedAliens.Clear();
    }

    private void MakeAliensInvisible()
    {
        //stop waiting for the lights to be switched off
        StopCoroutine(WaitForFlashlightOn());

        //disable colliders
        alienRegion.ExitRegion();

        //make all aliens invisible
        foreach (Alien alien in aliens)
        {
            alien.gameObject.SetActive(false);
        }
    }

    private IEnumerator CountAliens()
    {
        yield return new WaitUntil(()
            => collectedAliens.Count == 3);

        if(OnFoundAllAliens != null)
        {
            //unsubscribe the methods because all aliens have been found
            CraftingManager.OnFilterEquipped -= MakeAliensVisible;
            FilterTimer.OnFilterBroken -= PutAliensBack;
            //tell everyone that all aliens have been found
            OnFoundAllAliens();
            //add the journal Page
            GameManager.gameManager.FireNewJournalEntry(journalPage);
        }
    }

    public void AddAlien(Alien alien)
    {
        collectedAliens.Add(alien);
    }

    public void PauseFlashlight(bool pause)
    {
        if (pause)
        {
            MakeAliensInvisible();
        }
        else
        {
            MakeAliensVisible();
        }
    }
}
