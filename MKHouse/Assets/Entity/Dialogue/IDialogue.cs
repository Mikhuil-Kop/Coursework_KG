using UnityEngine;

namespace House.Dialogues
{
    public interface IDialogue
    {
        string TextString { get; }
        string NameString { get; }
        bool Finished { get; }
        Color NameColor { get; }

        void Tick();
    }
}