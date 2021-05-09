using UnityEngine;

namespace House
{
    public class MovableEntity : Entity
    {
        public override void OnInteraction()
        {
            Character.instance.GetInHands(this);
        }
    }
}