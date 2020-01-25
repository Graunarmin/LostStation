using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDispaly : MonoBehaviour
{
    TextMeshProUGUI displayText;

    //private void Start()
    //{
        
    //    //UpdateDisplay();
    //}

    public void Activate()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        UpdateDisplay();
        StartCoroutine(Close());
    }

    private void UpdateDisplay()
    {
        //string displayName = "none";
        //if(Reference.instance.collectHeld != null)
        //{
        //    if (Reference.instance.collectHeld.useMessage != "")
        //    {
        //        Debug.Log(Reference.instance.collectHeld.useMessage);
        //        displayText.text = Reference.instance.collectHeld.useMessage;
        //    }
        //    else
        //    {
        //        if (Reference.instance.collectHeld.collectName != "")
        //        {
        //            displayName = Reference.instance.collectHeld.collectName;
        //        }
        //        displayText.text = "Item Held: " + displayName;
        //    }
        //}
        
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(4);
        //Reference.instance.collectHeld = null;
        Reference.instance.gameUICanvas.gameObject.SetActive(false);
    }
}
