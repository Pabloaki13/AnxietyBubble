using System;
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
    public float decrease = 2500f;
    public float target = 1;

    private bool isAudioLoading = false;
    public float audioAfter = 2.5f;
    public GameObject player;
    public string type = "";

    void Start() {
        bar = GetComponent<Image>();
        bar.fillAmount = startBar;
    }

    private void Update() {
        StartCoroutine("barCharge");
    }
    public void StartBar()
    {
        StartCoroutine("barCharge");
    }

    IEnumerator barCharge() {
        if (Input.GetKey(KeyCode.Space)) {
            if (bar.fillAmount < target) {
                bar.fillAmount += increase * Time.deltaTime * 25;
                yield return new WaitForEndOfFrame();
            } else {
                if (!isAudioLoading) {
                    isAudioLoading = true;
                    gameObject.GetComponent<Image>().enabled = false;
                    yield return new WaitForSecondsRealtime(audioAfter);
                    
                    if (UnityEngine.Random.Range(0, 100) > 50) {
                        Audio.instance.PlaySFX("babycry");
                    } else {
                        Audio.instance.PlaySFX("shopCardPush");
                    }
                    yield return new WaitForSecondsRealtime(10.5f);
                    Destroy(gameObject);
                }
                
                
            }
        } else {
            if (bar.fillAmount > start) {
                if (!isAudioLoading) {
                    bar.fillAmount -= decrease * Time.deltaTime * 25;
                    yield return new WaitForEndOfFrame();
                }
            } else {
                player.GetComponent<PersonajePrincipal>().fin_de_barra(type);
                Destroy(gameObject);
            }
            player.GetComponent<PersonajePrincipal>().fin_de_barra(type);
            Destroy(this);
        }
    }
}
