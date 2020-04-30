using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool inPossession;
    private Color baseColor;
    public bool filterOn;

    private void Awake()
    {
        baseColor = gameObject.GetComponent<Light>().color;
    }

    public void ReceiveFlashlight()
    {
        inPossession = true;
    }

    public bool IsInPossession()
    {
        return inPossession;
    }

    public bool SwitchedOn()
    {
        return gameObject.activeInHierarchy;
    }

    public void Toggle()
    {
       

        if (gameObject.activeInHierarchy)
        {
            
            if (filterOn)
            {
                //AlienManager.mrSaru.PauseFlashlight(true);
            }
            else
            {
                gameObject.SetActive(false);
                
            }
        }
        else
        {
            gameObject.SetActive(true);
            if (filterOn)
            {
                //AlienManager.mrSaru.PauseFlashlight(false);
                gameObject.SetActive(true);
                AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightToggle);
            }
            else 
            {
                gameObject.SetActive(true);
                AudioManager.audioManager.PlaySound(AudioManager.audioManager.flashlightToggle);
            }
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
  