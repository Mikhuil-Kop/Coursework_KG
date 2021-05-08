using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace House.Dialogues
{
    using NodeType = DialogueText.NodeType;

    [CustomEditor(typeof(DialogueText), true)]
    public class DialogueTextEditor : Editor
    {
        DialogueText text;
        static bool showImproved, showDefault, showText;
        string textString = "";


        public void OnEnable() { text = (DialogueText)target; }

        public override void OnInspectorGUI()
        {
            showImproved = GUILayout.Toggle(showImproved, "Отображать специальное окно");
            if (showImproved)
                ImprovedGUI();
            GUILayout.Space(10);

            showDefault = GUILayout.Toggle(showDefault, "Отображать базовое окно");
            if (showDefault)
                base.OnInspectorGUI();
            GUILayout.Space(10);

            showText = GUILayout.Toggle(showText, "Отображать текстовое окно");
            if (showText)
                TextGUI();

            EditorUtility.SetDirty(text);
        }

        private void ImprovedGUI()
        {
            GUILayout.BeginVertical();

            for (int i = 0; i < text.nodes.Length; i++)
            {
                //паузы
                if (text.nodes[i].type == NodeType.Text)
                    GUILayout.Space(10);
                else if (text.nodes[i].type == NodeType.SetPerson)
                    GUILayout.Space(20);

                //основа
                GUILayout.BeginHorizontal();
                var t = text.nodes[i].type;

                GUILayout.Label(i.ToString(), GUILayout.Width(20));
                text.nodes[i].type = (NodeType)EditorGUILayout.EnumPopup(text.nodes[i].type);


                if (t == NodeType.Text)
                {
                    GUILayout.EndHorizontal();
                    text.nodes[i].arg = GUILayout.TextArea(text.nodes[i].arg);
                }
                else if (t == NodeType.Emotion || t == NodeType.SetPerson || t == NodeType.Speed)
                {
                    text.nodes[i].arg = GUILayout.TextField(text.nodes[i].arg);
                    GUILayout.EndHorizontal();
                }
                else
                {
                    text.nodes[i].arg = "";
                    GUILayout.EndHorizontal();
                }

                //паузы
                if (text.nodes[i].type == NodeType.Hide)
                    GUILayout.Space(30);
            }
            GUILayout.EndVertical();
        }

        private void TextGUI()
        {
            textString = GUILayout.TextArea(textString);

            if (GUILayout.Button("Извлечь"))
                textString = TextFromAsset();

            if (GUILayout.Button("Сохранить"))
                TextToAsset(textString);

            if (GUILayout.Button("Обработать и сохранить"))
                TextToAsset(textString);
        }

        private void TextToAsset(string textString)
        {
            var regex = new Regex(@"#(?<name>\w+)\((?<param>[^#]*)\)");
            var matches = regex.Matches(textString);


            text.nodes = new DialogueText.Node[matches.Count];
            int i = 0;
            foreach (Match m in matches)
            {
                var type = m.Groups["name"].Value;
                var arg = m.Groups["param"].Value;

                text.nodes[i] = new DialogueText.Node();
                text.nodes[i].type = DialogueText.NodeFromString(type);
                text.nodes[i].arg = arg;
                i++;
            }
            Debug.Log("Сохранение текста в файл успешно");
        }

        private string TextFromAsset()
        {
            string s = "";

            foreach(DialogueText.Node node in text.nodes) {
                s += $"#{node.type}({node.arg})\n";
            }
            Debug.Log("Загрузка текста из файла успешна");
            return s;
        }

        struct SS
        {
            public string type, arg;

            public SS(string type, string arg)
            {
                this.type = type;
                this.arg = arg;
            }
        }
    }
}