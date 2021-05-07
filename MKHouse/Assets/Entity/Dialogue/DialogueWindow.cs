using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace UI.Dialogues
{
    /// <summary>
    /// Класс, отвечающий за вывод диалога на экран
    /// </summary>
    public class DialogueWindow : MonoBehaviour
    {
        public static DialogueWindow instance;
        private IDialogue dialogue;

        //Переменные для отображения
        [SerializeField]
        GameObject view;
        [SerializeField]
        Text text, nameText;
        [SerializeField]
        Image face;
        [SerializeField]
        AudioSource voiceSource;

        bool active = false;

        void Awake()
        {
            instance = this;
            Finish();
        }

        void OnDestroy()
        {
            instance = null;
        }

        /// <summary>
        /// Начинает новый диалог
        /// </summary>
        public void StartDialogue(string fileName)
        {
            dialogue = new ScriptableDialogue(fileName);
            text.text = "";
            nameText.text = "";
            face.sprite = null;
            Show();
        }


        /// <summary>
        /// Отображает диалоговое окно.
        /// </summary>
        public void Show()
        {
            active = true;
            view.SetActive(true);
        }

        /// <summary>
        /// Скрывает диалоговое окно.
        /// </summary>
        public void Hide()
        {
            active = false;
            view.SetActive(false);
        }

        /// <summary>
        /// Завершает диалог и возобновляет TimeController, если он был заблокирован
        /// </summary>
        public void Finish()
        {
            dialogue = null;
            Hide();
        }

        public void PlayVoice(AudioClip clip)
        {
            voiceSource.Stop();
            voiceSource.clip = clip;
            voiceSource.Play();
        }

        void Update()
        {
            if (!active)
                return;
            if (dialogue == null || dialogue.Finished)
            {
                Finish();
                return;
            }

            dialogue.Tick();
            text.text = dialogue.TextString;
            nameText.text = dialogue.NameString;
            face.sprite = dialogue.Face;
        }

        [System.Serializable]
        class Voice
        {
            [SerializeField]
            public string key;
            [SerializeField]
            public AudioClip clip;
        }
    }

}