using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Text", menuName = "Entity Text", order = 1)]
public class EntityText : ScriptableObject
{
    public string description;

    public static EntityText Load(string code)
    {
        return Resources.Load<EntityText>(Main.language + "/" + code);
    }
}
