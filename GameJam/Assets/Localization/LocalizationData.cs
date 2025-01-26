using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/Language")]
public class LocalizationData : ScriptableObject
{
    [System.Serializable]
    public class Translation
    {
        public string key;
        public string englishText;
        public string spanishText;
    }

    public List<Translation> translations = new List<Translation>();

    public string GetTranslation(string key, string language)
    {
        foreach (var translation in translations)
        {
            if (translation.key == key)
            {
                return language == "English" ? translation.englishText : translation.spanishText;
            }
        }
        return key; // Return the key if no translation is found
    }
}