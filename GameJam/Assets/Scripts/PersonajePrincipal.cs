using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PersonajePrincipal : MonoBehaviour
{
    public Camera MainCamera;

    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private Vector2 moveDirection; // Dirección del movimiento



    bool tomates = false;
    bool platanos = false;
    bool leche = false;
    bool pagar = false;


    public TMP_Text TomateText;
    public TMP_Text GalletasText;
    public TMP_Text LecheText;
    public TMP_Text PagarText;

    GameObject current_interactable = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Presionar Espacio para activar el shake
        {
            switch(current_interactable.name)
            {
                case "Tomates":
                    TomateText.text = $"<s>{TomateText.text}</s>";
                    break;
                case "Galletas":
                    GalletasText.text = $"<s>{GalletasText.text}</s>";
                    break;
                case "Leche":
                    LecheText.text = $"<s>{LecheText.text}</s>";
                    break;
                case "Pagar":
                    PagarText.text = $"<s>{PagarText.text}</s>";
                    break;
            }
        }

        // Leer entrada del usuario
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calcular dirección del movimiento
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
            StartCoroutine(MainCamera.GetComponent<CameraShake>().Shake(0.3f, 0.2f)); // Duración 0.5s, Magnitud 0.3
            MainCamera.GetComponent<Camerazoom>().StartZoomIn(-1.0f);
        }
        if(collision.gameObject.tag == "Interactable")
        {
            current_interactable = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            current_interactable = null;
        }
    }
}
