using UnityEngine;

namespace House.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue Person", menuName = "Dialogue Person", order = 1)]
    public class DialoguePerson : ScriptableObject
    {
        public Color color;
        public string personName;
        public AudioClip voice;
    }
}
