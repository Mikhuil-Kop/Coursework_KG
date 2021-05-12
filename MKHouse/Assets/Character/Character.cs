using UnityEngine;
using House.Menu;


namespace House
{
    [SettingClass]
    public class Character : MonoBehaviour
    {
        public static Character instance;

        public Camera cam;
        [SettingValue(Submenu.controll, "sensX", false)]
        public static float sensivityX = 5;
        [SettingValue(Submenu.controll, "sensY", false)]
        public static float sensivityY = 5;

        [SettingValue(Submenu.controll, "invertX", false)]
        public static bool invertX = false;
        [SettingValue(Submenu.controll, "invertY", false)]
        public static bool invertY = false;

        public float speed;
        private bool active = true;
        private MovableEntity entity;

        private void Awake()
        {
            instance = this;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            active = true;
        }

        public void SetActive(bool active)
        {
            this.active = active;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && View.nowView == null)
            {
                SetActive(false);
                MainMenuView.instance.Show(null);
            }

            if (!active)
            {
                if (View.nowView == null)
                    SetActive(true);
                else
                    return;
            }

            float mouseX = (invertX ? -1 : 1) * Input.GetAxis("Mouse X") * sensivityX / 5;
            float mouseY = (invertY ? -1 : 1) * Input.GetAxis("Mouse Y") * sensivityY / 5;

            transform.Rotate(Vector3.up, mouseX);
            cam.transform.Rotate(Vector3.left, mouseY);

            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, 0, moveY);

            move = move.normalized * Mathf.Min(move.magnitude, 1);

            transform.Translate(move * Time.unscaledDeltaTime * speed);


            if (entity != null)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    entity.enabled = true;
                    entity.transform.parent = null;

                    var rb = entity.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.velocity = new Vector3(10, 0, 0);

                    entity = null;
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    entity.enabled = true;
                    entity.transform.parent = null;
                    entity.GetComponent<Rigidbody>().isKinematic = false;

                    entity = null;
                }
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void GetInHands(MovableEntity entity)
        {
            this.entity = entity;
            entity.enabled = false;
            entity.GetComponent<Rigidbody>().isKinematic = true;
            entity.transform.parent = cam.transform;
        }
    }
}