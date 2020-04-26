using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue Option", menuName = "Dialogue/DialogueOptions")]
public class DialogueOptions : DialogueBase
{
    public CharacterProfile character;
    public CharacterProfile answerProfile;

    [TextArea(2, 8)]
    public string questionText;

    [System.Serializable]
    public class Options
    {
        public string buttonName;
        public DialogueBase nextDialogue;
        public UnityEvent myEvent;
        
    }

    //shown in editor, can be edited
    public Options[] optionsInfo;
}
