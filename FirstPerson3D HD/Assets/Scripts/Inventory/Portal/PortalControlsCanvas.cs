using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControlsCanvas : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] GameObject controls;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject errorOrder;
    [SerializeField] GameObject error;

    private void Awake()
    {
        controls.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(false);
        errorOrder.gameObject.SetActive(false);
        error.gameObject.SetActive(false);
    }
    public void Activate()
    {
        GameManager.gameManager.SwitchCameras("2D");

        gameObject.SetActive(true);
        //ActivateControls();
    }

    public void Close()
    {
        Debug.Log("Close was called!");
        controls.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(false);
        errorOrder.gameObject.SetActive(false);
        error.gameObject.SetActive(false);

        gameObject.SetActive(false);
        GameManager.gameManager.SwitchCameras("3D");
    }

    public void ActivateControls()
    {
        controls.gameObject.SetActive(true);
    }

    public void ActivateLoadingScreen()
    {
        controls.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(true);
    }

    public void ThrowErrorOrder()
    {
        controls.gameObject.SetActive(false);
        errorOrder.gameObject.SetActive(true);
    }

    public void ThrowError()
    {
        controls.gameObject.SetActive(false);
        error.gameObject.SetActive(true);
    }

    public void LoadPortal()
    {
        PortalManager.portal.CheckSolution();
    }
}
