using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DescriptionUI : MonoBehaviour
{

	public string myString;
	public TextMeshProUGUI myText;
	public float fadeTime;
	public bool displayInfo;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
		myText = GetComponent<TextMeshProUGUI>();
		myText.color = Color.clear;
        transform.LookAt(player);

	}

    // Update is called once per frame
    void Update()
    {
		FadeText();

    }

    void OnMouseOver()
	{
        displayInfo = true;
	}

    void OnMouseExit()
    {
        displayInfo = false;
    }

    void FadeText()
    {
        if (displayInfo)
        {
            myText.text = myString;
            myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
        }
        else
        {
            myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}
