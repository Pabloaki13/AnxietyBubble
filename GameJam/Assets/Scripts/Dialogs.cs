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
        Text.text = ""; // Aseg�rate de que el texto comience vac�o

        foreach (char c in Written)
        {
            Text.text += c; // A�ade un car�cter al texto actual
            if (c == '.' || c == '?')
            {
                Text.text += '\n';
                yield return new WaitForSeconds(typeSpeed + 0.5f); // Espera antes de a�adir el siguiente car�cter
            }
            else
                yield return new WaitForSeconds(typeSpeed);
        }
        isSkippable = true;
    }
}
