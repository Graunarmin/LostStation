using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Sound References
    public Sound doorLocked;
    public Sound smashDoor;
    public Sound flashlightToggle;
    public Sound flashlightFilter;
    public Sound flashlightFilterCrack;
    public Sound flashlightFilterBroken;
    public Sound button;
    //Those should be the same for keypad, jigsaw and elevator
    public Sound solutionCorrect;
    public Sound solutionWrong;
    public Sound jigsawPieceCorrect;
    public Sound jigsawPieceWrong;
    public Sound returnJigsawPieceToInventory;
    public Sound newJournalPage;
    public Sound openJournal;
    public Sound turnPage;
    public Sound newItem;
    public Sound openInventory;
    public Sound inventoryFull;
    public Sound craftingSlot;
    public Sound crafting;
    public Sound craftingImpossible;
    public Sound getResult;
    //portal Control Sound is the sound that occurs when the controls are activated
    public Sound portalControls;
    //those should be more quiet than the other button sounds
    public Sound portalControlButtons;
    public Sound loadingPortal;
    public Sound errorPortal;

    #endregion

    public static AudioManager audioManager;

    //private Sound[] allSounds;

    private void Awake()
    {

        if (audioManager == null)
        {
            audioManager = this;
        }
    }

    public void PlaySound(Sound sound)
    {
        if (sound != null)
        {
            sound.PlaySound();
        }
    }

    public void StopSound(Sound sound)
    {
        if (sound != null)
        {
            sound.StopSound();
        }
    }
}
