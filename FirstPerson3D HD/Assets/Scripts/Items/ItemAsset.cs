using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class ItemAsset : ScriptableObject
{
    //Basic Info
    public string itemName = "New Item";
    public Sprite icon = null;
    public Sprite craftingIcon = null;
    public Region location;

    //if it adds a journalpage
    public JournalPage journalPage;
    [TextArea(3, 10)]
    public string descriptionText;

    //if it can be used for crafting
    public bool craftingMaterial;
    public bool isConsumed;
    public bool isResultItem;
}
