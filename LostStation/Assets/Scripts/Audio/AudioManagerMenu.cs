using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    #region Sound References
    public Sound hover;
    public Sound button;

    #endregion
    public static AudioManagerMenu audioManager;

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
