using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
