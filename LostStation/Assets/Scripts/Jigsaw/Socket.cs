using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public Piece puzzlePiece;

    //private void Start()
    //{
    //    Debug.Log("Socket Position of " + name + ": " + Reference.instance.camera2D.ScreenToWorldPoint(transform.position));
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Trigger entered!");
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Piece enteringPiece = collision.GetComponent<Piece>();
    //    Debug.Log(enteringPiece.name + " entering Collider of " + name);

    //    //if piece collides with it's socket:
    //    if ((this == enteringPiece.socket) && Input.GetMouseButtonUp(0))
    //    {
    //        //Debug.Log("Placing Piece");
    //        //put piece in correct position
    //        //Debug.Log("Socket Position: " + transform.position);
    //        enteringPiece.PlacePiece(transform.position);
    //        GetComponent<BoxCollider2D>().enabled = false;

    //    }
    //    //if piece collides with wrong socket:
    //    else if (this != enteringPiece.socket && Input.GetMouseButtonUp(0))
    //    {
    //        //Debug.Log("Wrong Piece");
    //        enteringPiece.WrongPiece(); 
    //    }
    //}

}
