using UnityEngine;
using UnityEngine.UI;

namespace House.Menu
{
    public class View : MonoBehaviour
    {
        public static View nowView;
        private View parent = null;

        public virtual void Show(View parent)
        {
            nowView = this;

            if (this.parent == null)
                this.parent = parent;

            if (this.parent != null)
                this.parent.Hide();
        }

        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Hide();
        }

        public virtual void Hide()
        {
            if (nowView == this)
            {
                if (parent != null)
                    parent.Show(null);
                else
                    nowView = null;
            }
        }

        private void OnDestroy()
        {
            if (nowView == this)
                nowView = null;
        }
    }
}
