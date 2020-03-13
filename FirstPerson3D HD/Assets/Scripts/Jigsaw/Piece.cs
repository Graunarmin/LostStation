using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //public to control stuff, make private later
    public bool pieceLocked;
    public bool piecePickedUp;

    public Socket socket;
    public GameObject invPos;

    public float transparency = 0.75f;

    List<RaycastResult> results = new List<RaycastResult>();

    public void OnDrag(PointerEventData eventData)
    {
        if (piecePickedUp)
        {
            //Debug.Log("My Position: " + transform.position);
            transform.position = Reference.instance.camera2D.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 100f);
            //Debug.Log("My new Position: " + transform.position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if the piece is not already locked we can interact with it
        if (!pieceLocked)
        {
            //do raycast to detect if we are over the right socket
            results.Clear();
            results = JigsawManager.jigsawManager.DoRaycast();
            Socket hitSocket = FindSocketHit();
            InventoryJigsaw inventory = FindInventoryHit();

            //let go of piece over sth that is not the inventory:
            //remove piece from inventory and inventory list
            if (inventory == null)
            {
                RemoveFromInventory();

                //let go over socket
                if (hitSocket != null)
                {
                    if (hitSocket == socket)
                    {
                        PlacePiece(socket.transform.position);
                        socket.GetComponent<Image>().raycastTarget = false;
                    }
                    else if (hitSocket != socket)
                    {
                        WrongPiece();
                    }
                }
            }
            // if placed anywhere over inventory: put piece back to inventory
            else if (inventory != null)
            {
                ReturnToInventory();
            }
        }
    }

    #region find Raycast-Hits
    Socket FindSocketHit()
    {
        Socket hitSocket;

        foreach (RaycastResult result in results)
        {
            //Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.GetComponent<Socket>() != null)
            {
                //should only be one socket as they don't overlap
                hitSocket = result.gameObject.GetComponent<Socket>();
                //Debug.Log("Hit Socket");
                return hitSocket;
            }
        }
        return null;
    }

    InventoryJigsaw FindInventoryHit()
    {
        InventoryJigsaw inventory;

        foreach (RaycastResult result in results)
        {
            //Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.GetComponent<InventoryJigsaw>() != null)
            {
                //should only be one inventory as there is only one
                inventory = result.gameObject.GetComponent<InventoryJigsaw>();
                //Debug.Log("Hit Inventory");
                return inventory;
            }
        }
        return null;
    }
    #endregion

    #region piece interaction

    //if a piece is not locked it can be picked up
    public void PickUpPiece()
    {
        if (!pieceLocked)
        {
            JigsawManager.jigsawManager.currentPiece = this;
            piecePickedUp = true;
            //So the piece picked up is always on top of the others
            transform.SetAsLastSibling();
        }
    }


    public void SetDownPiece()
    {
        piecePickedUp = false;
    }


    public void PlacePiece(Vector3 newPosition)
    {
        SetDownPiece();
        transform.position = newPosition;
        GetComponent<Image>().raycastTarget = false;
        socket.GetComponent<Image>().raycastTarget = false;
        pieceLocked = true;
        JigsawManager.jigsawManager.piecesLocked.Add(this);

        //Fedback-Effects:
        //Instantiate(JigsawManager.jigsawManager.edgeParticles, newPosition, JigsawManager.jigsawManager.edgeParticles.rotation);
        //Play(sound)
        //change color back to normal
        GetComponent<Image>().color = new Color(1, 1, 1, 1);

        JigsawManager.jigsawManager.currentPiece = null;
    }

    public void WrongPiece()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, transparency);
    }

    public void ReturnToInventory()
    {
        if (!pieceLocked)
        {
            SetDownPiece();
            GetComponent<RectTransform>().position = invPos.GetComponent<RectTransform>().position;
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //Debug.Log("Adding to List: " + name);
            JigsawManager.jigsawManager.inventory.inventoryPieces.Add(this);
            //Change Parent of piece  back to Inventory.Pieces
            transform.SetParent(JigsawManager.jigsawManager.inventoryPieces);
            JigsawManager.jigsawManager.currentPiece = null;
        }
    }

    public void RemoveFromInventory()
    {
        //Debug.Log("Removing from List: " + name);
        JigsawManager.jigsawManager.inventory.inventoryPieces.Remove(this);
        //Change Parent of piece from Inventory.Pieces to Playfield.Pieces
        //so it is not scrolled with the inventory
        transform.SetParent(JigsawManager.jigsawManager.playfieldPieces);
    }
    #endregion

}
