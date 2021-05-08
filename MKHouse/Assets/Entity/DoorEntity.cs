using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace House
{
    public class DoorEntity : Entity
    {
        public Animator animator;
        private bool opened;

        public override void OnInteraction()
        {
            opened = !opened;

            if (opened)
                animator.CrossFade("DoorOpen", 0.5f);
            else
                animator.CrossFade("DoorClose", 0.5f);
        }
    }

}