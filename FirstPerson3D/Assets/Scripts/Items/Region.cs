using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    
    public List<Item> containedItems = new List<Item>();
    
    //public List<Lightning> lights = new List<Lightning>();
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hi, I'm entering the " + name);

        //add Region to List of current Regions
        Reference.instance.currentRegions.Add(this);

        //switch on colliders of all contained Items
        SetContainedItems(true);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("And now I'm leaving the " + name);

        //set current Region to null
        Reference.instance.currentRegions.Remove(this);

        //switch off colliders of all contained Items
        SetContainedItems(false);
    }


    public void SetContainedItems(bool colliderValue)
    {
        foreach(Item item in containedItems){

            var hasPrerequ = item.GetComponent<Prerequisite>();
            //if item has a collider:
            if (item.col != null){
                //if it has a Prerequisite which is about acces
                if (hasPrerequ && hasPrerequ.itemAccess){
                    //the prerequisite has to be met to enable the collider
                    if (hasPrerequ.Complete){
                        item.col.enabled = colliderValue;
                        Debug.Log("ItemAccess granted");
                    }

                }else{
                    //turn collider on or off (depending in entering or exiting the region
                    item.col.enabled = colliderValue;
                }
                
            }
        }
    }
}
