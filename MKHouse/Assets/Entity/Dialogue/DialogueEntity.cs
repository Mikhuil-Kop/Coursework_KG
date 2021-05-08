using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace House.Dialogues
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