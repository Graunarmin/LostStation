using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseMenu : MonoBehaviour
{

    //public bool paused = false;
    public GameObject pauseMenuUI;
    public bool gameIsPaused = false;

    public void PauseGame()
    {
        GameManager.gameManager.SwitchCameras("2D");
        //PauseMenus Game-Objekt aktiveren
        pauseMenuUI.SetActive(true);

        //freeze Game:
        Time.timeScale = 0f;

        gameIsPaused = true;
        Debug.Log("Pause Game!");

    }

    public void ResumeButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.button);
        ResumeGame();
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        //set speed back to normal
        Time.timeScale = 1f;
        //PauseMenus Game-Objekt deaktivieren
        pauseMenuUI.SetActive(false);
        GameManager.gameManager.SwitchCameras("3D");
        
        Debug.Log("Resume Game!");
    }

    public void ControlsButton(Button button)
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.button);
        button.enabled = false;
        button.enabled = true;
        button.gameObject.GetComponent<Animator>().SetTrigger("Normal");
    }

    //load Main Menu
    public void MenuButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.button);
        LoadMenu();
    }


    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    public void QuitButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.button);
        QuitGame();
    }

    private void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();

    }

    public void BackButton(Button button)
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.button);
        button.enabled = false;
        button.enabled = true;
        button.gameObject.GetComponent<Animator>().SetTrigger("Normal");
    }

    public void HoverButton()
    {
        AudioManagerMenu.audioManager.PlaySound(AudioManagerMenu.audioManager.hover);
    }

    public void HoverExit()
    {
        AudioManagerMenu.audioManager.StopSound(AudioManagerMenu.audioManager.hover);
    }

}
