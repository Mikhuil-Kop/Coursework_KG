using UnityEngine;

namespace House.Dialogues
{
    public interface IDialogue
    {
        string TextString { get; }
        string NameString { get; }
        bool Finished { get; }
        Sprite Face { get; }

        void Tick();
    }
}