using UnityEngine;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnLanguageChanged);

        // Set initial language
        dropdown.value = LocalizationManager.Instance.GetCurrentLanguage() == "English" ? 0 : 1;
    }

    private void OnLanguageChanged(int index)
    {
        string selectedLanguage = index == 0 ? "English" : "Spanish";
        LocalizationManager.Instance.SetLanguage(selectedLanguage);

        // Update all localized text
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
    }
}