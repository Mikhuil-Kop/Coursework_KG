using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace House.Menu
{
    public class SettingsView : MonoBehaviour
    {
        public static SettingsView instance;

        public RectTransform view, submenuContainer, settingsContainer;
        public SubmenuNode submenuPrefab;
        public SettingNode settingsPrefab;

        private SettingField[] fields;
        private bool active;


        private void Start()
        {
            instance = this;
            fields = Main.instance.GetAllSettings();
            SetActive(false);

            foreach (Transform transform in submenuContainer.transform)
                Destroy(transform.gameObject);

            foreach (Submenu s in (Submenu[])Enum.GetValues(typeof(Submenu)))
                Instantiate(submenuPrefab, submenuContainer).
                    GetComponent<SubmenuNode>().SetSubmenu(s);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetActive(!active);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void SetActive(bool active)
        {
            gameObject.transform.position = gameObject.transform.position;
            this.active = active;

            if (active)
            {
                view.gameObject.SetActive(true);
                settingsContainer.gameObject.SetActive(false);
            }
            else
            {
                view.gameObject.SetActive(false);
                settingsContainer.gameObject.SetActive(false);
            }
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

    public struct SettingField
    {
        public FieldInfo info;
        public SettingValueAttribute attribute;
        public object newValue;
        public bool ValueChanged;
    }

    public enum Submenu : byte
    {
        main = 0, window = 1, aaa = 2
    }
}