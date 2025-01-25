using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public float timeBeforeBubblePop = 1;
    private Image bubble;
    private Color bubbleColor;
    private bool isSkippable = false;

    void Start() {
        StartCoroutine("WaitAndPop");
        bubble = GetComponent<Image>();
        bubbleColor = bubble.color;
    }

    IEnumerator WaitAndPop() {
        yield return new WaitForSecondsRealtime(timeBeforeBubblePop);
        Audio.instance.PlaySFX("bubblePop");
        yield return new WaitForSecondsRealtime(0.08f);
        Color tranparencyColor = bubble.color;
        for (float i = 1.00f; i > 0; i = i - 0.05f) {
            tranparencyColor.a = i;
            bubble.color = tranparencyColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Audio.instance.PlaySFX("phoneCall");
        isSkippable = true;
        yield return new WaitForSecondsRealtime(27);
        BackToTitle();
    }
    
    void BackToTitle() {
        SceneManager.LoadScene("Menu");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isSkippable) {
            SceneManager.LoadScene("Menu");
        }
    }


}
