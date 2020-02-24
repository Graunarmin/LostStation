using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterTimer : MonoBehaviour
{
    #region Singleton
    public static FilterTimer timer;
    private void Awake()
    {

        if (timer == null)
        {
            timer = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of Timer!");
        }
    }
    #endregion

    private void Start()
    {
        CraftingManager.OnFilterEquipped += FilterEquipped;
        AlienManager.OnFoundAllAliens += UnequipFilter;
    }

    public delegate void FilterBroken();
    public static event FilterBroken OnFilterBroken;

    [SerializeField] Item frame;
    [SerializeField] Canvas crack;

    private void FilterEquipped()
    {
        //make aliens visible:
        //aliens are listening to OnFilterEquipped as well, nothing to do here

        //change color of flashlight
        Reference.instance.flashlight.color = new Color(255/255, 194/255, 182/255, Reference.instance.flashlight.color.a);

        //start timer until Unequip
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(60);
        ShowCracks();

        yield return new WaitForSeconds(30);

        AlienFilterBroken();

    }

    private void ShowCracks()
    {
        Debug.Log("Be careful, time runs out!");

        //show first cracks in Lense
        crack.gameObject.SetActive(true);
    }

    private void AlienFilterBroken()
    {
        //make aliens invisible
        //and stop coroutine counting them
        if (OnFilterBroken != null)
        {
            OnFilterBroken();
        }

        UnequipFilter();
    }

    public void UnequipFilter()
    {
        Debug.Log("Unequipping the Filter!");

        //stop timer
        StopCoroutine(Timer());

        //change color of flashlight back to normal
        Reference.instance.flashlight.color = new Color(253/255, 240/255, 161/255, Reference.instance.flashlight.color.a);

        //remove crack
        crack.gameObject.SetActive(true);

        //put frame into inventory
        InventoryManager.invManager.AddItem(frame);
    }
}
