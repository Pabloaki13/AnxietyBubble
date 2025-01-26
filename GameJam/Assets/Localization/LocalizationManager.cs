using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public LocalizationData localizationData;
    private string currentLanguage = "English";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;
    }

    public string GetTranslation(string key)
    {
        return localizationData.GetTranslation(key, currentLanguage);
    }

    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
}