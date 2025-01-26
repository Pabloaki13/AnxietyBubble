using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour {

    // Associate prefab caller with own bar, so if they come back, won't create another
    // Also, on creation, give it the same position of shelf, with extra Y

    private Image bar;
    public float startBar = 0.1f;
    public float start = 0;
    public float increase = 5000f;
    public float decrease = 5000f;
    public float target = 1;

    void Start() {
        bar = GetComponent<Image>();
        bar.fillAmount = startBar;
    }

    private void Update() {
        StartCoroutine("barCharge");
    }

    IEnumerator barCharge() {
        if (Input.GetKey(KeyCode.Space)) {
            if (bar.fillAmount < target) {
                bar.fillAmount += increase * Time.deltaTime * 25;
                yield return new WaitForEndOfFrame();
            } else {
                // Bieeeen! Conseguiste el coso!!!
                Destroy(gameObject);
            }
        } else {
            if (bar.fillAmount > start) {
                bar.fillAmount -= decrease * Time.deltaTime * 12.5f;
                yield return new WaitForEndOfFrame();
            } else {
                Destroy(gameObject);
            }
        }
    }
}
