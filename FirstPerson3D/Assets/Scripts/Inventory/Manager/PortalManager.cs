using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : ItemContainerManager
{
    #region Singleton
    public static PortalManager portal;
    private void Awake()
    {

        if (portal == null)
        {
            portal = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of PortalManager!");
        }
        slots = new ItemSlot[] {null};
    }
    #endregion

    protected override void Start()
    {
        
    }

    //Gets the active slot from PortalPanel
    public void SetSlot(ItemSlot slot)
    {
        slots[0] = slot;
    }
}
