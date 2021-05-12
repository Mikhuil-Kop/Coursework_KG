using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace House.Menu
{
    public class SettingsView : View
    {
        public static SettingsView instance;

        public RectTransform submenuContainer, settingsContainer;
        public SubmenuNode submenuPrefab;
        public SettingNode settingsPrefab;

        private SettingField[] fields;

        private void Start()
        {
            instance = this;
            fields = Main.instance.GetAllSettingFields();
            Hide();

            foreach (Transform transform in submenuContainer.transform)
                Destroy(transform.gameObject);

            foreach (Submenu s in (Submenu[])Enum.GetValues(typeof(Submenu)))
                Instantiate(submenuPrefab, submenuContainer).
                    GetComponent<SubmenuNode>().SetSubmenu(s);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public override void Show(View parent)
        {
            base.Show(parent);

            gameObject.SetActive(true);
            settingsContainer.gameObject.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();

            gameObject.SetActive(false);
            settingsContainer.gameObject.SetActive(false);
            Main.instance.SaveSettings(fields);
        }

        public void GoToSubmenu(Submenu submenu)
        {
            settingsContainer.gameObject.SetActive(true);
            foreach (Transform transform in settingsContainer.transform)
                Destroy(transform.gameObject);

            int i = 0;
            foreach (SettingField field in GetFields(submenu))
                Instantiate(settingsPrefab, settingsContainer).GetComponent<SettingNode>().SetField(field, i++);
        }

        private IEnumerable<SettingField> GetFields(Submenu submenu)
        {
            foreach(SettingField field in fields)
            {
                if (field.attribute.submenu == submenu)
                    yield return field;
            }
        }
    }

    public class SettingField
    {
        public FieldInfo info;
        public SettingValueAttribute attribute;
        public object newValue;
        public bool ValueChanged;
    }

    public enum Submenu : byte
    {
        main = 0, keys = 1, controll = 2
    }
}