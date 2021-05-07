using System;
using UnityEngine;

namespace UI.Dialogues
{
    public class ScriptableDialogue : IDialogue
    {
        private readonly DialogueText text;
        private DialoguePerson person;

        private int lineIndex = 0;
        private float charIndex = 0;
        private int charsVoiceTimer = 0;
        private float waitTimer = 1f;
        
        private float speed = 25f;
        private int charsBeforeVoice = 3;

        private string textString = "", nameString = "";
        private Sprite face;
        private bool waiting = false, finished = false;

        public string TextString => textString;
        public string NameString => nameString;
        public Sprite Face => face;
        public bool Finished => finished;

        public ScriptableDialogue(string fileName)
        {
            text = Resources.Load<DialogueText>(Main.language + "/DialogueTexts/" + fileName);
        }

        public void Tick()
        {
            bool input = false;


            if (waiting)
            {
                if (waitTimer <= 0)
                    input = true;
                else
                {
                    input = false;
                    waitTimer -= Time.deltaTime;
                }

                if (input)
                    waiting = input = false;
                else
                    return;
            }
            

            //Обработка командных строк
            while (!waiting && !finished && text.nodes[lineIndex].type != DialogueText.NodeType.Text)
            {
                OnComandLine(text.nodes[lineIndex]);
                NextLine();
            }

            //Обработка текстовой строки
            if (!waiting && !finished && text.nodes[lineIndex].type == DialogueText.NodeType.Text)
            {
                //вывод текста
                int oldIndex = (int)charIndex;
                int newIndex;
                bool timeForNextLine;
                charIndex += speed * Time.deltaTime;

                if (input || charIndex > text.nodes[lineIndex].arg.Length)
                {
                    newIndex = text.nodes[lineIndex].arg.Length;
                    timeForNextLine = true;
                }
                else
                {
                    newIndex = (int)charIndex;
                    timeForNextLine = false;
                }

                string addString = "";
                for (int i = oldIndex; i < newIndex; i++)
                    addString += text.nodes[lineIndex].arg[i];
                textString += addString;

                if (timeForNextLine)
                    NextLine();

                //voice
                charsVoiceTimer += addString.Length;
                if (charsVoiceTimer >= charsBeforeVoice)
                {
                    charsVoiceTimer = 0;
                    DialogueWindow.instance.PlayVoice(person.voice);

                }
            }

        }

        void NextLine()
        {
            lineIndex++;
            charIndex = 0;
            if (lineIndex >= text.nodes.Length)
                finished = true;
        }


        #region ComandLines
        //При появлении новых команд - вписывать их здесь
        protected void OnComandLine(DialogueText.Node node)
        {
            switch (node.type)
            {
                case DialogueText.NodeType.SetPerson: SetPersonComand(node.arg); break;
                case DialogueText.NodeType.Emotion: EmotionComand(node.arg); break;
                case DialogueText.NodeType.Speed: SpeedComand(node.arg); break;
                case DialogueText.NodeType.Input: InputComand(); break;
                case DialogueText.NodeType.Clear: ClearComand(); break;
                case DialogueText.NodeType.Hide: HideComand(); break;
                //case "CLOSE": CloseComand(); break;
                default: Debug.LogError($"Неизвестная команда {node.type} в строке {lineIndex}"); break;
            }
        }

        protected virtual void SetPersonComand(string name)
        {
            person = Resources.Load<DialoguePerson>(Main.language + "/DialoguePersons/" + name);
            nameString = person.personName;
            face = person.face;
        }

        protected virtual void EmotionComand(string animationName)
        {
            //DialogueWindow.dialogueWindow.Animate(animationName);
        }

        protected virtual void SpeedComand(string speed)
        {
            if (speed == "")
                this.speed = 25f;
            else
                this.speed = float.Parse(speed);
        }

        protected virtual void InputComand()
        {
            waiting = true;
            waitTimer = 1f;
        }

        protected virtual void ClearComand()
        {
            textString = "";
        }

        protected virtual void HideComand()
        {
            DialogueWindow.instance.Hide();
        }

        protected virtual void CloseComand()
        {
            DialogueWindow.instance.Hide();
        }

        #endregion
    }
}
