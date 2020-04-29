using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] Door entrance;
    [SerializeField] Door exit;
    [SerializeField] CorrectSolutionReaction firstRideAnim;
    [SerializeField] CorrectSolutionReaction rideUpAnim;
    [SerializeField] CorrectSolutionReaction rideDownAnim;

    #region singleton
    public static ElevatorManager elevator;

    private void Awake()
    {
        if (elevator == null)
        {
            elevator = this;
        }
    }
    #endregion


    public void CloseDoors()
    {
        //close both doors
        if (entrance.DoorIsOpen())
        {
            entrance.GetComponent<DoorOpener>().CloseDoor();
        }
        entrance.BlockDoor();
        if (exit.DoorIsOpen())
        {
            exit.GetComponent<DoorOpener>().CloseDoor();
        }
        exit.BlockDoor();
    }
     public void Up()
    {
        CloseDoors();
        StartCoroutine(RideUp());
        rideUpAnim.React();
    }

    public void Down()
    {
        CloseDoors();
        
        //if (firstRide)
        //{
        //    firstRideAnim.React();
        //    firstRide = false;
        //}
        
        StartCoroutine(RideDown());
        rideDownAnim.React();
    }

    private IEnumerator RideUp()
    {
        yield return new WaitForSecondsRealtime(5f);
        entrance.UnblockDoor();
        Debug.Log("Arrived upstairs"); 
    }

    private IEnumerator RideDown()
    {
        yield return new WaitForSecondsRealtime(5f);
        exit.UnblockDoor();
        Debug.Log("Arrived downstairs");
    }

    public void ButtonSound()
    {
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.button);
    }
}
