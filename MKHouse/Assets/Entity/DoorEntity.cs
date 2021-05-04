using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntity : Entity
{
    public Animator animator;
    private bool opened;

    public override void OnInteraction()
    {
        opened = !opened;

        if (opened)
            animator.Play("DoorOpen");
        else
            animator.Play("DoorClose");
    }
}
