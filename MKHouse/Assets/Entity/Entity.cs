using System.Collections;
using System.Collections.Generic;
using House.Menu;
using UnityEngine;

namespace House
{

    public class Entity : MonoBehaviour
    {
        public string code;

        public virtual void OnInteraction()
        {

        }

        public void OnMouseDown()
        {
            OnInteraction();
        }

        public void OnMouseOver()
        {
            EntityView.instance.SetEntity(this);
        }

        public void OnMouseExit()
        {
            EntityView.instance.ClearEntity();
        }

        [SettingValue(Submenu.main, "a", false)]
        public static bool A;
        [SettingValue(Submenu.main, "b", false)]
        public static bool B;
        [SettingValue(Submenu.main, "c", false)]
        public static bool C;
    }
}
