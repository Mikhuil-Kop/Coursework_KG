using UnityEngine;
using UnityEngine.UI;

namespace House.Menu
{
    public class SubmenuNode : MonoBehaviour
    {
        public Text text;
        private Submenu submenu;

        public void SetSubmenu(Submenu submenu)
        {
            this.submenu = submenu;
            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(20, -20 - 80 * (byte)submenu);
            text.text = StringResources.Get(submenu.ToString() + "_submenu");
        }

        public void OnClick()
        {
            SettingsView.instance.GoToSubmenu(submenu);
        }
    }
}
