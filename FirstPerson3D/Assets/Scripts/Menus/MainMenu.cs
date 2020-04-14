﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    public List<Button> buttons = new List<Button>();

    private void Start()
    {
        StartCoroutine(ActivateButtons());

        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeTextToFullAlpha(3f, text));
        }
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

    public IEnumerator ActivateButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        //Wait until text is loaded
        yield return new WaitForSecondsRealtime(3f);

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
}