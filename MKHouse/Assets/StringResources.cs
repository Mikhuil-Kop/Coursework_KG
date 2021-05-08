using System;
using House;
using UnityEngine;

[CreateAssetMenu(fileName = "New String Resource", menuName = "StringResources", order = 2)]
public class StringResources : ScriptableObject
{
    private static Language language;
    private static StringResources resources;

    [SerializeField]
    private SS[] strings;

    public static string Get(string key)
    {
        if (language != Main.language || resources == null)
        {
            language = Main.language;
            resources = Resources.Load<StringResources>($"{language}/StringResources");
        }
        foreach (var s in resources.strings)
            if (s.key == key)
                return s.value;
        return $"NONE AT KEY ({key})";
    }

    [Serializable]
    struct SS
    {
        public string key, value;
    }
}
