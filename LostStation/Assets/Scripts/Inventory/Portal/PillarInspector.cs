using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarInspector : Interactable
{
    public PortalPanel portalPanel;

    public override void Interact()
    {
        portalPanel.Activate();
        portalPanel.ActivatePillar(GetComponent<Item>());
    }
}
