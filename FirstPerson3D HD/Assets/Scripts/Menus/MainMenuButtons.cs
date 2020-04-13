using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject credits;

    private void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        credits.SetActive(false);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void QuitGame()
    {
        Debug.Log("I'm outta here!");
        Application.Quit();
    }

    public void PlayButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.playButton);
        PlayGame();
    }

    public void OptionsButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.optionsButton);
    }

    public void QuitButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.quitButton);
        QuitGame();
    }

    public void BackButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.quitButton);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonHover()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.hover);
    }

    public void HoverExit()
    {
        AudioManagerMenu.audioManager.StopSound(AudioManagerMenu.audioManager.hover);
    }
}
