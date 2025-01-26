using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogs : MonoBehaviour
{
    public TMP_Text Text;
    public string Dialog;
    private string Written;
    float typeSpeed = 0.1f;

    bool isSkippable = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isSkippable) {
                SceneManager.LoadScene("GameScene");
            }
        }
        
    }

    public void type()
    {
        StartCoroutine(TypeText(Dialog));
    }
    private IEnumerator TypeText(string Written)
    {
        Text.text = ""; // Asegúrate de que el texto comience vacío

        foreach (char c in Written)
        {
            Text.text += c; // Añade un carácter al texto actual
            if (c == '.' || c == '?')
            {
                Text.text += '\n';
                yield return new WaitForSeconds(typeSpeed + 0.5f); // Espera antes de añadir el siguiente carácter
            }
            else
                yield return new WaitForSeconds(typeSpeed);
        }
        isSkippable = true;
    }
}
