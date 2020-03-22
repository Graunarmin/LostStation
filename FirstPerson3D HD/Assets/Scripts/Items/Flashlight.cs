using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool inPosession;
    private Color baseColor;

    private void Awake()
    {
        baseColor = gameObject.GetComponent<Light>().color;
    }

    public bool IsInPossession()
    {
        return inPosession;
    }

    public bool SwitchedOn()
    {
        return gameObject.activeInHierarchy;
    }

    public void Toggle()
    {
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightToggle);

        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ChangeColor(Color col)
    {
        gameObject.GetComponent<Light>().color = col;
    }

    public void ResetColor()
    {
        gameObject.GetComponent<Light>().color = baseColor;
    }

    public float GetAlpha()
    {
        return baseColor.a;
    }
}
  