using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayThemePlay : MonoBehaviour
{
    public GameObject Theme;
    public GameObject FadeOutoldThemeTimeline;
    // Start is called before the first frame update
    private void Start()
    {
        Theme.SetActive(false);
        FadeOutoldThemeTimeline.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        Theme.SetActive(true);
        FadeOutoldThemeTimeline.SetActive(true);
    }
}
