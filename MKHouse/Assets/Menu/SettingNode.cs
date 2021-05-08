using UnityEngine;
using UnityEngine.UI;

namespace House.Menu
{
    public class SettingNode : MonoBehaviour
    {
        public Text text;
        public InputField inputField;
        public Dropdown dropdown;
        public Toggle toggle;

        private SettingField field;

        public void SetField(SettingField field, int index)
        {
            this.field = field;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(20, -20 - 80 * index);
            dropdown.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            toggle.gameObject.SetActive(false);

            text.text = StringResources.Get(field.attribute.name);
            var type = field.info.FieldType;

            if (type == typeof(bool))
            {
                toggle.gameObject.SetActive(true);
                toggle.isOn = (bool)field.info.GetValue(null);
            }
            else if (type == typeof(Language))
            {

            }
            else
                Debug.LogError("Неподходящий тип: " + type);
        }

        public void ToggleChange(bool b)
        {
            field.newValue = b;
            field.ValueChanged = true;
        }

        public void DropdownChange()
        {
        }
    }
}