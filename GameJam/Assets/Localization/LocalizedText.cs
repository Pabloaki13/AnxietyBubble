using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string translationKey;
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        textComponent.text = LocalizationManager.Instance.GetTranslation(translationKey);
    }
}