using System;

namespace House.Menu
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SettingValueAttribute : Attribute
    {
        public Submenu submenu;
        public string name;
        public bool reloadScene;

        public SettingValueAttribute(Submenu submenu, string name, bool reloadScene)
        {
            this.name = name + "_setting";
            this.submenu = submenu;
            this.reloadScene = reloadScene;
        }
    }
}
