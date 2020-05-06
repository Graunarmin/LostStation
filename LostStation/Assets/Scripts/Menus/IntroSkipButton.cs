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
        yield return new WaitForSecondsRealtime(32f);
        LoadNextScene();

    }
    public void SkipVideo()
    {
        StopCoroutine(NextScene());
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
