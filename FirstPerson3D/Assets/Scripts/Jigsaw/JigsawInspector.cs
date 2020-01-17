using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawInspector : Interactable
{
    public JigsawCanvas JigsawCanvas;

    public override void Interact()
    {
        JigsawCanvas.Activate();

        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();

        StartCoroutine(CheckSolution());
    }

    //count the locked pieces:
    //as they only get locked when in correct postion we only need to count this list
    //and as soon as it contains all pieces we know the jigsaw was solved correctly
    public IEnumerator CheckSolution()
    {
        yield return new WaitUntil(()
            => JigsawManager.jigsawManager.piecesLocked.Count == JigsawManager.jigsawManager.numberOfPieces);

        yield return new WaitForSecondsRealtime(1);

        JigsawManager.jigsawManager.canvas.solved = true;
        GetComponent<Collider>().enabled = false;

        Reference.instance.jigsawCanvas.Close();
    }
}
