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

        //Play Sound
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightFilter);
        //change color of flashlight
        Reference.instance.flashlight.ChangeColor( new Color(255/255, 194/255, 182/255, Reference.instance.flashlight.GetAlpha()));
        Reference.instance.flashlight.filterOn = true;

        //start timer until Unequip
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        //wait until Player switches on Flashlight
        yield return new WaitUntil(()
            => Reference.instance.flashlight.SwitchedOn());

        //then they have 50 Seconds until Crack
        yield return new WaitForSeconds(40);
        ShowCracks();

        //and 30 with crack
        yield return new WaitForSeconds(30);
        AlienFilterBroken();
    }

    private void ShowCracks()
    {
        Debug.Log("Be careful, time runs out!");
        //Play Sound
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightFilterCrack);
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

        //Play Sound
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightFilterBroken);
        UnequipFilter();
    }

    public void UnequipFilter()
    {
        Debug.Log("Unequipping the Filter!");
        //stop timer
        StopAllCoroutines();
        //change color of flashlight back to normal
        Reference.instance.flashlight.ResetColor();
        Reference.instance.flashlight.filterOn = false;

        //remove crack
        crack.gameObject.SetActive(false);

        //put frame into inventory
        InventoryManager.invManager.AddItem(frame);
    }
}
