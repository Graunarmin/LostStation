using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    #region Sound References
    public Sound hover;
    public Sound playButton;
    public Sound optionsButton;
    public Sound quitButton;

    //public Sound[] BuildArray()
    //{
    //    return new Sound[] { hover, playButton, optionsButton, quitButton };
    //}
    #endregion
    public static AudioManagerMenu audioManager;

    //private Sound[] allSounds;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }

        //allSounds = BuildArray();
    }

    public void PlaySound(Sound sound)
    {
        sound.PlaySound();
    }

    public void StopSound(Sound sound)
    {
        sound.StopSound();

    }


}
