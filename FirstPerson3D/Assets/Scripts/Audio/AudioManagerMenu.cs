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

    public Sound[] BuildArray()
    {
        return new Sound[] { hover, playButton, optionsButton, quitButton };
    }
    #endregion
    public static AudioManagerMenu audioManager;

    private Sound[] allSounds;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }

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
        if (sound.clip == null)
        {
            Debug.LogWarning("Sound not defined");
            return;
        }
        sound.source.Stop();

    }


}
