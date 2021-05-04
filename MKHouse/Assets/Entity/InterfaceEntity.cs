using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceEntity : Entity
{
    public Canvas canvas;

    public override void OnInteraction()
    {
        canvas.gameObject.SetActive(true);
    }
}