using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EndGame();
    }

    public void EndGame()
    {
        //Show black screen first, maybe some text?
        //Or other animation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Reference.instance.crosshair.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
