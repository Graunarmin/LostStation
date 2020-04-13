using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseMenu : MonoBehaviour
{

    //public bool paused = false;
    public GameObject pauseMenuUI;
    public bool gameIsPaused = false;

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

    //load Main Menu
    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();

    }
}
