using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryJigsaw : MonoBehaviour
{

    public List<Piece> inventoryPieces;
    public List<GameObject> inventoryPlaces;

   
    ////Return piece to inventory by dragging it over the inventory and letting go
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<Piece>() != null)
    //    {
    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            collision.GetComponent<Piece>().ReturnToInventory();
    //        }
    //    }
        
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<Piece>() != null)
    //    {
    //        Debug.Log("Removin from Inventory");
    //        collision.GetComponent<Piece>().RemoveFromInventory();
    //    }
    //}
}
