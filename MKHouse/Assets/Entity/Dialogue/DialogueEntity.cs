using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogues
{
    public class DialogueEntity : Entity
    {
        public string dialogueName;

        public override void OnInteraction()
        {
            DialogueWindow.instance.StartDialogue(dialogueName);
        }
    }
}