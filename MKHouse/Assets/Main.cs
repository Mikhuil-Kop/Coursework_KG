using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using House.Menu;
using UnityEngine;

namespace House
{

    [SettingClass]
    public class Main : MonoBehaviour
    {
        public static Main instance;

        [SettingValue(Submenu.main, "lang", true)]
        public static Language language = Language.Rus;

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void SetSettings(SettingField[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].info.SetValue(null, fields[i].newValue);
                fields[i].ValueChanged = false;
            }
        }


        public SettingField[] GetAllSettings()
        {
            Stack<SettingField> stack = new Stack<SettingField>();

            var assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<SettingClassAttribute>(true) != null)
                    continue;

                var rawFields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo field in rawFields)
                {
                    var attribute = field.GetCustomAttribute<SettingValueAttribute>(true);
                    if (attribute == null)
                        continue;

                    stack.Push(new SettingField
                    {
                        info = field,
                        attribute = attribute
                    });
                }
            }

            return stack.ToArray();
        }
    }
}