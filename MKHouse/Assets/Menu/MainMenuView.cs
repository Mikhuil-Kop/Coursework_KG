using UnityEngine.UI;

namespace House.Menu
{
    public class MainMenuView : View
    {
        public static MainMenuView instance;

        public Text[] texts;

        private void Awake()
        {
            instance = this;

            texts[0].text = StringResources.Get("continue_button");
            texts[1].text = StringResources.Get("settings_button");
            texts[2].text = StringResources.Get("save_button");
            texts[3].text = StringResources.Get("load_button");
            texts[4].text = StringResources.Get("exit_button");

            Hide();
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public override void Show(View parent)
        {
            base.Show(parent);
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            base.Hide();
            gameObject.SetActive(false);
        }

        public void OnSettingsButton()
        {
            SettingsView.instance.Show(this);
        }
    }
}
