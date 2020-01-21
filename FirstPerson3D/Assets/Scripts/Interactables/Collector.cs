using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collector : Interactable
{

    [HideInInspector]
    public Collectable collectableItem;

    private void Awake()
    {
        collectableItem = GetComponent<Collectable>();
    }

    public override void Interact()
    {
        PickUpItem();
    }

    public void PickUpItem()
    {
        //Hide the Object Information
        HideInfo();
        //Inspect Object before we add it to the backpack
        InspectObject();

        bool wasPickedUp =
            InventoryManager.invManager.AddItem(gameObject.GetComponent<Collectable>());

        //and set it to inactive so it is no longer visible or accessible
        if (wasPickedUp)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void InspectObject()
    {
        //"duplicate" the clicked on Object
        GameObject item = Instantiate(gameObject);

        //set the rig as the parent (as in the unity history)
        item.transform.SetParent(Reference.instance.obsCam.rig);

        //bring it to the middle of the observer Camera's Screen
        item.transform.localPosition = Vector3.zero;

        //push the model of the Prop down so the pivot is aligned with the "actual" Prop
        item.transform.GetChild(0).localPosition = Vector3.zero;

        Reference.instance.obsCam.model = item.transform;

        //turn on observer Camera
        Reference.instance.obsCam.Activate();
    }
}
