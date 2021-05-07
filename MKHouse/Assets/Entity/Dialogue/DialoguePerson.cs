using UnityEngine;

namespace UI.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue Person", menuName = "Dialogue Person", order = 1)]
    public class DialoguePerson : ScriptableObject
    {
        public Sprite face;
        public string personName;
        public AudioClip voice;
    }
}
