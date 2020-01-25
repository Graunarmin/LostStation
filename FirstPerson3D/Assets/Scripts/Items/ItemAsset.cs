using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class ItemAsset : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public JournalPage journalPage;
    [TextArea(3, 10)]
    public string descriptionText;
}
