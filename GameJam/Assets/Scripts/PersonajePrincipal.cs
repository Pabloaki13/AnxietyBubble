using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonajePrincipal : MonoBehaviour
{
    public Camera MainCamera;

    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private Vector2 moveDirection; // Direcci�n del movimiento

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Presionar Espacio para activar el shake
        {
            StartCoroutine(MainCamera.GetComponent<CameraShake>().Shake(0.3f, 0.2f)); // Duraci�n 0.5s, Magnitud 0.3
            MainCamera.GetComponent<Camerazoom>().StartZoomIn(-2.0f);
        }

        // Leer entrada del usuario
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calcular direcci�n del movimiento
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Mover al personaje usando Rigidbody2D
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Person")
        {
            StartCoroutine(MainCamera.GetComponent<CameraShake>().Shake(0.3f, 0.2f)); // Duraci�n 0.5s, Magnitud 0.3
            MainCamera.GetComponent<Camerazoom>().StartZoomIn(-1.0f);
        }
    }
}
