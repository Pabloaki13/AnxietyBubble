using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajePrincipal : MonoBehaviour
{

    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private Vector2 moveDirection; // Dirección del movimiento

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
}
