using UnityEngine;

namespace House.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue Text", menuName = "Dialogue Text", order = 1)]
    public class DialogueText : ScriptableObject
    {
        public Node[] nodes = new Node[0];

        [System.Serializable]
        public class Node
        {
            public NodeType type;
            public string arg;
        }

        public enum NodeType { Text, Clear, Input, Hide, SetPerson, Emotion, Speed }

        public static NodeType NodeFromString(string str)
        {
            str = str.ToUpper();

            switch (str)
            {
                case "SETPERSON": return NodeType.SetPerson;
                case "EMOTION": return NodeType.Emotion;
                case "SPEED": return NodeType.Speed;
                case "INPUT": return NodeType.Input;
                case "CLEAR": return NodeType.Clear;
                case "HIDE": return NodeType.Hide;
                case "TEXT": return NodeType.Text;
                default: throw new System.ArgumentException(str);
            }
        }
    }


}
