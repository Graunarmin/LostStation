using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    private void Start()
    {
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeTextToFullAlpha(3f, text));
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("I'm outta here!");
        Application.Quit();
    }


    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI textElement)
    {
        //first set alpha to zero
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 0);

        //wait until video is loaded
        yield return new WaitForSecondsRealtime(2f);

        //the fade in text
        while (textElement.color.a < 1.0f)
        {
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, textElement.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    //public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    //{
    //    i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    //    while (i.color.a > 0.0f)
    //    {
    //        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
    //        yield return null; 
    //    }
    //}
}