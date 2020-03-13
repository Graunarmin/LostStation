using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkipButton : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(NextScene());
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSecondsRealtime(32);
        LoadNextScene();

    }
    public void SkipVideo()
    {
        StopCoroutine(NextScene());
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
