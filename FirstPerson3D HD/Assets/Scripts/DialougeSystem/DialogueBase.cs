using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName ="Dialogue/DialogueBase")]
public class DialogueBase : ScriptableObject
{
    public bool givesJournalInfo;
    public JournalPage journalPage;
    public UnityEvent dialogueEvent;

    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;

        [TextArea(3, 10)]
        public string myText;
    }

    [Header("Insert Dialogue Information below")]
    public Info[] dialogueInfo;
    
}
