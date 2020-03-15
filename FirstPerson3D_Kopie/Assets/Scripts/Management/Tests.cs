using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
    public Collectable keycard;
    public Collectable crowbar;
    public Item filter;
    public Alien fireAlien;
    public Alien airAlien;
    public Alien waterAlien;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FinishChapter01();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            FinishChapter02();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            FinishChapter03();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            FinishChapter04();
        }
    }

    private void FinishChapter01()
    {
        //Get the flashlight and find the keycard

        //set flashlight to enabled
        DialogueTracker.dialogueTracker.gotFlashlight = true;
        GameManager.gameManager.flashlightEnabled = true;

        //set Keycard to collected
        keycard.GetComponent<Interactable>().Interact();
    }

    private void FinishChapter02()
    {
        //get the crowbar and repair the generator

        //set crowbar to collected
        crowbar.GetComponent<Interactable>().Interact();

        //set Generator to active
        JigsawManager.jigsawManager.canvas.solved = true;
        GameManager.gameManager.powerIsBack = true;
        Reference.instance.generator.GetComponent<Switcher>().ChangeState();
    }

    private void FinishChapter03()
    {
        InventoryManager.invManager.RemoveItem(crowbar);
        //craft Filter and put it in backpack
        InventoryManager.invManager.AddItem(filter);

    }

    private void FinishChapter04()
    {
        //CollectAllAliens
        InventoryManager.invManager.RemoveItem(filter);
        InventoryManager.invManager.AddItem(fireAlien);
        InventoryManager.invManager.AddItem(waterAlien);
        InventoryManager.invManager.AddItem(airAlien);
    }

}
