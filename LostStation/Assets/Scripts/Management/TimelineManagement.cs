using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManagement : MonoBehaviour
{
    public PlayableDirector openingScene;
    public PlayableDirector rideUp;
    public PlayableDirector rideDown;
    public PlayableDirector endAnimation;


    private void Awake()
    {
        Debug.Log("I'm awake");
        openingScene.stopped += OnOpeningSceneStop;
        rideDown.stopped += OnRideDownStop;
        rideUp.stopped += OnRideUpStop;
        endAnimation.stopped += OnEndAnimStop;
    }

    private void Start()
    {
        if (openingScene.gameObject.activeSelf == true)
        {
            GameManager.gameManager.OpeningScenePlaying(true);
        }
    }

    void OnOpeningSceneStop(PlayableDirector director)
    {
        if (openingScene == director)
        {
            GameManager.gameManager.OpeningScenePlaying(false);
        }
    }

    void OnRideUpStop(PlayableDirector director)
    {
        if  (rideUp == director)
        {
            GameManager.gameManager.RideUpPlaying(false);
        }
    }

    void OnRideDownStop(PlayableDirector director)
    {
        if (rideDown == director)
        {
            GameManager.gameManager.RideDownPlaying(false);
        }
    }

    void OnEndAnimStop(PlayableDirector director)
    {
        if (endAnimation == director)
        {
            GameManager.gameManager.EndAnimPlaying(false);
        }
    }
}
