using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    [SerializeField] Item frame;

    private void FilterEquipped()
    {
        //change color of flashlight
        Reference.instance.flashlight.color = new Color(255, 194, 182, Reference.instance.flashlight.color.a);

        //make the aliens visible

        //start timer until Unequip
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(120);
        ShowCracks();

        yield return new WaitForSeconds(30);

        UnequipFilter();

    }

    private void ShowCracks()
    {
        Debug.Log("Be careful, time runs out!");
        //show first cracks in Lense
    }

    public void UnequipFilter()
    {
        Debug.Log("Unequipping the Filter!");
        //stop timer
        StopCoroutine(Timer());

        //make aliens invisible

        //change color of flashlight back to normal
        Reference.instance.flashlight.color = new Color(253, 240, 161, Reference.instance.flashlight.color.a);

        //remove crack

        //put frame into inventory
        InventoryManager.invManager.AddItem(frame);
    }


}
