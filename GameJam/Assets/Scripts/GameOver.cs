using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private Image bubble;
    private bool isSkippable = false;

    void Start() {
        StartCoroutine("WaitAndPop");
        bubble = GetComponent<Image>();
    }

    IEnumerator WaitAndPop() {
        Audio.instance.PlaySFX("breathHard");
        yield return new WaitForSecondsRealtime(2.5f);
        Audio.instance.PlaySFX("bubblePop");
        yield return new WaitForSecondsRealtime(0.08f);
        Color tranparencyColor = bubble.color;
        for (float i = 1.00f; i > 0; i = i - 0.04f) {
            tranparencyColor.a = i;
            bubble.color = tranparencyColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        isSkippable = true;
        yield return new WaitForSecondsRealtime(1.5f);
        BackToTitle();
    }
    
    void BackToTitle() {
        SceneManager.LoadScene("Intro");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isSkippable) {
            SceneManager.LoadScene("Intro");
        }
    }


}
