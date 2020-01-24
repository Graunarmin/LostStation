using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkipButton : MonoBehaviour
{
    public void SkipVideo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
