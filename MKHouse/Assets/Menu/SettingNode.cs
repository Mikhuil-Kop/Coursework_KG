using System;
using System.Collections.Generic;
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
        public Slider slider;

        private SettingField field;

        public void SetField(SettingField field, int index)
        {
            this.field = field;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(20, -20 - 80 * index);
            dropdown.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            toggle.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);

            text.text = StringResources.Get(field.attribute.name);
            var type = field.info.FieldType;

            if (type == typeof(bool))
            {
                toggle.gameObject.SetActive(true);
                toggle.isOn = (bool)field.info.GetValue(null);
            }
            else if (type == typeof(float))
            {
                slider.gameObject.SetActive(true);
                slider.value = (float)field.info.GetValue(null);
            }
            else if (type.IsEnum)
            {
                dropdown.gameObject.SetActive(true);
                dropdown.ClearOptions();

                foreach (object s in Enum.GetValues(type))
                    dropdown.AddOptions(new List<string>() { StringResources.Get(s.ToString()) });
            }
            else
                Debug.LogError("Неподходящий тип: " + type);
        }

        public void ToggleChange()
        {
            bool b = toggle.isOn;
            field.newValue = b;
            field.ValueChanged = true;
        }

        public void SliderChange()
        {
            float f = slider.value;
            field.newValue = f;
            field.ValueChanged = true;
        }

        public void DropdownChange()
        {
            int value = dropdown.value;
            //field.newValue = Enum.GetValues(field.info.FieldType).GetValue(value);
            field.newValue = value;
            field.ValueChanged = true;
        }
    }
}