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

    private bool chapter1;
    private bool chapter2;
    private bool chapter3;
    private bool chapter4;

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
        if (!chapter1)
        {
            //Get the flashlight and find the keycard

            //set flashlight to enabled
            DialogueTracker.dialogueTracker.gotFlashlight = true;
            //GameManager.gameManager.flashlightEnabled = true;
            Reference.instance.flashlight.ReceiveFlashlight();

            //set Keycard to collected
            keycard.GetComponent<Interactable>().Interact();

            chapter1 = true;
        }
    }

    private void FinishChapter02()
    {
        if (!chapter2)
        {
            //get the crowbar and repair the generator

            //set crowbar to collected
            crowbar.GetComponent<Interactable>().Interact();

            //set Generator to active
            JigsawManager.jigsawManager.canvas.solved = true;
            GameManager.gameManager.powerIsBack = true;
            Reference.instance.generator.GetComponent<Switcher>().ChangeState();

            chapter2 = true;
        }
    }

    private void FinishChapter03()
    {
        if (!chapter3)
        {
            InventoryManager.invManager.RemoveItem(crowbar);
            //craft Filter and put it in backpack
            InventoryManager.invManager.AddItem(filter);

            chapter3 = true;
        }

    }

    private void FinishChapter04()
    {
        if (!chapter4)
        {
            //CollectAllAliens
            InventoryManager.invManager.RemoveItem(filter);
            InventoryManager.invManager.AddItem(fireAlien);
            InventoryManager.invManager.AddItem(waterAlien);
            InventoryManager.invManager.AddItem(airAlien);

            chapter4 = true;
        }
    }

}
