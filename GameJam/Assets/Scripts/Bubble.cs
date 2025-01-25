using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bubble : MonoBehaviour {

    public int life = 1; // 1->3 (4º is death), made this way for line 23
    public Vector2 originSize;
    //public int zoom;
    public bool isInvincible = false;
    public float InvencibleTime = 1.25f;

    private SpriteRenderer sprite;
    private Color originColor;
    private Color originColorTranspararent;

    private void Start() {
        originSize = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
        originColor = sprite.color;
        originColorTranspararent = new (
            originColor.r,
            originColor.g,
            originColor.b,
            .40f);
    }

    public void heal() {
        if (life > 0) life--;
        transform.localScale = new(
                        originSize.x / life,
                        originSize.y / life);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (life < 4) {
                if (!isInvincible) {
                    isInvincible = true;
                    life++;

                    transform.localScale = new(
                        originSize.x / life,
                        originSize.y / life);
                    StartCoroutine("TemporalInvencible");
                }
            } else {
                //SceneManager.LoadScene(La concha)
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            if (life < 4) {
                if (isInvincible) {
                    life++;
                    isInvincible = true;

                    transform.localScale = new(
                        originSize.x / life,
                        originSize.y / life);
                    StartCoroutine("TemporalInvencible");
                }
            } else {
                //SceneManager.LoadScene(La concha)
            }
        }
    }

    IEnumerator TemporalInvencible() {
        Debug.Log("empezó");
        for(int a = 0; a  <2; a++) {
            sprite.color = originColorTranspararent;
            yield return new WaitForSecondsRealtime(InvencibleTime / 5);
            sprite.color = originColor;
            yield return new WaitForSecondsRealtime(InvencibleTime / 5);
        }
        sprite.color = originColorTranspararent;
        yield return new WaitForSecondsRealtime(InvencibleTime / 5);
        sprite.color = originColor;

        isInvincible = false;
    }

}
