using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : MonoBehaviour {

    bool isCallable = true;
    public float cooldown = 1;
    [SerializeField] GameObject Bubble;

    IEnumerator CallForHealth() {
        Bubble.GetComponent<Bubble>().heal();
        yield return new WaitForSecondsRealtime(cooldown);
        isCallable = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("CabinCall")) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isCallable) {
                    isCallable = false;
                    StartCoroutine("CallForHealth");
                }
            }
        }
    }
}
