using UnityEngine;
using UnityEngine.UI;

namespace House
{

    public class EntityView : MonoBehaviour
    {
        public static EntityView instance;

        public Text nameText;

        private Entity nowEntity;
        private EntityText nowText;


        private void Awake()
        {
            instance = this;
            ClearEntity();
        }

        private void OnDestroy()
        {
            instance = null;
        }


        public void SetEntity(Entity entity)
        {
            gameObject.SetActive(true);

            nowEntity = entity;
            nowText = EntityText.Load(entity.code);

            nameText.text = nowText.description;
        }

        public void ClearEntity()
        {
            gameObject.SetActive(false);
            nowEntity = null;
            nowText = null;
        }
    }
}