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
        FilterTimer.OnFilterBroken += MakeAliensInvisible;
    }

    private void TestForLights()
    {
        //Aliens are only visible if all lights but filtered lights (flashlight)
        //are switched off, so we have to wait until the lights are switched off
        StartCoroutine(WaitForLightsOut());
    }

    private IEnumerator WaitForLightsOut()
    {
        yield return new WaitUntil(()
            => lightSwitch.LightsAreOff());

        //Then we can make all aliens visible
        MakeAliensVisible();
    }

    private void MakeAliensVisible()
    {
        //make all aliens visible
        foreach(Alien alien in aliens)
        {
            alien.gameObject.SetActive(true);
        }
        alienRegion.EnterRegion();

        //and start counting the collected aliens
        StartCoroutine(CountAliens());
    }

    private void MakeAliensInvisible()
    {
        //stop waiting for the lights to be switched off
        StopCoroutine(WaitForLightsOut());
        //and stop counting the aliens
        StopCoroutine(CountAliens());

        //disable colliders
        alienRegion.ExitRegion();

        //make all aliens invisible
        foreach (Alien alien in aliens)
        {
            alien.gameObject.SetActive(false);
        }

        foreach(Alien alien in collectedAliens)
        {
            InventoryManager.invManager.RemoveItem(alien);
        }
        //and clear the List of collected aliens
        collectedAliens.Clear();
       
    }

    private IEnumerator CountAliens()
    {
        yield return new WaitUntil(()
            => collectedAliens.Count == 3);

        if(OnFoundAllAliens != null)
        {
            //unsubscribe the methods because all aliens have been found
            CraftingManager.OnFilterEquipped -= MakeAliensVisible;
            FilterTimer.OnFilterBroken -= MakeAliensInvisible;
            //tell everyone that all aliens have been found
            OnFoundAllAliens();
        }
    }

    public void AddAlien(Alien alien)
    {
        collectedAliens.Add(alien);
    }
}
