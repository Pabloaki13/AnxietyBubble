using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : MonoBehaviour {

    bool isCallable = true;
    public float cooldown = 60;
    [SerializeField] GameObject Bubble;
    public Camera MainCamera;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "CabineCall") {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isCallable) {
                    isCallable = false;
                    Audio.instance.PlaySFX("phoneCall");
                    StartCoroutine("CallForHealth");
                    
                }
            }
        }
    }
    
    IEnumerator CallForHealth() {
        yield return new WaitForSecondsRealtime(7);
        Bubble.GetComponent<Bubble>().heal();
        MainCamera.GetComponent<Camerazoom>().StartZoomOut(5);
        Audio.instance.PlaySFX("breathSoft");
        yield return new WaitForSecondsRealtime(cooldown);
        isCallable = true;
    }
}
