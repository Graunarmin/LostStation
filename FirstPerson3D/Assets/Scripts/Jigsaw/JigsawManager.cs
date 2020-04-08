using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//pick up: click
//move: drag
//put back: hit delete or let go over inventory
//insert: let go over socket

public class JigsawManager : MonoBehaviour
{
    #region variables

    public static JigsawManager jigsawManager;
    public JigsawCanvas canvas;
    public InventoryJigsaw inventory;
    public Transform inventoryPieces;
    public Transform playfieldPieces;
    public List<Piece> allPieces = new List<Piece>();

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    //public Transform edgeParticles;
    //public Sound sound

    public Piece currentPiece;
    private List<Piece> tmpPieces = new List<Piece>();
    private Piece tmpPiece;

    [HideInInspector]
    public int numberOfPieces;

    [HideInInspector]
    public List<Piece> piecesLocked = new List<Piece>();

    #endregion


    private void Awake()
    {
        if (jigsawManager == null)
        {
            jigsawManager = this;
        }
    }

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

        numberOfPieces = inventory.inventoryPlaces.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        //on mouse down: this piece is picked up
        if (Input.GetMouseButtonDown(0))
        {
            tmpPiece = FindPieceHit();
            if (tmpPiece != null)
            {
                tmpPiece.PickUpPiece();
            }
        }
        //on mouse up: Let the piece go
        if (Input.GetMouseButtonUp(0))
        {
            tmpPiece = FindPieceHit();
            if (tmpPiece != null)
            {
                tmpPiece.SetDownPiece();
            }
        }

        //on delete: return piece to inventory if not already locked
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            tmpPiece = FindPieceHit();
            if (tmpPiece != null)
            {
                tmpPiece.ReturnToInventory();
            }
        }
    }

    //raycast only works in manager method, not in piece class!
    public List<RaycastResult> DoRaycast()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        return results;
    }

    Piece FindPieceHit()
    {
        tmpPieces.Clear();

        List<RaycastResult> results = DoRaycast();

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            //Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.GetComponent<Piece>() != null)
            {
                tmpPieces.Add(result.gameObject.GetComponent<Piece>());
            }
        }

        //if pieces overlap: Sort the list so the upper piece is picked up
        if (tmpPieces.Count > 0)
        {
            tmpPieces.Sort(new SortPiecesByZ());
            //Debug.Log("List: ");
            foreach (Piece p in tmpPieces)
            {
                //Debug.Log(p.gameObject.name);
            }
            return tmpPieces[0];
        }
        return null;
    }
            

    public class SortPiecesByZ : Comparer<Piece>
    {
        public override int Compare(Piece x, Piece y)
        {
            return x.transform.position.z.CompareTo(y.transform.position.z);
        }
    }

    public void ResetGame()
    {
        //if (!canvas.solved)
        //{
        piecesLocked.Clear();

        foreach(Piece piece in allPieces)
        {
            Debug.Log("Resetting " + piece.name);
            piece.pieceLocked = false;
            piece.GetComponent<Image>().raycastTarget = true;
            piece.socket.GetComponent<Image>().raycastTarget = true;
            piece.ReturnToInventory(false);
            piece.piecePickedUp = false;

        }
        //}
    }
}
