using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] Door entrance;
    [SerializeField] Door exit;

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
        //play animation + Sound
        //...
        CloseDoors();
        StartCoroutine(RideUp());
    }

    public void Down()
    {   //play animation + Sound
        //...
        CloseDoors();
        StartCoroutine(RideDown());
    }

    private IEnumerator RideUp()
    {
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("Arrived upstairs");
        exit.UnblockDoor();
    }

    private IEnumerator RideDown()
    {
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("Arrived downstairs");
        entrance.UnblockDoor();
    }

    public void ButtonSound()
    {
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.elevatorButtons);
    }
}
