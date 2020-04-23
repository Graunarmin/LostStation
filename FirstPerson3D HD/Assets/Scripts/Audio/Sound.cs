using UnityEngine.Audio;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource source;

    //get the respective audiosource from object
    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (source != null)
        {
            if (source.clip != null)
            {
                source.Play();
            }
            else
            {
                Debug.LogWarning("Sound not defined");
            }
        }
        else
        {
            Debug.LogWarning("Sound not defined");
        }
    }

    public void StopSound()
    {
        if (source != null)
        {
            if (source.clip != null)
            {
                source.Stop();
            }
            else
            {
                Debug.LogWarning("Sound not defined");
            }
        }
        else
        {
            Debug.LogWarning("Sound not defined");
        }
    }
}
