using UnityEngine;
using House.Menu;


namespace House
{
    [SettingClass]
    public class Character : MonoBehaviour
    {
        public Camera cam;
        [SettingValue(Submenu.controll, "sensX", false)]
        public static float sensivityX;
        [SettingValue(Submenu.controll, "sensY", false)]
        public static float sensivityY;

        [SettingValue(Submenu.controll, "invertX", false)]
        public static bool invertX;
        [SettingValue(Submenu.controll, "invertY", false)]
        public static bool invertY;

        public float speed;

        public static bool active = true;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            active = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                if (active)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }


            if (!active)
                return;

            float mouseX = Input.GetAxis("Mouse X") * sensivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensivityY;

            transform.Rotate(Vector3.up, mouseX);
            cam.transform.Rotate(Vector3.left, mouseY);

            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, 0, moveY);

            move = move.normalized * Mathf.Min(move.magnitude, 1);

            transform.Translate(move * Time.unscaledDeltaTime * speed);
        }
    }
}