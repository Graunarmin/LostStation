using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool playTheme;

    #region Sound References
    public Sound theme;
    public Sound doorLocked;
    public Sound smashDoor;
    public Sound flashlightToggle;
    public Sound flashlightFilter;
    public Sound flashlightFilterCrack;
    public Sound flashlightFilterBroken;
    public Sound keypadButtons;
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
    public Sound elevatorButtons;
    public Sound portalControls;
    public Sound portalControlButtons;
    public Sound loadingPortal;
    public Sound errorPortal;

    public Sound[] BuildArray()
    {
        return new Sound[]{theme, doorLocked, smashDoor, flashlightToggle, flashlightFilter, flashlightFilterCrack,
        flashlightFilterBroken, keypadButtons, solutionCorrect, solutionWrong, jigsawPieceCorrect,
        jigsawPieceWrong, returnJigsawPieceToInventory, 
        newJournalPage,openJournal, turnPage, newItem, openInventory, inventoryFull, 
        craftingSlot, crafting, craftingImpossible, getResult, elevatorButtons,
        portalControls, portalControlButtons, loadingPortal, errorPortal};
    }

    #endregion

    public static AudioManager audioManager;

    private Sound[] allSounds;

    private void Awake()
    {
        //so sounds are not cut between scenes
        //DontDestroyOnLoad(gameObject);

        if(audioManager == null)
        {
            audioManager = this;
        }
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        allSounds = BuildArray();

        foreach (Sound s in allSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if (playTheme)
        {
            PlaySound(theme);
        }
        
    }

    public void PlaySound(Sound sound)
    {
        if (sound.clip == null)
        {
            Debug.LogWarning("Sound not defined");
            return;
        }
        sound.source.Play();
    }

    public void StopSound(Sound sound)
    {
        if(sound.clip == null)
        {
            Debug.LogWarning("Sound not defined");
            return;
        }
        sound.source.Stop();

    }
}
